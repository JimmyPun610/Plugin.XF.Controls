using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.XF.Controls.Controls;
using Plugin.XF.Controls.iOS.Renderer;

[assembly: ExportRenderer(typeof(ZoomImage), typeof(ZoomImageRenderer))]

namespace Plugin.XF.Controls.iOS.Renderer
{
    public class ZoomImageRenderer : ViewRenderer<ZoomImage, UIScrollView>
    {
        private ZoomImage _zoomImage;
        private UIScrollView _scrollView;
        private UIImageView _imageView;
        private nfloat _baseScalingFactor;
        private bool doubleTapped;
        public static void Init() { }
        protected override async void OnElementChanged(ElementChangedEventArgs<ZoomImage> e)
        {
            if (this.Control == null && e.NewElement != null)
            {
                // setup the control to be a scroll view with an image in it
                _zoomImage = e.NewElement;

                // create the scroll view
                _scrollView = new UIScrollView
                {
                    ClipsToBounds = true,
                    BackgroundColor = UIColor.Red,
                    ContentMode = _zoomImage.Aspect.ToUIViewContentMode(),
                    ScrollEnabled = _zoomImage.ScrollEnabled
                };
                // add the image view to it
                await AssignImageAsync();
                _scrollView.AddSubview(_imageView);
               
                // setup the zooming and double tap
                _scrollView.ViewForZoomingInScrollView += (view) => _imageView;
                _scrollView.AddGestureRecognizer(
                    new UITapGestureRecognizer((gest) =>
                    {
                        if (_zoomImage.DoubleTapToZoomEnabled)
                        {
                            if (!doubleTapped)
                            {
                                var location = gest.LocationOfTouch(0, _scrollView);
                                //_scrollView.ZoomToRect(GenerateZoomRect((float)_zoomImage.TapZoomScale, location), true);
                                //var zoomRect = ZoomRectForScale((float)_zoomImage.TapZoomScale, _scrollView, location);
                                //_scrollView.ZoomToRect(zoomRect, true);
                                //_scrollView.ContentSize = _imageView.Frame.Size;

                                ZoomAndMoveToPoint(location, _scrollView, _imageView, (nfloat)_zoomImage.TapZoomScale);
                            }
                            else
                            {
                                SetZoomToAspect();
                                CenterImage();
                            }
                            doubleTapped = !doubleTapped;
                        }
                    })
                    { NumberOfTapsRequired = 2 }
                );
                _scrollView.PinchGestureRecognizer.Enabled = _zoomImage.ZoomEnabled;

                this.SetNativeControl(_scrollView);
                CenterImage();
            }
            SetNeedsDisplay();
            base.OnElementChanged(e);
        }
        private void CenterImage()
        {
            var imageViewSize = _imageView.Frame.Size;
            var scrollViewSize = _scrollView.Frame.Size;
            var verticalPadding = imageViewSize.Height < scrollViewSize.Height ? (scrollViewSize.Height - imageViewSize.Height) / 2 : 0;
            var horizontalPadding = imageViewSize.Width < scrollViewSize.Width ? (scrollViewSize.Width - imageViewSize.Width) / 2 : 0;
            _scrollView.ContentInset = new UIEdgeInsets(verticalPadding, horizontalPadding, verticalPadding, horizontalPadding);
        }
        private async Task AssignImageAsync()
        {
            // reset the scroll or the size and offsets will all be off for the new image (do this before updating the image)
            ResetScrollView();

            var webSource = _zoomImage.Source as UriImageSource;
            var fileSource = _zoomImage.Source as FileImageSource;
            var streamSource = _zoomImage.Source as StreamImageSource;
            if (webSource != null)
            {
                try
                {
                    var url = webSource.Uri.ToString();
                    using (var data = NSData.FromUrl(NSUrl.FromString(url)))
                    using (var image = UIImage.LoadFromData(data))
                    {
                        if (_imageView == null)
                            _imageView = new UIImageView(image);
                        else
                            _imageView.Image = image;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading image from {webSource.Uri.ToString()}. Exception: {e.Message}");
                    _imageView = new UIImageView();
                }
            }
            else if (fileSource != null)
            {
                try
                {
                    // use a file source
                    using (var image = UIImage.FromFile(fileSource.File))
                    {
                        if (_imageView == null)
                            _imageView = new UIImageView(image);
                        else
                            _imageView.Image = image;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading image from file {fileSource.File}. Exception: {e.Message}");
                    _imageView = new UIImageView();
                }
            }
            else if (streamSource != null)
            {
                try
                {
                    var cts = new CancellationTokenSource();
                    using (var stream = await streamSource.Stream(cts.Token))
                    using (var data = NSData.FromStream(stream))
                    using (var image = UIImage.LoadFromData(data))
                    {
                        if (_imageView == null)
                            _imageView = new UIImageView(image);
                        else
                            _imageView.Image = image;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading image from stream. Exception: {e.Message}");
                    _imageView = new UIImageView();
                }
            }
            else
            {
                Debug.WriteLine("ZoomImageRenderer: Unable to load image, creating empty image view.");
                if (_imageView == null)
                    _imageView = new UIImageView();
                else
                    _imageView.Image = null;
            }

            _imageView.SizeToFit();
            _scrollView.ContentSize = _imageView.Frame.Size;
        }

        private void ResetScrollView()
        {
            _scrollView.ContentOffset = new CGPoint(0, 0);
            _scrollView.ContentInset = new UIEdgeInsets(0, 0, 0, 0);
            // the min and max don't really matter, they will be reset, just need 1.0 to be in the range so that
            // when the new image is set the sizing is correct.
            _scrollView.MinimumZoomScale = 1.0f;
            _scrollView.MaximumZoomScale = 2.0f;
            _scrollView.ZoomScale = 1.0f;
        }

        private void SetZoomToAspect(bool reapplyCurrentScale = false)
        {
            // the min and max zoom provided by the zoom control will be based on whatever initial scale is determined here
            // so 10X max will be 10 x original zoom and similiarly for min zoom

            if (_scrollView == null || _imageView == null || _imageView.Image == null)
                return;

            // if the scroll view doesn't have any size, just exit
            if (_scrollView.Frame.Width == 0 || _scrollView.Frame.Height == 0)
                return;

            if (_baseScalingFactor == 0)
                reapplyCurrentScale = false;

            // if reapplying the current scale, hold on to what it currently is without the base scaling factor (which may change)
            nfloat oldScale = 0;
            if (reapplyCurrentScale)
                oldScale = _scrollView.ZoomScale / _baseScalingFactor;

            // get the scale for each dimension
            var wScale = _scrollView.Frame.Width / _imageView.Image.Size.Width;
            var hScale = _scrollView.Frame.Height / _imageView.Image.Size.Height;

            // determine the base scaling factor to use based on the requested aspect
            switch (_zoomImage.Aspect)
            {
                case Aspect.AspectFill:
                case Aspect.Fill:
                    // fill the view, so scale to the larger of the two scales
                    _baseScalingFactor = (nfloat)Math.Max(wScale, hScale);
                    break;
                default:
                    // fit the full image, so scale to the smaller of the two scales
                    _baseScalingFactor = (nfloat)Math.Min(wScale, hScale);
                    break;
            }

            // assign the min and max zooms based on the user request and base scaling factor
            _scrollView.MinimumZoomScale = (nfloat)_zoomImage.MinZoom * _baseScalingFactor;
            _scrollView.MaximumZoomScale = (nfloat)_zoomImage.MaxZoom * _baseScalingFactor;

            // center image when filling the screen
            var widthDiff = (_imageView.Bounds.Width * _baseScalingFactor) - _scrollView.Bounds.Width;
            var heightDiff = (_imageView.Bounds.Height * _baseScalingFactor) - _scrollView.Bounds.Height;
            var widthOffset = Math.Max(widthDiff / 2, 0);
            var heightOffset = Math.Max(heightDiff / 2, 0);
            // if offset is 0, apply it immediately - it won't change and this allows the inset to be correct
            // logically this is == 0, but since using floats there are warnings about rounding errors
            if (widthOffset < 0.1 && heightOffset < 0.1)
                _scrollView.ContentOffset = new CGPoint(0, 0);


            // center the image 
            CenterImage();

            // set the current scale
            if (reapplyCurrentScale)
                _scrollView.SetZoomScale(oldScale * _baseScalingFactor, true);
            else
                _scrollView.SetZoomScale(_baseScalingFactor, true);

            // if non-zero offset, apply that animated (so it completes after the zoom)
            if (widthOffset > 0 || heightOffset > 0)
                _scrollView.SetContentOffset(new CGPoint(widthOffset, heightOffset), true);

            // updating the zoom scale resets the pinch gesture recognizer, so set it back to the current zoom enabled state
            _scrollView.PinchGestureRecognizer.Enabled = _zoomImage.ZoomEnabled;
        }

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);

                if (e.PropertyName == ZoomImage.AspectProperty.PropertyName)
                {
                    SetZoomToAspect();
                }
                else if (e.PropertyName == ZoomImage.CurrentZoomProperty.PropertyName)
                {
                    var scale = (nfloat)_zoomImage.Scale * _baseScalingFactor;
                    _scrollView.SetZoomScale(scale, true);
                }
                else if (e.PropertyName == ZoomImage.HeightProperty.PropertyName
                    || e.PropertyName == ZoomImage.WidthProperty.PropertyName)
                {
                    await Task.Delay(50); // give a short delay for changes to be applied to the frame
                    SetZoomToAspect(true); // reapply the current scale
                    SetNeedsDisplay();
                }
                else if (e.PropertyName == ZoomImage.MaxZoomProperty.PropertyName)
                {
                    _scrollView.MaximumZoomScale = (nfloat)_zoomImage.MaxZoom * _baseScalingFactor;
                }
                else if (e.PropertyName == ZoomImage.MinZoomProperty.PropertyName)
                {
                    _scrollView.MaximumZoomScale = (nfloat)_zoomImage.MinZoom * _baseScalingFactor;
                }
                else if (e.PropertyName == ZoomImage.ScrollEnabledProperty.PropertyName)
                {
                    _scrollView.ScrollEnabled = _zoomImage.ScrollEnabled;
                }
                else if (e.PropertyName == ZoomImage.SourceProperty.PropertyName)
                {
                    await AssignImageAsync();
                    SetZoomToAspect();
                    SetNeedsDisplay();
                }
                else if (e.PropertyName == ZoomImage.ZoomEnabledProperty.PropertyName)
                {
                    _scrollView.PinchGestureRecognizer.Enabled = _zoomImage.ZoomEnabled;
                    // if zoom is disabled, return to aspect view
                    if (!_zoomImage.ZoomEnabled)
                        SetZoomToAspect();
                }
            }
            catch (Exception ex)
            {
                // nothing we can really do here, but will catch it because it can be difficult
                // with bindings for the caller to catch
                Debug.WriteLine($"ZoomImageRenderer: Error: {ex.Message}\nStack: {ex.StackTrace}");
            }
        }

        //private CGRect GenerateZoomRect(UIScrollView scrollView, float scaleFactor, CGPoint point)
        //{
        //    nfloat scale;
        //    if (scrollView.ZoomScale < scrollView.MaximumZoomScale)
        //    {
        //        // not at max zoom so zoom in
        //        scale = (nfloat)Math.Min(scrollView.ZoomScale * scaleFactor, scrollView.MaximumZoomScale);
        //    }
        //    else
        //    {
        //        // already at max zoom so zoom out
        //        scale = (nfloat)Math.Max(scrollView.ZoomScale / scaleFactor, scrollView.MinimumZoomScale);
        //    }

        //    // note that the point location is from the top left of the image and is measured in the scaled size
        //    CGRect zoomRect = new CGRect
        //    {
        //        Height = scrollView.Frame.Height / scale,
        //        Width = scrollView.Frame.Width / scale,
        //        X = (point.X / scrollView.ZoomScale) - (scrollView.Frame.Width / (scale * 2.0f)), // half the width
        //        Y = (point.Y / scrollView.ZoomScale) - (scrollView.Frame.Height / (scale * 2.0f)) // half the height
        //    };

        //    return zoomRect;
        //}


        public CGRect ZoomRectForScale(nfloat scale, UIScrollView scrollView, CGPoint center)
        {
            CGRect zoomRect = new CGRect();
            zoomRect.Height = (scrollView.Frame.Height / scale);
            zoomRect.Width = (scrollView.Frame.Width / scale);

            //zoomRect.X = (nfloat)(center.X - (zoomRect.Width * scale));
            //zoomRect.Y = (nfloat)(center.Y - (zoomRect.Height * scale));
            zoomRect.X = (nfloat)(center.X * scale);
            zoomRect.Y = (nfloat)(center.Y * scale);

            return zoomRect;
        }

        public void ZoomAndMoveToPoint(CGPoint location, UIScrollView scrollView, UIImageView uIImageView, nfloat scale)
        {
            nfloat zoomScaleBefore = scrollView.ZoomScale;
            if (location.X <= 0) location.X = 1;
            if (location.Y <= 0) location.Y = 1;
            if (location.X >= uIImageView.Bounds.Size.Width) location.X = uIImageView.Bounds.Size.Width - 1;
            if (location.Y >= uIImageView.Bounds.Size.Height) location.Y = uIImageView.Bounds.Size.Height - 1;

            //nfloat percentX = location.X / uIImageView.Bounds.Size.Width;
            //nfloat percentY = location.Y / uIImageView.Bounds.Size.Height;

            scrollView.SetZoomScale(scale, true);

            nfloat posX = (nfloat)(location.X * scale);
            nfloat posY = (nfloat)(location.Y * scale);
            //nfloat posX = (percentX * scrollView.ContentSize.Width) - (scrollView.Frame.Size.Width / 2);
            //nfloat posY = (percentY * scrollView.ContentSize.Height) - (scrollView.Frame.Size.Height / 2);
            if (posX <= 0) posX = 1;
            if (posY <= 0) posY = 1;
            scrollView.ScrollRectToVisible(new CGRect(posX, posY, scrollView.Frame.Size.Width, scrollView.Frame.Size.Height), scale == zoomScaleBefore);
        }
    }
}