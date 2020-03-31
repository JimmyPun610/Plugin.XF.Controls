using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.XF.Controls.Droid.Renderer;
using Plugin.XF.Controls.Shared.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EnhancedWebView), typeof(EnhancedWebViewRenderer))]
namespace Plugin.XF.Controls.Droid.Renderer
{
    public class EnhancedWebViewRenderer : ViewRenderer<EnhancedWebView, Android.Webkit.WebView>
    {
        Android.Content.Context _localContext;
        public EnhancedWebViewRenderer(Context context) : base(context)
        {
            _localContext = context;
        }
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<EnhancedWebView> e)
        {
            base.OnElementChanged(e);

            Android.Webkit.WebView webView = Control as Android.Webkit.WebView;

            if (Control == null)
            {
                webView = new Android.Webkit.WebView(_localContext);
                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                // ...
            }

            if (e.NewElement != null)
            {
                Dictionary<string, string> headers = Element.CustomHeaders;
                webView.Settings.JavaScriptEnabled = true;
                webView.Settings.BuiltInZoomControls = false;
                //webView.Settings.MediaPlaybackRequiresUserGesture = false;
                webView.Settings.SetSupportZoom(true);
                webView.Settings.DomStorageEnabled = true;
                EnhancedWebViewClient webViewClient = new EnhancedWebViewClient();
                webViewClient.Username = Element.Username;
                webViewClient.Password = Element.Password;
                webViewClient.Headers = headers;
                webView.SetWebViewClient(webViewClient);
                if(Element.Source != null)
                {
                    if(Element.Source.GetType() == typeof(UrlWebViewSource))
                    {
                        UrlWebViewSource source = Element.Source as UrlWebViewSource;
                        webView.LoadUrl(source.Url, headers);
                    }
                }
                
            }
        }

    }
}