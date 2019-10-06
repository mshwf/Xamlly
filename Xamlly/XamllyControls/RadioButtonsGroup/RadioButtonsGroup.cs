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
        StackLayout parentStack;
        List<SelectionFrame> lbRadios;

        public RadioButtonsGroup()
        {
            parentStack = new StackLayout();
            Content = parentStack;
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(RadioButtonsGroup), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnItemsSourceChanged);
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(RadioButtonsGroup), defaultValue: -1, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RadioButtonsGroup), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(RadioButtonsGroup), defaultValue: StackOrientation.Vertical);
        public static readonly BindableProperty FrontColorProperty = BindableProperty.Create(nameof(FrontColor), typeof(Color), typeof(RadioButtonsGroup), defaultValue: Color.Black);
        public static readonly BindableProperty DirectionProperty = BindableProperty.Create(nameof(Direction), typeof(Directions), typeof(RadioButtonsGroup), defaultValue: Directions.LTR);
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

        public Color FrontColor
        {
            get => (Color)GetValue(FrontColorProperty);
            set => SetValue(FrontColorProperty, value);
        }

        public Directions Direction
        {
            get => (Directions)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
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

            var oldNPC = oldValue as INotifyPropertyChanged;
            if (oldNPC != null)
            {
                oldNPC.PropertyChanged -= @this.OnItemsSourcePropertyChanged;
            }

            var oldNCC = oldValue as INotifyCollectionChanged;
            if (oldNCC != null)
            {
                oldNCC.CollectionChanged -= @this.OnItemsSourceCollectionChanged;
            }

            // subscribe to the new value

            var newNPC = newValue as System.ComponentModel.INotifyPropertyChanged;
            if (newNPC != null)
            {
                newNPC.PropertyChanged += @this.OnItemsSourcePropertyChanged;
            }

            var newNCC = newValue as INotifyCollectionChanged;
            if (newNCC != null)
            {
                newNCC.CollectionChanged += @this.OnItemsSourceCollectionChanged;
            }

            // inform the instance to do something

            @this.RebuildOnItemsSource();
        }

        private void RebuildOnItemsSource()
        {
            if (parentStack.Children.Count > 0)
                return;
            parentStack.Orientation = Orientation;
            parentStack.HorizontalOptions = Orientation == StackOrientation.Vertical ? LayoutOptions.Center : HorizontalOptions;
            parentStack.VerticalOptions = VerticalOptions;

            var items = ItemsSource;
            if (Orientation == StackOrientation.Horizontal && Direction == Directions.RTL)
            {
                items = items.Reverse();
            }

            foreach (var item in items)
            {
                StackLayout radio = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    BindingContext = item,
                    HorizontalOptions = Orientation == StackOrientation.Horizontal ? LayoutOptions.CenterAndExpand : LayoutOptions.Fill,
                };
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += RadioChecked;
                radio.GestureRecognizers.Add(tap);

                var displayText = DisplayMemberPath == null ? item.ToString() : item.GetType().GetProperty(DisplayMemberPath).GetValue(item, null).ToString();

                Label radioText = new Label
                {
                    Text = displayText,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = FrontColor,
                    FontAttributes = FontAttributes,
                    FontSize = FontSize,
                    FontFamily = FontFamily
                };
                SelectionFrame circle = new SelectionFrame { ClassId = "r", BorderColor = FrontColor, VerticalOptions = LayoutOptions.Center };
                InitDirection(radio, radioText, circle);
            }

            lbRadios = parentStack.Children.Where(x => x is StackLayout).SelectMany(x => ((StackLayout)x).Children.Where(l => l.ClassId == "r").Cast<SelectionFrame>()).ToList();
            if (Orientation == StackOrientation.Horizontal && Direction == Directions.RTL)
                lbRadios.Reverse();
            if (SelectedIndex >= 0)
            {
                try
                {
                    lbRadios[SelectedIndex].IsSelected = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    SetValue(SelectedIndexProperty, 0);
                    lbRadios[SelectedIndex].IsSelected = true;
                }
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

        private void InitDirection(StackLayout radio, Label radioText, SelectionFrame radioCircle)
        {
            if (Direction == Directions.RTL)
            {
                radioText.HorizontalOptions = LayoutOptions.EndAndExpand;
                radioCircle.HorizontalOptions = LayoutOptions.StartAndExpand;
                radio.Children.Add(radioCircle);
                radio.Children.Add(radioText);
            }
            else
            {
                radioCircle.HorizontalOptions = LayoutOptions.EndAndExpand;
                radio.Children.Add(radioText);
                radio.Children.Add(radioCircle);
            }
            parentStack.Children.Add(radio);
        }

        private void RadioChecked(object sender, EventArgs e)
        {
            StackLayout stRadio = (StackLayout)sender;
            var lb = stRadio.Children.First(x => x.ClassId == "r") as SelectionFrame;
            if (lb is null)
                return;
            if (!lb.IsSelected)
            {
                if (SelectedIndex >= 0)
                    lbRadios.Single(x => x.IsSelected).IsSelected = false;
                lb.IsSelected = true;
                SelectedItem = stRadio.BindingContext;
                SelectedValue = SelectedValuePath == null ? null : SelectedItem.GetType().GetProperty(SelectedValuePath).GetValue(SelectedItem, null);
                SelectedIndex = ItemsSource.ToList().IndexOf(SelectedItem);
                OnSelectionChanged?.Invoke(this, new SelectionChangedEventArgs(SelectedItem, SelectedValue, SelectedIndex));
            }
        }

    }
}
