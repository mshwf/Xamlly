using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamlly.Sample
{

    class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var people = new List<Person> {
                new Person {ID = 1, Name = "Mohamed"},
                new Person {ID = 2, Name = "Ali"},
                new Person {ID = 3, Name = "Kamal"},
                new Person {ID = 4, Name = "Shit"},
            };
            bar.ItemsSource = people;
            rbg.ItemsSource = people;
        }


        private void Button_Clicked(object sender, System.EventArgs e)
        {
            foreach (Xamlly.XamllyControls.ProgressBar prog in stkParent.Children.Where(x => x is Xamlly.XamllyControls.ProgressBar))
            {
                if (prog.Progress == 1.0d)
                    prog.Progress = 0.0d;
                else
                    prog.Progress += .1;

            }
        }

        private void rbg_OnSelectionChanged(object sender, XamllyControls.SelectionChangedEventArgs osc)
        {

        }
    }
}
