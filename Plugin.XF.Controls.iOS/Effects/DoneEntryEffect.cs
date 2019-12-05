using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Xamarin.Forms;
using Plugin.XF.Controls.iOS.Effects;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(DoneEntryEffect), nameof(DoneEntryEffect))]
namespace Plugin.XF.Controls.iOS.Effects
{
    public class DoneEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            AddDoneButton();
        }

        protected override void OnDetached()
        {
        }
        protected void AddDoneButton()
        {
            UIToolbar toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate {
                this.Control.ResignFirstResponder();
            });

            toolbar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };
            ((UITextField)Control).InputAccessoryView = toolbar;
        }
    }
}