﻿using System;
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
    }
}