using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Create a new instance of your main page
            MainPage mainPage = new MainPage();

            // Create a new instance of a navigation page with your main page as the root page
            NavigationPage navigationPage = new NavigationPage(mainPage);

            // Set the main page of your application to the navigation page
            MainPage = navigationPage;

            // Hide the navigation bar on the MainPage
            NavigationPage.SetHasNavigationBar(mainPage, false);

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
