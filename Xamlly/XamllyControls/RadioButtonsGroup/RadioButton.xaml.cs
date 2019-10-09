using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamlly.XamllyControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButton : ContentView
    {
        public RadioButton()
        {
            InitializeComponent();
        }
        public event EventHandler SelectionChanged;

        #region Bindable Properties
        public static readonly BindableProperty RadioButtonColorProperty = BindableProperty.Create(nameof(RadioButtonColor), typeof(Color), typeof(RadioButton), defaultValue: Color.Black);
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(RadioButton), defaultValue: false, propertyChanged: IsSelectedChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioButton));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RadioButton), defaultValue: Color.Default);
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(RadioButton), defaultValue: Label.FontAttributesProperty.DefaultValue);
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(RadioButton), defaultValue: Label.FontFamilyProperty.DefaultValue);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(RadioButton), defaultValue: Label.FontSizeProperty.DefaultValue);

        public Color RadioButtonColor
        {
            get => (Color)GetValue(RadioButtonColorProperty);
            set => SetValue(RadioButtonColorProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        } 
        #endregion

        private static void IsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RadioButton)bindable).SetSelection();
        }

        private void Radio_Tapped(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetSelection()
        {
            radio.ScaleTo(IsSelected ? 1 : 0, easing: Easing.CubicIn, length: 100);
        }
    }
}