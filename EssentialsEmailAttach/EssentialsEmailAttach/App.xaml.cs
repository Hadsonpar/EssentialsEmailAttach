using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EssentialsEmailAttach
{
    public partial class App : Application
    {
        public static NavigationPage Mainpage;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
