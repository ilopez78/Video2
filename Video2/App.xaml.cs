using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Video2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MenuView());
        }
    }
}