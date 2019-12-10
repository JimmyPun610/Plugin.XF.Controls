using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.XF.Controls.Shared.Controls
{
    public class Snackbar
    {
        private static Snackbar _instance { get; set; }
        public static Snackbar Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Snackbar();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }


    }
}
