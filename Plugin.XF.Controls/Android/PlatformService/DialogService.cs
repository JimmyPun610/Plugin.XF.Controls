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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;



namespace Plugin.XF.Controls.Droid.PlatformService
{
    public class DialogService 
    {
        public static void ShowSnackbar(string text, int duration, Color textColor, Color backgroundColor, float backgroundAlpha, string actionText, Color actionTextColor, Action action)
        {
            var activity = (Android.App.Activity)Forms.Context;
            var view = activity.FindViewById(Android.Resource.Id.Content);
            var snackbar = Android.Support.Design.Widget.Snackbar.Make(view, text, duration * 1000);
            snackbar.View.Alpha = backgroundAlpha;
            snackbar.View.SetBackgroundColor(backgroundColor.ToAndroid());
            TextView tv = snackbar.View.FindViewById<TextView>(Resource.Id.snackbar_text);
            tv.SetTextColor(textColor.ToAndroid());
            var snackAction = new Action<Android.Views.View>((Android.Views.View obj) =>
            {
                if (action != null) action();
                snackbar.SetDuration(0);
            });
            snackbar.SetAction(actionText, snackAction);
            snackbar.SetActionTextColor(actionTextColor.ToAndroid());
            snackbar.Show();

        }

    }
}