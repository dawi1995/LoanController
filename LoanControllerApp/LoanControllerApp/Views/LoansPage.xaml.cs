using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoanControllerApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoansPage : ContentPage
	{
		public LoansPage()
		{
			InitializeComponent();
            if (!string.IsNullOrEmpty(App.UserToken))
            {
                Label.Text = App.UserToken;
            }
		}
	}
}