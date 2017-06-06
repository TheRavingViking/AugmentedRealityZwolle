using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ARzwolle
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Checks if the current application contains a key named id
            if (Application.Current.Properties.ContainsKey("id"))
            {
                // Checks if the key named id contains 1 and parses it to int since it's a boolean
                if ((int)Application.Current.Properties["id"] == 1)
                    // Creates the MasterPage and starts the application with GoogleMaps since the default in MasterPage is GoogleMaps
                    MainPage = new MasterPage();
                else
                    // Creates the Introduction page and starts the application with this page
                    MainPage = new Introduction();
            }
            else
            {
                // Creates a key named id, sets the value to 0 and creates the Introduction page to start the application with
                Application.Current.Properties["id"] = 0;
                MainPage = new Introduction();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
