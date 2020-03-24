using System.ComponentModel;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Xamlly.Sample
{

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var prgrss = stkParent.Children.Where(x => x is XamllyControls.ProgressBar);
            foreach (XamllyControls.ProgressBar prog in prgrss)
            {
                if (prog.Progress > 0.9d)
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
