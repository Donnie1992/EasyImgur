using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EasyImgur
{
    static class Program
    {
        static System.Threading.Mutex mutex = new System.Threading.Mutex(true, "EasyImgurMutex");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();

            bool daemonMode = false;
            foreach (string arg in commandLineArgs)
            {
                switch (arg)
                {
                    case "-D":
                        {
                            daemonMode = true;
                            break;
                        }
                }
            }


            bool isServer = false;
            // Try to lock the mutex. If we can, we're the very first instance of the application and therefore we need to 
            // run in server mode and ensure a UI.
            if (mutex.WaitOne(0, true))
            {
                isServer = true;
            }

            Log.Info("Running as " + (isServer ? "server" : "client"));

            InitializeMarshalledObjects(isServer);

            // The server always requires a UI. If the command line tells us we want to run as a daemon,
            // but we actually also need to be a server, we run as a server.
            if (daemonMode && !isServer)
            {
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form1 form = new Form1();
                form.Initialize();
                Properties.Settings.Default.Reload();   // To make sure we can access the current settings.
                Application.Run();
                mutex.ReleaseMutex();
            }
        }

        static void InitializeMarshalledObjects( bool _IsServer )
        {
            if (_IsServer)
            {
                History.impl = new HistoryServer();
                ImgurAPI.impl = new ImgurAPIServer();

                System.Runtime.Remoting.Channels.BinaryClientFormatterSinkProvider clientProvider = new System.Runtime.Remoting.Channels.BinaryClientFormatterSinkProvider();
                System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider serverProvider = new System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider();
                
                serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

                System.Collections.Hashtable properties = new System.Collections.Hashtable()
                {
                    {"name", "EasyImgurServerChannel"},
                    {"port", 9000}
                };
                System.Runtime.Remoting.Channels.Tcp.TcpChannel channel = new System.Runtime.Remoting.Channels.Tcp.TcpChannel(properties, clientProvider, serverProvider);
                System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(channel, false);
                System.Runtime.Remoting.RemotingServices.Marshal(History.impl, "HistoryServer");
                System.Runtime.Remoting.RemotingServices.Marshal(ImgurAPI.impl, "ImgurAPIServer");
            }
            else
            {
                History.impl = (HistoryServer)Activator.GetObject(typeof(HistoryServer), "tcp://localhost:9000/HistoryServer");
                ImgurAPI.impl = (ImgurAPIServer)Activator.GetObject(typeof(ImgurAPIServer), "tcp://localhost:9000/ImgurAPIServer");


                System.Runtime.Remoting.Channels.BinaryClientFormatterSinkProvider clientProvider = new System.Runtime.Remoting.Channels.BinaryClientFormatterSinkProvider();
                System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider serverProvider = new System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider();

                serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

                System.Collections.Hashtable properties = new System.Collections.Hashtable()
                {
                    {"name", "EasyImgurClientChannel"},
                    {"port", 9001}
                };
                System.Runtime.Remoting.Channels.Tcp.TcpChannel channel = new System.Runtime.Remoting.Channels.Tcp.TcpChannel(properties, clientProvider, serverProvider);
                System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(channel, false);
            }
        }
    }
}
