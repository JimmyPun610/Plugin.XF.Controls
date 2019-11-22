using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebKit;

namespace Plugin.XF.Controls.iOS
{
    public partial class WebViewDelegate : WKNavigationDelegate, INSUrlConnectionDataDelegate
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public override void DidReceiveAuthenticationChallenge(WKWebView webView, NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler)
        {
            //base.DidReceiveAuthenticationChallenge(webView, challenge, completionHandler);
            completionHandler(NSUrlSessionAuthChallengeDisposition.UseCredential, new NSUrlCredential(Username, Password, NSUrlCredentialPersistence.ForSession));
            return;
        }
    }
}
