using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using Plugin.XF.Controls.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(Plugin.XF.Controls.iOS.Effects.BorderlessEditorEffect), nameof(Plugin.XF.Controls.iOS.Effects.BorderlessEditorEffect))]
namespace Plugin.XF.Controls.iOS.Effects
{
    public class BorderlessEditorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                (Control as UITextView).TextContainer.LineBreakMode = UILineBreakMode.WordWrap;
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