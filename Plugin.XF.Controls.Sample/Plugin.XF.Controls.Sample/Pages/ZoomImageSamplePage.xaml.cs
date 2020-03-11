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
    public partial class ZoomImageSamplePage : ContentPage
    {
        public ZoomImageSamplePage()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}