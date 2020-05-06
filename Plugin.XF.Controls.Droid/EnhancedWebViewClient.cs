using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace Plugin.XF.Controls.Droid
{
    public class EnhancedWebViewClient : Android.Webkit.WebViewClient
    {
        public Dictionary<string, string> Headers { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public event Action<string> LoadCompleted;
        public event Action<string> LoadError;

        public EnhancedWebViewClient()
        {
        }
   
        public override void OnReceivedHttpAuthRequest(WebView view, HttpAuthHandler handler, string host, string realm)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                handler.Proceed(Username, Password);
            else base.OnReceivedHttpAuthRequest(view, handler, host, realm);
        }

        public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
        {
            return base.ShouldOverrideUrlLoading(view, request);

        }

        public override void OnPageStarted(Android.Webkit.WebView view, string url, Android.Graphics.Bitmap favicon)
        {
            System.Diagnostics.Debug.WriteLine("Loading website...");
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            LoadCompleted?.Invoke(view.Url);
            System.Diagnostics.Debug.WriteLine("Load finished.");
        }

        public override void OnReceivedError(Android.Webkit.WebView view, IWebResourceRequest request, WebResourceError error)
        {
            LoadError?.Invoke(error.Description);
            base.OnReceivedError(view, request, error);
        }
    }
}