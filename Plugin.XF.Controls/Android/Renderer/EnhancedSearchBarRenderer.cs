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
using Plugin.XF.Controls.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EnhancedSearchBar), typeof(EnhancedSearchBarRenderer))]
namespace Plugin.XF.Controls.Droid.Renderer
{
    public class EnhancedSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N && Element.HeightRequest == -1)
                Element.HeightRequest = 40;
        }
    }
}