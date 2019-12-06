using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plugin.XF.Controls.Shared.Controls
{
    /// <summary>
    /// This is a ContentView combined with Entry and Label, it also support Material Visual.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnotatedEntry : ContentView
    {
        public AnnotatedEntry()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        public void SetAnnotation(bool isShown, string message, Color color)
        {
            Annotation = message;
            IsShownAnnotation = isShown;
            AnnotationColor = color;
        }

        public void HideAnnotation()
        {
            IsShownAnnotation = false;
        }

        public static readonly BindableProperty EntryProperty =
   BindableProperty.Create<AnnotatedEntry, Entry>(p => p.Entry, null);

        public Entry Entry
        {
            get { return (Entry)GetValue(EntryProperty); }
            set { SetValue(EntryProperty, value); }
        }

        public static readonly BindableProperty AnnotationColorProperty =
BindableProperty.Create<AnnotatedEntry, Color>(p => p.AnnotationColor, Color.Black);

        public Color AnnotationColor
        {
            get { return (Color)GetValue(AnnotationColorProperty); }
            set { SetValue(AnnotationColorProperty, value); }
        }

        public static readonly BindableProperty AnnotationProperty =
BindableProperty.Create<AnnotatedEntry, string>(p => p.Annotation, null);

        public string Annotation
        {
            get { return (string)GetValue(AnnotationProperty); }
            set { SetValue(AnnotationProperty, value); }
        }

        public static readonly BindableProperty IsShownAnnotationProperty =
BindableProperty.Create<AnnotatedEntry, bool>(p => p.IsShownAnnotation, false);

        public bool IsShownAnnotation
        {
            get { return (bool)GetValue(IsShownAnnotationProperty); }
            set { SetValue(IsShownAnnotationProperty, value); }
        }
    }
}