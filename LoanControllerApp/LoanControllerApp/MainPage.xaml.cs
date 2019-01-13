using LoanControllerApp.Languages;
using LoanControllerApp.Models;
using LoanControllerApp.Services.Web;
using LoanControllerApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoanControllerApp
{
	public partial class MainPage : ContentPage
	{
        SecurityProxyClient _securityProxyClient;
		public MainPage()
		{
			InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            _securityProxyClient = new SecurityProxyClient();
            //LoginData user = new LoginData();
            //Task getStartScreen = Task.Run(async () => { user = await _securityProxyClient.Login(new Models.User { Email = "test2@gmail.com", Password = "test" }); });
            //getStartScreen.Wait();
            ////User user = await _securityProxyClient.Login(new Models.User { Email = "test2@gmail.com", Password = "test" });
            //var test = user;
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new RegistrationPage());
            }
            catch (Exception ex)
            {
                var aaa = ex.ToString();
                //log insert
            }
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            try
            {
                LoginData loginData = await _securityProxyClient.Login(new User {
                                                    Email = emailEntry.Text,
                                                    Password = passwordEntry.Text
                                                });

                if(loginData == null || string.IsNullOrEmpty(loginData.Token))
                {
                    await DisplayAlert("Błąd", "Dane są nprawidłowe", "OK");
                }
                else
                {
                    App.UserToken = loginData.Token;
                    await Navigation.PushAsync(new LoansPage());
                }
            }
            catch (Exception ex)
            {
                var aaa = ex.ToString();
                //log insert
            }
        }
    }
}
