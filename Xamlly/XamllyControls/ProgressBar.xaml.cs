using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamlly.Extensions;

namespace Xamlly.XamllyControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressBar : ContentView
    {
        public event EventHandler ProgressChanged;
        public ProgressBar()
        {
            InitializeComponent();
            container.LowerChild(frameBorder);
            frameProgress.SetBinding(RelativeLayout.WidthConstraintProperty, new Binding(nameof(Progress), source: this));
            frameBorder.SetBinding(VisualElement.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
            Padding = new Thickness(3);
        }

        #region Bindable properties
        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public static readonly BindableProperty ImageProperty =
           BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(ProgressBar), null);

        public Color ProgressColor
        {
            get => (Color)GetValue(ProgressColorProperty);
            set => SetValue(ProgressColorProperty, value);
        }

        public static readonly BindableProperty ProgressColorProperty =
            BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(ProgressBar), null);

        public Color ProgressTextColor
        {
            get => (Color)GetValue(ProgressTextColorProperty);
            set => SetValue(ProgressTextColorProperty, value);
        }

        public static readonly BindableProperty ProgressTextColorProperty =
            BindableProperty.Create(nameof(ProgressTextColor), typeof(Color), typeof(ProgressBar), null);


        public Color OutlineColor
        {
            get => (Color)GetValue(OutlineColorProperty);
            set => SetValue(OutlineColorProperty, value);
        }

        public static readonly BindableProperty OutlineColorProperty =
            BindableProperty.Create(nameof(OutlineColor), typeof(Color), typeof(ProgressBar), null);

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(ProgressBar), 0f);

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor),
    typeof(Color), typeof(ProgressBar), Color.Default);
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(ProgressBar), 0d,
                coerceValue: (bindable, value) =>
                 ((double)value).Clamp(0, 1),
                propertyChanged: ProgressPropertyChanged);

        #endregion
        private static void ProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ProgressBar)bindable;
            double oldProgress = (double)oldValue;
            double newProgress = (double)newValue;
            control.SetProgress(oldProgress, newProgress);
            control.ProgressChanged?.Invoke(control, EventArgs.Empty);
        }

        private void SetProgress(double oldProgress, double newProgress)
        {
            var progressAnimation = new Animation(
                (value) =>
                {
                    RelativeLayout.SetWidthConstraint(frameProgress, Constraint.RelativeToParent((rl) => rl.Width * value));
                    percentageText.Text = $"{value:P0}";
                },
                 start: oldProgress,
                 end: newProgress
            );
            frameProgress.Animate("anim", progressAnimation, length: 500, easing: Easing.CubicOut);
        }
    }
}