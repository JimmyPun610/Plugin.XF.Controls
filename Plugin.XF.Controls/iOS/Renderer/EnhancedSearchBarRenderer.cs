using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Plugin.XF.Controls.Controls;
using Xamarin.Forms;
using Plugin.XF.Controls.iOS.Renderer;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EnhancedSearchBar), typeof(EnhancedSearchBarRenderer))]
namespace Plugin.XF.Controls.iOS.Renderer
{
   
    public class EnhancedSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;
            if (Control != null)
            {
                EnhancedSearchBar searchBar = (EnhancedSearchBar)Element;
                Control.SearchBarStyle = (UISearchBarStyle)searchBar.iOSSearchBarStyle;
            }
        }
    }
}