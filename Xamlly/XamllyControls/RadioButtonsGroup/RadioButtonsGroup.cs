using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamlly.XamllyControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class RadioButtonsGroup : ContentView
    {
        readonly StackLayout radiosContainer;

        public RadioButtonsGroup()
        {
            radiosContainer = new StackLayout();
            Content = radiosContainer;
            HorizontalOptions = LayoutOptions.Start;
        }

        #region Bindable properties
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(RadioButtonsGroup), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnItemsSourceChanged);
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(RadioButtonsGroup), defaultValue: -1, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RadioButtonsGroup), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(RadioButtonsGroup), defaultValue: StackOrientation.Vertical);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RadioButtonsGroup), defaultValue: Color.Black);
        public static readonly BindableProperty RadioButtonColorProperty = BindableProperty.Create(nameof(RadioButtonColor), typeof(Color), typeof(RadioButtonsGroup), defaultValue: Color.Black);
        public static readonly BindableProperty DisplayMemberPathProperty = BindableProperty.Create(nameof(DisplayMemberPath), typeof(string), typeof(RadioButtonsGroup));
        public static readonly BindableProperty SelectedValuePathProperty = BindableProperty.Create(nameof(SelectedValuePath), typeof(string), typeof(RadioButtonsGroup));
        public static readonly BindableProperty SelectedValueProperty = BindableProperty.Create(nameof(SelectedValue), typeof(object), typeof(RadioButtonsGroup));
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(RadioButtonsGroup), defaultValue: FontAttributes.None);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(RadioButtonsGroup), defaultValue: default(double));
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(RadioButtonsGroup), defaultValue: default(string));

        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public StackOrientation Orientation
        {
            get => (StackOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        public Color RadioButtonColor
        {
            get => (Color)GetValue(RadioButtonColorProperty);
            set => SetValue(RadioButtonColorProperty, value);
        }
        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        public string SelectedValuePath
        {
            get => (string)GetValue(SelectedValuePathProperty);
            set => SetValue(SelectedValuePathProperty, value);
        }

        public object SelectedValue
        {
            get => GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
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

        #endregion

        public delegate void ItemsAddedHandler(object sender, ItemsAddedEventArgs e);
        public event ItemsAddedHandler OnItemsAdded;

        public delegate void SelectionChangedHandler(object sender, SelectionChangedEventArgs e);
        public event SelectionChangedHandler OnSelectionChanged;

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            var @this = bindable as RadioButtonsGroup;

            // unsubscribe from the old value

            if (oldValue is INotifyPropertyChanged oldNPC)
            {
                oldNPC.PropertyChanged -= @this.OnItemsSourcePropertyChanged;
            }

            if (oldValue is INotifyCollectionChanged oldNCC)
            {
                oldNCC.CollectionChanged -= @this.OnItemsSourceCollectionChanged;
            }

            // subscribe to the new value

            if (newValue is INotifyPropertyChanged newNPC)
            {
                newNPC.PropertyChanged += @this.OnItemsSourcePropertyChanged;
            }

            if (newValue is INotifyCollectionChanged newNCC)
            {
                newNCC.CollectionChanged += @this.OnItemsSourceCollectionChanged;
            }

            // inform the instance to do something

            @this.RebuildOnItemsSource();
        }

        private void RebuildOnItemsSource()
        {
            if (radiosContainer.Children.Count > 0)
                return;
            radiosContainer.Orientation = Orientation;
            radiosContainer.HorizontalOptions = Orientation == StackOrientation.Vertical ? LayoutOptions.Center : HorizontalOptions;
            radiosContainer.VerticalOptions = VerticalOptions;
            int i = 0;
            foreach (var item in ItemsSource)
            {
                var displayText = DisplayMemberPath == null ? item.ToString() : item.GetType().GetProperty(DisplayMemberPath).GetValue(item, null).ToString();
                RadioButton radioButton = new RadioButton
                {
                    RadioButtonColor = RadioButtonColor,
                    VerticalOptions = LayoutOptions.Center,
                    Text = displayText,
                    TextColor = TextColor,
                    FontFamily = FontFamily,
                    FontSize = FontSize,
                    FontAttributes = FontAttributes,
                    BindingContext = item
                };
                radioButton.SelectionChanged += RadioChecked;
                radiosContainer.Children.Add(radioButton);
                if (SelectedIndex == i)
                    radioButton.IsSelected = true;
                i++;
            }
        }


        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnItemsAdded?.Invoke(this, new ItemsAddedEventArgs(ItemsSource));
        }

        private void OnItemsSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnItemsAdded?.Invoke(this, new ItemsAddedEventArgs(ItemsSource));
        }

        private void RadioChecked(object sender, EventArgs e)
        {
            RadioButton currentRadio = (RadioButton)sender;
            var all = radiosContainer.Children.OfType<RadioButton>();

            foreach (var radio in all)
                radio.IsSelected = radio == currentRadio;

            SelectedItem = currentRadio.BindingContext;
            SelectedValue = SelectedValuePath == null ? null : SelectedItem.GetType().GetProperty(SelectedValuePath).GetValue(SelectedItem, null);
            SelectedIndex = radiosContainer.Children.IndexOf(currentRadio);

            OnSelectionChanged?.Invoke(this, new SelectionChangedEventArgs(SelectedItem, SelectedValue, SelectedIndex));
        }

    }
}
