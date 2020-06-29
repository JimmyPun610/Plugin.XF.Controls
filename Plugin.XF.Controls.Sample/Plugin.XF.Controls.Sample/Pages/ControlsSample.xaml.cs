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
    public partial class ControlsSample : ContentPage
    {
        public ControlsSample()
        {
            InitializeComponent();
        }

        private void ActionButton1_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Button 1 Clicked", "You clicked Action Button 1", "OK");
        }

        private void ActionButton2_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Button 2 Clicked", "You clicked Action Button 2", "OK");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string randomMessage = $"{new Random().Next(0, 100)} : Non all capital letter button for Android";
            //Keep action = null if you just want to dismiss the snackbar
            Services.DialogService.ShowSnackbar(randomMessage, 3, Color.White, Color.Blue, 0.75f, "OK", Color.Yellow, ()=>
            {
                DisplayAlert("Action Button Clicked", "You clicked action button", "OK");

            });
        }
    }
}