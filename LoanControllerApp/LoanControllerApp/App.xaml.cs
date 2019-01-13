using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LoanControllerApp
{
	public partial class App : Application
	{
        public static string UserToken = null;
        public App ()
		{
            CultureInfo currentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = currentCulture;
            InitializeComponent();
			MainPage = new NavigationPage(new LoanControllerApp.MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
