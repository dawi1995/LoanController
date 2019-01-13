using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoanControllerApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryWithMessage : Grid
	{
        public static readonly BindableProperty TextProperty = BindableProperty.Create(propertyName: "Text", returnType: typeof(string),
        declaringType: typeof(EntryWithMessage), defaultValue: string.Empty);
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty MessageTextProperty = BindableProperty.Create(propertyName: "MessageText", returnType: typeof(string),
        declaringType: typeof(EntryWithMessage), defaultValue: string.Empty);
        public string MessageText
        {
            get { return (string)GetValue(MessageTextProperty); }
            set { SetValue(MessageTextProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string),
        declaringType: typeof(EntryWithMessage), defaultValue: string.Empty);
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty MessageTextColorHexProperty = BindableProperty.Create(propertyName: "MessageTextColorHex", returnType: typeof(string),
        declaringType: typeof(EntryWithMessage), defaultValue: "#333436");
        public string MessageTextColorHex
        {
            get { return (string)GetValue(MessageTextColorHexProperty); }
            set { SetValue(MessageTextColorHexProperty, value); }
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(propertyName: "IsPassword", returnType: typeof(bool),
        declaringType: typeof(EntryWithMessage), defaultValue: false);
        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }
        public EntryWithMessage()
		{
			InitializeComponent();
            BindingContext = this;
        }
    }
}
