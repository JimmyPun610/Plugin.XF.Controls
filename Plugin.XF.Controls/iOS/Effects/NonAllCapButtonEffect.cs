using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Xamarin.Forms;
using Plugin.XF.Controls.Effects;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(Plugin.XF.Controls.iOS.Effects.NonAllCapButtonEffect), nameof(Plugin.XF.Controls.iOS.Effects.NonAllCapButtonEffect))]
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