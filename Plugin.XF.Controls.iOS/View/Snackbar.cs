using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Plugin.XF.Controls.iOS.View
{
    public class SnackBar : NSObject
    {
        public float SnackbarHeight = 65;
        public UIColor BackgroundColor = UIColor.DarkGray;
        public UIColor TextColor = UIColor.White;
        public UIColor ButtonColor = UIColor.Cyan;
        public UIColor ButtonColorPressed = UIColor.Gray;
        public double AnimateDuration = 0.5;
        //private variables
        private UIWindow window = UIApplication.SharedApplication.KeyWindow;
        private UIView snackbarView = null;
        private UILabel txt = new UILabel();
        private UIButton btn = new UIButton();
        private Action action = null;
        public nfloat Alpha = 100;
        NSTimer timer = null;
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private bool isShowing
        {
            get { 
                return snackbarView != null; 
            }
        }
        private static SnackBar _instance { get; set; }
        public static SnackBar Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SnackBar();
                return _instance;
            }
        }

        public SnackBar()
        {
            NSNotificationCenter.DefaultCenter.AddObserver(Self, new ObjCRuntime.Selector("rotate"), UIDevice.OrientationDidChangeNotification, null);
            txt.LineBreakMode = UILineBreakMode.WordWrap;
            txt.Lines = 0;
        }

        /// Show snackbar with text and button
        public void ShowSnackBar(string text, int duration = 3, string actionTitle = "", Action action = null)
        {
            if (isShowing)
                hideNow();
            //Unblock thread
            autoResetEvent.Set();
            new Thread(() =>
            {
                autoResetEvent.WaitOne();
                autoResetEvent.Reset();
                Device.BeginInvokeOnMainThread(() =>
                {
                    snackbarView = new UIView();

                    txt.Text = text;
                    txt.TextColor = TextColor;
                    


                    
                    if (action == null)
                        action = () => hide();
                    float margin = 16;

                    if (!string.IsNullOrEmpty(actionTitle))
                    {
                        this.action = action;
                        btn.SetTitleColor(ButtonColor, UIControlState.Normal);
                        btn.SetTitleColor(UIColor.Gray, UIControlState.Highlighted);
                        btn.SetTitle(actionTitle, UIControlState.Normal);
                        
                        btn.Frame = new CGRect(window.Frame.Width - btn.IntrinsicContentSize.Width - margin, 0,
                            btn.IntrinsicContentSize.Width, SnackbarHeight);
                        btn.AddTarget(Self, new ObjCRuntime.Selector("actionButtonPress"), UIControlEvent.TouchUpInside);
                        txt.Frame = new CGRect(margin, 0, window.Frame.Width - btn.IntrinsicContentSize.Width - margin * 2, SnackbarHeight);
                        snackbarView.AddSubview(btn);
                    }
                    else
                    {
                        txt.Frame = new CGRect(margin, 0, window.Frame.Width - margin * 2, SnackbarHeight);
                    }


                    CGSize maxSize = new CGSize(txt.Frame.Width, float.MaxValue);
                    NSString t = (NSString)text;
                    var textHeight = t.GetBoundingRect(maxSize, NSStringDrawingOptions.UsesLineFragmentOrigin, new UIStringAttributes
                    {
                        Font = txt.Font
                    }, null).Height;
                    SnackbarHeight = (float)textHeight;

                    txt.Frame = new CGRect(txt.Frame.X, txt.Frame.Y, txt.Frame.Width, textHeight);
                    btn.Frame = new CGRect(btn.Frame.X, btn.Frame.Y, btn.Frame.Width, textHeight);
                    float y = (float)window.Bounds.Size.Height - SnackbarHeight - (float)window.SafeAreaInsets.Bottom;
                    snackbarView.Frame = new CGRect(0, y, window.Frame.Width, SnackbarHeight + (float)window.SafeAreaInsets.Bottom);
                    snackbarView.BackgroundColor = this.BackgroundColor;
                    snackbarView.Alpha = Alpha;
                    snackbarView.AddSubview(txt);
                    show(duration);
                });
            }).Start();
        }


        private void show(int duration = 3)
        {
            animateBar(duration);
        }

        private void animateBar(int timerLength)
        {
            
            UIView.Animate(AnimateDuration, () =>
            {
                window.AddSubview(snackbarView);
            }, () =>
            {
                if (timer != null)
                {
                    timer.Invalidate();
                    timer = null;
                }
                timer = NSTimer.CreateScheduledTimer(timerLength, false, (NSTimer obj) =>
                {
                    hide();
                });
            });
        }

        [Foundation.Export("rotate")]
        private void rotate()
        {
            if(snackbarView != null)
            {
                float y = (float)window.Bounds.Size.Height - SnackbarHeight - (float)window.SafeAreaInsets.Bottom;
                snackbarView.Frame = new CGRect(0, y, window.Frame.Width, SnackbarHeight + window.SafeAreaInsets.Bottom);
                btn.Frame = new CGRect(window.Frame.Width * 73 / 100, 0, window.Frame.Width * 25 / 100, SnackbarHeight);
            }
            
        }

        [Foundation.Export("actionButtonPress")]
        private void actionButtonPress()
        {
            if (action != null)
                action.Invoke();
            hide();
        }

        [Foundation.Export("hide")]
        private void hide()
        {

            UIView.Animate(AnimateDuration, () => {
                if(snackbarView != null)
                    snackbarView.Alpha = nfloat.Parse("0");
            }, () =>
            {
                if (snackbarView != null)
                {
                    snackbarView.RemoveFromSuperview();
                    snackbarView = null;
                }
                autoResetEvent.Set();
            });
        }
        private void hideNow()
        {
            if (snackbarView != null)
            {
                snackbarView.RemoveFromSuperview();
                snackbarView = null;
            }
            autoResetEvent.Set();
        }
     
    }
}