﻿using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace XamllySample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
    }
}