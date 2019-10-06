using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamlly.XamllyControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToggleButton : ContentView
    {
        public event EventHandler<bool> SelectionChanged;
        public ToggleButton()
        {
            InitializeComponent();
            if (HeightRequest > 0)
                boxView.HeightRequest = HeightRequest / 10d;
        }
        
        #region Bindable Properties
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(ToggleButton),
defaultValue: default(string));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(ToggleButton),
defaultValue: Color.Default);

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(ToggleButton),
defaultValue: Color.Default);

        public Color UnselectedColor
        {
            get { return (Color)GetValue(UnselectedColorProperty); }
            set { SetValue(UnselectedColorProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ToggleButton),
defaultValue: Button.FontFamilyProperty.DefaultValue);

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ToggleButton),
defaultValue: Button.FontFamilyProperty.DefaultValue);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(ToggleButton), false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((ToggleButton)bindable).MutateSelect();
                }
            );

        #endregion

        void MutateSelect()
        {
            if (IsSelected)
            {
                label.TextColor = SelectedColor;
                boxView.Color = SelectedColor;
            }
            else
            {
                label.TextColor = UnselectedColor;
                boxView.Color = BackgroundColor;
            }
        }
        private void VerticalStack_Tapped(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
            SelectionChanged?.Invoke(this, IsSelected);
        }
    }
}