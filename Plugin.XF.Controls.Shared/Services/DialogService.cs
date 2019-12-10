using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.XF.Controls.Shared.Services
{
    public static class DialogService
    {
        public static void ShowSnackbar(string text, int duration, Xamarin.Forms.Color textColor, Xamarin.Forms.Color backgroundColor, float backgroundAlpha, string actionText, Xamarin.Forms.Color actionTextColor, Action action)
        {
            DependencyService.Get<Interface.IDialogService>().ShowSnackbar(text, duration, textColor, backgroundColor, backgroundAlpha, actionText, actionTextColor, action);
        }
    }
}
