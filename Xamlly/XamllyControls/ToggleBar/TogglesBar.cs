using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.Forms;

namespace Xamlly.XamllyControls
{

    public class TogglesBarSelectionChangedEventArgs : EventArgs
    {
        public object SelectedItems { get; set; }
        public object SelectedIndices { get; set; }
    }

    public class TogglesBar : ContentView
    {
        ScrollView scrollContainer;
        StackLayout stackContainer;
        public event EventHandler<TogglesBarSelectionChangedEventArgs> SelectedItemsChanged;

        public TogglesBar()
        {
            scrollContainer = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never
            };

            stackContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Margin = 0,
                Padding = 0
            };
            scrollContainer.Content = stackContainer;
            Content = scrollContainer;
        }

        public int InitialIndex { get; set; } = -1;

        #region Bindable Properties
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(TogglesBar),
          defaultBindingMode: BindingMode.TwoWay, propertyChanged: CustomPropertyChanging);

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(object), typeof(TogglesBar),
  defaultBindingMode: BindingMode.TwoWay);
        public object SelectedItems
        {
            get { return GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly BindableProperty DisplayMemberPathProperty = BindableProperty.Create(nameof(DisplayMemberPath), typeof(string), typeof(TogglesBar),
            defaultBindingMode: BindingMode.OneTime,
            defaultValue: default(string),
            propertyChanged: CustomPropertyChanging);

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly BindableProperty ItemsSpacingProperty =
        BindableProperty.Create(nameof(ItemsSpacing), typeof(double), typeof(TogglesBar), 0d);

        public double ItemsSpacing
        {
            get { return (double)GetValue(ItemsSpacingProperty); }
            set { SetValue(ItemsSpacingProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(TogglesBar), Button.FontFamilyProperty.DefaultValue);

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty IsMultiSelectProperty =
            BindableProperty.Create(nameof(IsMultiSelect), typeof(bool), typeof(TogglesBar), false);

        public bool IsMultiSelect
        {
            get { return (bool)GetValue(IsMultiSelectProperty); }
            set { SetValue(IsMultiSelectProperty, value); }
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(TogglesBar),
            defaultValue: Color.Black, propertyChanged: CustomPropertyChanging);

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(TogglesBar),
            defaultValue: Color.Gray, propertyChanged: CustomPropertyChanging);


        public Color UnselectedColor
        {
            get { return (Color)GetValue(UnselectedColorProperty); }
            set { SetValue(UnselectedColorProperty, value); }
        }

        public static readonly BindableProperty InitialValuePathProperty =
           BindableProperty.Create(nameof(InitialValuePath), typeof(string), typeof(TogglesBar), null,
               propertyChanged: CustomPropertyChanging);

        public string InitialValuePath
        {
            get { return (string)GetValue(InitialValuePathProperty); }
            set { SetValue(InitialValuePathProperty, value); }
        }

        public static readonly BindableProperty InitialValueProperty =
            BindableProperty.Create(nameof(InitialValue), typeof(object), typeof(TogglesBar), null,
                propertyChanged: CustomPropertyChanging);

        public object InitialValue
        {
            get { return GetValue(InitialValueProperty); }
            set { SetValue(InitialValueProperty, value); }
        }
        #endregion

        private static void CustomPropertyChanging(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((TogglesBar)bindable).Render();
        }

        private void Render()
        {
            try
            {
                if (ItemsSource == null || ItemsSource.Count() == 0)
                    return;

                stackContainer.Children.Clear();
                if (IsMultiSelect)
                    SelectedItems = new ObservableCollection<object>();
                foreach (var item in ItemsSource)
                {
                    var displayText = DisplayMemberPath == null ? item.ToString() : item.GetType().GetProperty(DisplayMemberPath).GetValue(item, null).ToString();
                    var btn = new ToggleButton
                    {
                        Text = displayText,
                        FontFamily = FontFamily,
                        BackgroundColor = BackgroundColor,
                        SelectedColor = SelectedColor,
                        UnselectedColor = UnselectedColor
                    };
                    if (!string.IsNullOrEmpty(InitialValuePath))
                    {
                        var value = item.GetType().GetProperty(InitialValuePath).GetValue(item, null);
                        if (value.ToString().Equals(InitialValue))
                            btn.IsSelected = true;

                    }
                    else if (InitialIndex >= 0)
                    {
                        if (ItemsSource.IndexOf(item) == InitialIndex)
                            btn.IsSelected = true;
                    }
                    btn.SelectionChanged += (s, e) =>
                    {
                        if (IsMultiSelect)
                        {
                            if (btn.IsSelected)
                                (SelectedItems as ObservableCollection<object>).Add(item);
                            else
                                (SelectedItems as ObservableCollection<object>).Remove(item);
                        }
                        else
                        {
                            var allToggleButtons = stackContainer.Children.Where(x => x is ToggleButton);
                            allToggleButtons?.ForEach(x => ((ToggleButton)x).IsSelected = false);
                            btn.IsSelected = true;
                            SelectedItems = item;
                        }
                        if (!IsMultiSelect && btn.IsSelected)
                            SelectedItemsChanged?.Invoke(this, new TogglesBarSelectionChangedEventArgs
                            {
                                SelectedItems = SelectedItems,
                                SelectedIndices = ItemsSource.IndexOf(item)
                            });
                        else if (IsMultiSelect)
                        {
                            SelectedItemsChanged?.Invoke(this, new TogglesBarSelectionChangedEventArgs
                            {
                                SelectedItems = SelectedItems,
                                SelectedIndices = (SelectedItems as ObservableCollection<object>).Select(x => ItemsSource.IndexOf(x))
                            });
                        }
                    };
                    stackContainer.Spacing = ItemsSpacing;
                    stackContainer.Children.Add(btn);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
