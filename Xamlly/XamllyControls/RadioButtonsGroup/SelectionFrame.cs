using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamlly.XamllyControls
{
    public class SelectionFrame : ContentView
    {
        Frame fX;
        Frame fM;
        Frame fS;
        readonly double fSr;
        public SelectionFrame()
        {
            fSr = 2;
            const double fMr = 9;
            const double fXr = 11;
            fS = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Padding = 0,
                HasShadow = false,
                BorderColor = Color.Transparent,
                Margin = 0,
                CornerRadius = (float)fSr,
                HeightRequest = fSr * 2,
                WidthRequest = fSr * 2
            };

            fM = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                HasShadow = false,
                Padding = 0,
                Margin = 0,
                CornerRadius = (float)fMr,
                HeightRequest = fMr * 2,
                WidthRequest = fMr * 2,
                Content = fS
            };

            fX = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = BorderColor,
                HasShadow = false,
                Padding = new Thickness(0),
                CornerRadius = (float)fXr,
                HeightRequest = fXr * 2,
                WidthRequest = fXr * 2,
                Content = fM
            };
            fX.SetBinding(BackgroundColorProperty, new Binding(nameof(BorderColor), source: this));
            fX.BindingContext = this;
            Content = fX;
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(SelectionFrame), defaultValue: Color.Black);
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SelectionFrame), defaultValue: false, propertyChanged: SelectionChanged);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        private static void SelectionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var @this = bindable as SelectionFrame;
            bool chckd = (bool)newValue;
            if (chckd)
            {
                @this.fS = new Frame
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                    Padding = 0,
                    HasShadow = false,
                    BorderColor = Color.Transparent,
                    Margin = 0,
                    CornerRadius = (float)@this.fSr,
                    HeightRequest = @this.fSr * 2,
                    WidthRequest = @this.fSr * 2
                };
                @this.fM.Content = @this.fS;

                @this.fS.ScaleTo(3.5, length: 50, easing: Easing.SinIn);

                @this.fS.BackgroundColor = @this.BorderColor;
            }
            else
            {
                @this.fS.ScaleTo(0, length: 50, easing: Easing.SinOut);
            }
        }

    }
}
