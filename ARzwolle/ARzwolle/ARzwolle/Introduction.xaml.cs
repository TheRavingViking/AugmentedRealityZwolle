﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using ARzwolle.Classes;

namespace ARzwolle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Introduction : ContentPage
	{

		public Introduction ()
		{
			InitializeComponent();

            // Checks if toggle button is toggled and sets the toggle button to true
            if ((int)Application.Current.Properties["id"] == 1)
            {
                Show_Again.IsToggled = true;
            }

            IntroductionText.Text = "Het klimaat verandert. Verander jij mee? Door samen te werken aan oplossingen die helpen de overlast door klimaatverandering tegen te gaan. Die ons helpen de toenemende verstening in steden te veranderen naar een omgeving met meer groen. Zoals stenen vervangen door begroeiing voor meer verkoeling tegen de hittestress. Of het toenemende water van stortbuien beter gebruiken. Het zijn een paar voorbeelden van oplossingen waarmee we de gevolgen van de klimaatverandering kunnen beperken. Kijk hoe jij kunt meedoen aan een klimaatbestendig Nederland!";
            TextTitle.Text = "Introductie";
        }

        // Function used when skip button is clicked
        private async void SkipVideo_Clicked(object sender, EventArgs e)
        {

            SkipVideo.IsEnabled = false;
            if (SkipVideo.IsEnabled == false)
            {
                MasterPage MasterPage = new MasterPage();

                //Checks is toggle button is toggled and saves it in the current application
                if (Show_Again.IsToggled == true)
                    Application.Current.Properties["id"] = 1;
                else
                    Application.Current.Properties["id"] = 0;

                await Navigation.PushModalAsync(MasterPage);
                SkipVideo.IsEnabled = true;
            }

        }

        
    }
}
