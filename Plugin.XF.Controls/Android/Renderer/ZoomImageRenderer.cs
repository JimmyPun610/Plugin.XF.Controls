using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading.Forms;
using FFImageLoading.Forms.Platform;
using Plugin.XF.Controls.Droid.Renderer;
using Plugin.XF.Controls.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ZoomImage), typeof(ZoomImageRenderer))]
namespace Plugin.XF.Controls.Droid.Renderer
{
    public class ZoomImageRenderer : CachedImageRenderer
    {
        private ZoomImage _zoomImage;
        private ScaleImageView _scaleImage;
        public static void Init() { }

        protected async override void OnElementChanged(ElementChangedEventArgs<CachedImage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _zoomImage = (ZoomImage)e.NewElement;

                // create the scale image and set it as the native control so it's available
                _scaleImage = new ScaleImageView(Context, null);
                _scaleImage.ZoomImage = _zoomImage;
                SetNativeControl(_scaleImage);
                await LoadImage();
            }
        }

        protected async override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ZoomImage.AspectProperty.PropertyName
                || e.PropertyName == ZoomImage.HeightProperty.PropertyName
                || e.PropertyName == ZoomImage.WidthProperty.PropertyName)
            {
                _scaleImage.ZoomToAspect();
            }
            else if (e.PropertyName == ZoomImage.SourceProperty.PropertyName)
            {
                await LoadImage();
                _scaleImage.ZoomToAspect();
            }
            else if (e.PropertyName == ZoomImage.CurrentZoomProperty.PropertyName)
            {
                _scaleImage.ZoomFromCurrentZoom();
            }
            else if (e.PropertyName == ZoomImage.MaxZoomProperty.PropertyName)
            {
                _scaleImage.UpdateMaxScaleFromZoomImage();
            }
            else if (e.PropertyName == ZoomImage.MinZoomProperty.PropertyName)
            {
                _scaleImage.UpdateMinScaleFromZoomImage();
            }
        }

        private async Task LoadImage()
        {
            var image = await (new ImageLoaderSourceHandler()).LoadImageAsync(_zoomImage.Source, Context);
            // would seem like the below would offer more flexibility, but actually is causing an exception
            //            if (image == null)
            //                image = await (new StreamImagesourceHandler()).LoadImageAsync(_zoomImage.Source, Context);
            if (image == null)
                image = await (new FileImageSourceHandler()).LoadImageAsync(_zoomImage.Source, Context);

            try
            {
                if (image != null && image.ByteCount > 0)
                    _scaleImage.SetImageBitmap(image);
            }
            catch (Exception e)
            {
                // catch an image loading failure
                Console.WriteLine($"Unable to load bitmap. Exception: {e.Message}");
            }
        }
    }
}