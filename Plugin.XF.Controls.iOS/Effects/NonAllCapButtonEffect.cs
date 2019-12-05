using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Xamarin.Forms;
using Plugin.XF.Controls.iOS.Effects;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(NonAllCapButtonEffect), nameof(NonAllCapButtonEffect))]
namespace Plugin.XF.Controls.iOS.Effects
{
    public class NonAllCapButtonEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
        }

        protected override void OnDetached()
        {
        }
      
    }
   
}