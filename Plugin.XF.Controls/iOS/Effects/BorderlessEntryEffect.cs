using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using Plugin.XF.Controls.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Effects")]
[assembly: ExportEffect(typeof(Plugin.XF.Controls.iOS.Effects.BorderlessEntryEffect), nameof(Plugin.XF.Controls.iOS.Effects.BorderlessEntryEffect))]
namespace Plugin.XF.Controls.iOS.Effects
{
    public class BorderlessEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                Control.Layer.BorderWidth = 0;
                (Control as UITextField).BorderStyle = UITextBorderStyle.None;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}