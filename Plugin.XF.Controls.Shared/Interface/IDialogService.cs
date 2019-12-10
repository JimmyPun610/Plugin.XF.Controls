using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.XF.Controls.Shared.Interface
{
    public interface IDialogService
    {
        void ShowSnackbar(string text, int duration, Xamarin.Forms.Color textColor, Xamarin.Forms.Color backgroundColor, float backgroundAlpha, string actionText, Xamarin.Forms.Color actionTextColor, Action action);
    }
}
