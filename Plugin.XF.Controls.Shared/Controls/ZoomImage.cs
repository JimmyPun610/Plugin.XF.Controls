﻿using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plugin.XF.Controls.Shared.Controls
{
    public class ZoomImage : CachedImage
    {
        public ZoomImage()
        {

        }

        public static readonly BindableProperty ZoomEnabledProperty =
            BindableProperty.Create<ZoomImage, bool>(p => p.ZoomEnabled, true, BindingMode.Default);

        public bool ZoomEnabled
        {
            get { return (bool)GetValue(ZoomEnabledProperty); }
            set { SetValue(ZoomEnabledProperty, value); }
        }

        public static readonly BindableProperty ScrollEnabledProperty =
            BindableProperty.Create<ZoomImage, bool>(p => p.ScrollEnabled, true, BindingMode.Default);

        public bool ScrollEnabled
        {
            get { return (bool)GetValue(ScrollEnabledProperty); }
            set { SetValue(ScrollEnabledProperty, value); }
        }

        public static readonly BindableProperty DoubleTapToZoomEnabledProperty =
            BindableProperty.Create<ZoomImage, bool>(p => p.DoubleTapToZoomEnabled, true, BindingMode.Default);

        public bool DoubleTapToZoomEnabled
        {
            get { return (bool)GetValue(DoubleTapToZoomEnabledProperty); }
            set { SetValue(DoubleTapToZoomEnabledProperty, value); }
        }

        public static readonly BindableProperty TapZoomScaleProperty =
            BindableProperty.Create<ZoomImage, double>(p => p.TapZoomScale, 2.0, BindingMode.Default);

        public double TapZoomScale
        {
            get { return (double)GetValue(TapZoomScaleProperty); }
            set { SetValue(TapZoomScaleProperty, value); }
        }

        public static readonly BindableProperty MaxZoomProperty =
            BindableProperty.Create<ZoomImage, double>(p => p.MaxZoom, 10.0, BindingMode.Default);

        public double MaxZoom
        {
            get { return (double)GetValue(MaxZoomProperty); }
            set { SetValue(MaxZoomProperty, value); }
        }

        public static readonly BindableProperty MinZoomProperty =
            BindableProperty.Create<ZoomImage, double>(p => p.MinZoom, 1, BindingMode.Default);

        public double MinZoom
        {
            get { return (double)GetValue(MinZoomProperty); }
            set { SetValue(MinZoomProperty, value); }
        }

        public static readonly BindableProperty CurrentZoomProperty =
            BindableProperty.Create<ZoomImage, double>(p => p.CurrentZoom, 1.0, BindingMode.Default);

        public double CurrentZoom
        {
            get { return (double)GetValue(CurrentZoomProperty); }
            set { SetValue(CurrentZoomProperty, value); }
        }
    }
}
