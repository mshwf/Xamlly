using System;
using System.Collections.Generic;

namespace Xamlly.XamllyControls
{
    public class ItemsAddedEventArgs : EventArgs
    {
        public IEnumerable<object> Items { get; }
        public ItemsAddedEventArgs(IEnumerable<object> items)
        {
            Items = items;
        }
    }
}