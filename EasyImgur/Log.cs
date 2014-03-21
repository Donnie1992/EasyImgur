using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyImgur
{
    public class Log
    {
        private static System.Object ThreadLock = new System.Object();
        private static string LogFile = "log.log";
        private static bool FirstInvocation = true;

        public static string Info( string _Message )
        {
            return LogMessage("INFO", _Message);
        }

        public static string Warning( string _Message )
        {
            return LogMessage("WARNING", _Message);
        }

        public static string Error( string _Message )
        {
            return LogMessage("ERROR", _Message);
        }

        private static string LogMessage( string _Prefix, string _Message )
        {
            string TimeStamp = System.DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss:ffff]", System.Globalization.CultureInfo.InvariantCulture);
            string line = TimeStamp + " [" + _Prefix + "] " + _Message;
            lock (ThreadLock)
            {
                if (FirstInvocation)
                {
                    FirstInvocation = false;
                    try
                    {
                        System.IO.File.WriteAllText(LogFile, string.Empty);
                    }
                    catch (System.Exception ex)
                    {
                        // Can't exactly log it here, so we do nothing.
                    }
                }
                try
                {
                    using (System.IO.StreamWriter w = System.IO.File.AppendText(LogFile))
                    {
                        w.WriteLine(line);
                    }
                }
                catch (System.Exception ex)
                {
                    // Do nothing.
                }
                Console.WriteLine(line);
            }
            return line;
        }
    }
}
