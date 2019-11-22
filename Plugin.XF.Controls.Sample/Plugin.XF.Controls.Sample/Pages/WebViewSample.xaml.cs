using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.XF.Controls.Sample.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewSample : ContentPage
    {
        public WebViewSample()
        {
            InitializeComponent();

            Dictionary<string, string> customHeaders = new Dictionary<string, string>();
            customHeaders.Add("Header1", "Value1");
            customHeaders.Add("Header2", "Value2");
            customHeaders.Add("Header3", "Value3");
            EnhancedWeb.CustomHeaders = customHeaders;
            EnhancedWeb.Username = "user";
            EnhancedWeb.Password = "password";
            UrlWebViewSource urlWebViewSource = new UrlWebViewSource();
            urlWebViewSource.Url = "https://www.whatismybrowser.com/detect/what-http-headers-is-my-browser-sending";
            EnhancedWeb.Source = urlWebViewSource;
        }
    }
}