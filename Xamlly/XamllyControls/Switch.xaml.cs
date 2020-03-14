using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamlly.XamllyControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Switch : ContentView
    {
        public event EventHandler Switched;
        private static readonly Color lightGray = Color.FromHex("E0E0E0");
        private static readonly Color darkGray = Color.FromHex("C5C5C5");
        private bool setAppearancePending;
        private double buttonFrameWidth;
        private double containerWidth;

        public Switch()
        {
            InitializeComponent();
            buttonFrame.SizeChanged += ButtonFrame_SizeChanged;
            container.SizeChanged += Container_SizeChanged;
        }

        //this is called right after ButtonFrame_SizeChanged (buttonFrameWidth already initialized)
        private void Container_SizeChanged(object sender, EventArgs e)
        {
            containerWidth = ((Frame)sender).Width;

            if (containerWidth > 0 && buttonFrameWidth > 0 && setAppearancePending)
                SetAppearance();
        }

        private void ButtonFrame_SizeChanged(object sender, EventArgs e)
        {
            buttonFrameWidth = ((Frame)sender).Width;
        }
        #region Bindable properties

        public static readonly BindableProperty IsOnProperty = BindableProperty.Create(nameof(IsOn),
          typeof(bool), typeof(Switch), defaultValue: false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: IsOnChanged);

        public static readonly BindableProperty OnTextProperty = BindableProperty.Create(nameof(OnText),
          typeof(string), typeof(Switch), defaultValue: string.Empty, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty OffTextProperty = BindableProperty.Create(nameof(OffText),
            typeof(string), typeof(Switch), defaultValue: string.Empty, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor),
            typeof(Color), typeof(Switch), defaultValue: Color.White);

        public static readonly BindableProperty OnColorProperty = BindableProperty.Create(nameof(OnColor),
            typeof(Color), typeof(Switch), defaultValue: Color.CadetBlue);

        public static readonly BindableProperty OffColorProperty = BindableProperty.Create(nameof(OffColor),
         typeof(Color), typeof(Switch), defaultValue: lightGray);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
       typeof(float), typeof(Switch), defaultValue: 0f);

        public static readonly BindableProperty ButtonWidthProperty = BindableProperty.Create(nameof(ButtonWidth),
       typeof(float), typeof(Switch), defaultValue: 50f);

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor),
    typeof(Color), typeof(Switch), darkGray);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize),
  typeof(double), typeof(Switch), Label.FontSizeProperty.DefaultValue);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily),
  typeof(string), typeof(Switch), Label.FontFamilyProperty.DefaultValue);


        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes),
  typeof(FontAttributes), typeof(Switch), Label.FontAttributesProperty.DefaultValue);

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public bool IsOn
        {
            get => (bool)GetValue(IsOnProperty);
            set => SetValue(IsOnProperty, value);
        }
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public float ButtonWidth
        {
            get => (float)GetValue(ButtonWidthProperty);
            set => SetValue(ButtonWidthProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }
        public string OnText
        {
            get => (string)GetValue(OnTextProperty);
            set => SetValue(OnTextProperty, value);
        }

        public string OffText
        {
            get => (string)GetValue(OffTextProperty);
            set => SetValue(OffTextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public Color OnColor
        {
            get => (Color)GetValue(OnColorProperty);
            set => SetValue(OnColorProperty, value);
        }
        public Color OffColor
        {
            get => (Color)GetValue(OffColorProperty);
            set => SetValue(OffColorProperty, value);
        }
        #endregion
        private static void IsOnChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((Switch)bindable).SetAppearance();
        }
        private void ToggleSwitch(object sender, EventArgs e)
        {
            IsOn = !IsOn;
            Switched?.Invoke(this, EventArgs.Empty);
        }
        private void SetAppearance()
        {
            if (IsOn)//make it on
            {
                if (containerWidth <= 0 || buttonFrameWidth <= 0)
                {
                    setAppearancePending = true;
                    return;
                }
                buttonFrame.TranslateTo(containerWidth - buttonFrameWidth, 0, easing: Easing.CubicOut);
                buttonFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("OnColor", source: this));
                switchLabel.SetBinding(Label.TextProperty, new Binding("OnText", source: this, mode: BindingMode.TwoWay));
            }
            else//make it off
            {
                buttonFrame.TranslateTo(0, 0, easing: Easing.CubicOut);
                buttonFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("OffColor", source: this));
                switchLabel.SetBinding(Label.TextProperty, new Binding("OffText", source: this, mode: BindingMode.TwoWay));
            }
        }
    }
}