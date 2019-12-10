using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using Plugin.XF.Controls.iOS.PlatformService;
using Plugin.XF.Controls.Shared.Interface;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DialogService))]

namespace Plugin.XF.Controls.iOS.PlatformService
{
    public class DialogService : IDialogService
    {
        public void ShowSnackbar(string text, int duration, Xamarin.Forms.Color textColor, Xamarin.Forms.Color backgroundColor, float backgroundAlpha, string actionText, Xamarin.Forms.Color actionTextColor, Action action)
        {
            var snackbar = View.SnackBar.Instance;
            Xamarin.Forms.Color XFTextColor = textColor;

            snackbar.TextColor = UIColor.FromRGBA(nfloat.Parse(XFTextColor.R.ToString()), nfloat.Parse(XFTextColor.G.ToString()), nfloat.Parse(XFTextColor.B.ToString()), nfloat.Parse(XFTextColor.A.ToString()));
           
            Xamarin.Forms.Color XFBackgroundColor = backgroundColor;
            snackbar.BackgroundColor = UIColor.FromRGBA(nfloat.Parse(XFBackgroundColor.R.ToString()), nfloat.Parse(XFBackgroundColor.G.ToString()), nfloat.Parse(XFBackgroundColor.B.ToString()), nfloat.Parse(XFBackgroundColor.A.ToString()));

            Xamarin.Forms.Color XFActionTextColor = actionTextColor;
            snackbar.ButtonColor = UIColor.FromRGB(nfloat.Parse(XFActionTextColor.R.ToString()), nfloat.Parse(XFActionTextColor.G.ToString()), nfloat.Parse(XFActionTextColor.B.ToString()));

            snackbar.Alpha = backgroundAlpha;

            snackbar.ShowSnackBar(text, duration, actionText, action);
        }
    }
}