﻿using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using UIKit;

namespace Plugin.XF.Controls.iOS
{
    public class RendererInitializar
    {
        public static void Init()
        {
            Renderer.EnhancedWebViewRenderer.Init();
            Renderer.ZoomImageRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
        }
    }
}