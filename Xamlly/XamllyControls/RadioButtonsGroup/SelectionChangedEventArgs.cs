using System;

namespace Xamlly.XamllyControls
{
    public class SelectionChangedEventArgs : EventArgs
    {
        public object SelectedItem { get; }
        public object SelectedValue { get; }
        public int SelectedIndex { get; }
        public SelectionChangedEventArgs(object selectedItem, object selectedValue, int selectedIndex)
        {
            SelectedItem = selectedItem;
            SelectedIndex = selectedIndex;
            SelectedValue = selectedValue;
        }
    }
}