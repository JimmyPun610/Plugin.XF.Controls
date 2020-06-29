using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using Xamarin.Forms;
using Plugin.XF.Controls.Effects;
using Xamarin.Forms.Platform.iOS;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

[assembly: ExportEffect(typeof(Plugin.XF.Controls.iOS.Effects.EntryWithToolbarEffect), nameof(Plugin.XF.Controls.iOS.Effects.EntryWithToolbarEffect))]
namespace Plugin.XF.Controls.iOS.Effects
{
    public class EntryWithToolbarEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            AddActionButton();
        }

        protected override void OnDetached()
        {
        }


        protected void AddActionButton()
        {
            var effect = (Plugin.XF.Controls.Effects.EntryWithToolbarEffect)Element.Effects.FirstOrDefault(e => e is Plugin.XF.Controls.Effects.EntryWithToolbarEffect);
         
            List<ActionButton> actionButtons = effect.ActionButtons;
            if (actionButtons != null)
            {
                //Create toolbar
                UIToolbar toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));
                List<UIBarButtonItem> uIBarButtonItems = new List<UIBarButtonItem>();
                if(actionButtons.Count > 1)
                {
                    foreach (ActionButton actionButton in actionButtons)
                    {
                        UIBarButtonItem barButton = new UIBarButtonItem(actionButton.Title, (UIBarButtonItemStyle)actionButton.BarButtonItemStyle, (s, e) =>
                        {
                            actionButton.OnClicked(e);
                            if (actionButton.DismissKeyboard)
                                this.Control.ResignFirstResponder();
                        });
                        uIBarButtonItems.Add(barButton);
                        if (actionButton.FlexibleSpaceBehind)
                        {
                            //Insert space between action button
                            UIBarButtonItem spacingBarButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
                            uIBarButtonItems.Add(spacingBarButton);
                        }
                    }
                }
                else
                {
                    //Make sure the action button on the right hand side
                    ActionButton actionButton = actionButtons.FirstOrDefault();
                    UIBarButtonItem spacingBarButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

                    uIBarButtonItems.Add(spacingBarButton);
                    UIBarButtonItem barButton = new UIBarButtonItem(actionButton.Title, (UIBarButtonItemStyle)actionButton.BarButtonItemStyle, (s, e) =>
                    {
                        actionButton.OnClicked(e);
                        if (actionButton.DismissKeyboard)
                            this.Control.ResignFirstResponder();
                    });
                    uIBarButtonItems.Add(barButton);
                }
              
                toolbar.Items = uIBarButtonItems.ToArray();
                ((UITextField)Control).InputAccessoryView = toolbar;
            }
         
        }
    }
}