
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Xamlly.Sample
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Option> colors;

        public ObservableCollection<Option> Colors
        {
            get => colors; 
            set
            {
                colors = value;
                OnPropertyChanged();
            }
        }
        public MainPageViewModel()
        {
            Colors = new ObservableCollection<Option>(new List<Option>
            {
                new Option {ID = 1, Name = "Option 1"},
                new Option {ID = 2, Name = "Option 2"},
                new Option {ID = 3, Name = "Option 3"},
                new Option {ID = 4, Name = "Option 4"},
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}