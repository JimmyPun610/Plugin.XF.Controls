using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plugin.XF.Controls.Sample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OpenWeb_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.WebViewSample());
        }

        private async void OpenSample_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.ControlsSample());

        }

        private async void OpenZoomImage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Pages.ZoomImageSamplePage()));

        }

        void ShowSnackbar_Clicked(System.Object sender, System.EventArgs e)
        {
            Plugin.XF.Controls.Services.DialogService.ShowSnackbar("Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg Testing msg ", 3, Color.White, Color.Red, 0.8f, "Hide", Color.Yellow, null);
        }
    }
}
