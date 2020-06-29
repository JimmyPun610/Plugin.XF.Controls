using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.XF.Controls.Controls
{
    /// <summary>
    /// iOS : Allow settings SearchBar Style
    /// Android : Auto set HeightRequest to 40 for Android N if HeightRequest is not defined
    /// </summary>
    public class EnhancedSearchBar : SearchBar
    {
        public static readonly BindableProperty iOSSearchBarStyleProperty = BindableProperty.Create(
                propertyName: nameof(iOSSearchBarStyle),
                returnType: typeof(iOSSearchBarStyles),
                declaringType: typeof(EnhancedSearchBar),
                defaultValue: iOSSearchBarStyles.Default
            );
        public iOSSearchBarStyles iOSSearchBarStyle
        {
            get => (iOSSearchBarStyles)GetValue(iOSSearchBarStyleProperty);
            set => SetValue(iOSSearchBarStyleProperty, value);
        }
        public enum iOSSearchBarStyles
        {
            Default = 0,
            Prominent = 1,
            Minimal = 2
        }

    }
}
