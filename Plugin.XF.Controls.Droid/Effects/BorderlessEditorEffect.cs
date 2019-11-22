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

[assembly: ExportEffect(typeof(BorderlessEditorEffect), nameof(BorderlessEditorEffect))]
namespace Plugin.XF.Controls.Droid.Effects
{

    public class BorderlessEditorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                EditText editText = (Control as EditText);
                editText.Background = null;
                editText.InputType = Android.Text.InputTypes.TextFlagCapSentences | Android.Text.InputTypes.TextFlagMultiLine;
                editText.ImeOptions = Android.Views.InputMethods.ImeAction.Unspecified;
                editText.SetSingleLine(false);
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
