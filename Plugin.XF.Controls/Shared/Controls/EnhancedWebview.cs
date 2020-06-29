using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Forms;

namespace Plugin.XF.Controls.Controls
{
    /// <summary>
    /// Currently allow custom headers and username password for Basic Auth
    /// </summary>
    public class EnhancedWebView : WebView
    {
        /// <summary>
        /// Fired when web view load completed
        /// iOS : DidFinishNavigation
        /// Android : OnPageFinished
        /// Return source URL as action parameter
        /// iOS : WKWebView Url
        /// Android : Android.Webkit.WebView Url
        /// </summary>
        public event EventHandler<string> EnhancedWebViewLoadCompleted;
        public void TriggerEnhancedWebViewLoadCompleted(string url)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                EnhancedWebViewLoadCompleted?.Invoke(this, url);
            });
        }

        /// <summary>
        /// Fired when web view load error
        /// iOS : DidFailNavigation
        /// Android : OnReceivedError
        /// Return error message as action parameter
        /// iOS : NSError DebugDescription
        /// Android : WebResourceError Description
        /// </summary>
        public event EventHandler<string> EnhancedWebViewLoadError;
        public void TriggerEnhancedWebViewLoadError(string errorMsg)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                EnhancedWebViewLoadError?.Invoke(this, errorMsg);
            });
        }

        public Action RefreshPageAction;
        /// <summary>
        /// Reload page
        /// </summary>
        public virtual void RefreshPage()
        {
            RefreshPageAction?.Invoke();
        }

        public static readonly BindableProperty CustomHeadersProperty = BindableProperty.Create(
             propertyName: nameof(CustomHeaders),
             returnType: typeof(Dictionary<string, string>),
             declaringType: typeof(EnhancedWebView),
             defaultValue: default(Dictionary<string, string>)
         );
        public Dictionary<string, string> CustomHeaders
        {
            get => (Dictionary<string, string>)GetValue(CustomHeadersProperty);
            set => SetValue(CustomHeadersProperty, value);
        }

        public static readonly BindableProperty UsernameProperty = BindableProperty.Create(
           propertyName: nameof(Username),
           returnType: typeof(string),
           declaringType: typeof(EnhancedWebView),
           defaultValue: default(string)
       );

        public string Username
        {
            get => (string)GetValue(UsernameProperty);
            set => SetValue(UsernameProperty, value);
        }

        public static readonly BindableProperty PasswordProperty = BindableProperty.Create(
         propertyName: nameof(Password),
         returnType: typeof(string),
         declaringType: typeof(EnhancedWebView),
         defaultValue: default(string)
        );

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        

    }
}
