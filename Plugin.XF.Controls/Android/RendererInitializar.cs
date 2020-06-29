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

namespace Plugin.XF.Controls.Droid
{
    public class RendererInitializer
    {
        public static void Init(Context context, Bundle bundle, bool? enableFastRenderer)
        {
            Renderer.EnhancedWebViewRenderer.Init();
            Renderer.ZoomImageRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer);
        }
    }
}