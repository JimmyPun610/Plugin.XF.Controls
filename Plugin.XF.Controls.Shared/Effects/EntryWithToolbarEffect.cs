using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Plugin.XF.Controls.Shared.Effects
{
    /// <summary>
    /// Only effects on iOS
    /// </summary>
    public class EntryWithToolbarEffect : RoutingEffect
    {
        public EntryWithToolbarEffect() : base("Effects.EntryWithToolbarEffect")
        {
        }
        public List<ActionButton> ActionButtons { get; set; } = new List<ActionButton>();
    }
    public enum UIBarButtonItemStyles
    {
        Plain = 0,
        Bordered = 1,
        Done = 2
    }
    public class ActionButton
    {
        public string Title { get; set; }
        public event EventHandler Clicked;
        public object Parameter { get; set; }
        public bool DismissKeyboard { get; set; } = true;
        public bool FlexibleSpaceBehind { get; set; } = true;
        public UIBarButtonItemStyles BarButtonItemStyle { get; set; }
        public virtual void OnClicked(EventArgs e)
        {
            EventHandler handler = Clicked;
            if (handler != null)
                handler(this, e);
        }
    }
}
