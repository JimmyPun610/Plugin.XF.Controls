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
using Plugin.XF.Controls.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportEffect(typeof(EntryWithToolbarEffect), nameof(EntryWithToolbarEffect))]
namespace Plugin.XF.Controls.Droid.Effects
{
    public class EntryWithToolbarEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
          
        }

        protected override void OnDetached()
        {
        }

    }
}