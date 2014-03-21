using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;
using System.Linq;

namespace EasyImgur
{
    class ImgurAPI
    {
        static public ImgurAPIServer impl = null;

        static public event ImgurAPIServer.AuthorizationEventHandler obtainedAuthorization
        {
            add { impl.obtainedAuthorization += value; }
            remove { impl.obtainedAuthorization -= value; }
        }
        static public event ImgurAPIServer.AuthorizationEventHandler lostAuthorization
        {
            add { impl.lostAuthorization += value; }
            remove { impl.lostAuthorization -= value; }
        }
        static public event ImgurAPIServer.AuthorizationEventHandler refreshedAuthorization
        {
            add { impl.refreshedAuthorization += value; }
            remove { impl.refreshedAuthorization -= value; }
        }
        static public event ImgurAPIServer.NetworkEventHandler networkRequestFailed
        {
            add { impl.networkRequestFailed += value; }
            remove { impl.networkRequestFailed -= value; }
        }

        static public int numSuccessfulUploads
        {
            get
            {
                return impl.m_NumUploads;
            }
        }

        static public APIResponses.ImageResponse UploadImage( Image _Image, string _Title, string _Description, bool _Anonymous )
        {
            return impl.UploadImage(_Image, _Title, _Description, _Anonymous);
        }

        static public APIResponses.ImageResponse UploadImage( string _URL, string _Title, string _Description, bool _Anonymous )
        {
            return impl.UploadImage(_URL, _Title, _Description, _Anonymous);
        }

        static public bool DeleteImage( string _DeleteHash, bool _AnonymousImage )
        {
            return impl.DeleteImage(_DeleteHash, _AnonymousImage);
        }

        static public void OpenAuthorizationPage()
        {
            impl.OpenAuthorizationPage();
        }

        static public void RequestTokens( string _PIN )
        {
            impl.RequestTokens(_PIN);
        }

        static public void ForceRefreshTokens()
        {
            impl.ForceRefreshTokens();
        }

        // This function attempts to read any old refresh and access tokens from the settings file
        // and then tries to use these to obtain new ones. This method should be called at the start of the application
        // in order to be able to persistently keep the app authorized after the user doing so once.
        static public void AttemptRefreshTokensFromDisk()
        {
            impl.AttemptRefreshTokensFromDisk();
        }

        static public bool HasBeenAuthorized()
        {
            return impl.HasBeenAuthorized();
        }

        static public void OnMainThreadExit()
        {
            impl.OnMainThreadExit();
        }

        static public void ForgetTokens()
        {
            impl.ForgetTokens();
        }
    }
}
