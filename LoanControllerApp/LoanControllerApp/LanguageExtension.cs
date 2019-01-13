using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoanControllerApp
{
    [ContentProperty("Text")]
    public class LanguageExtension : IMarkupExtension
    {
        const string ResourceId = "LoanControllerApp.Languages.Resource";
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(LanguageExtension).GetTypeInfo().Assembly);

            return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }
    }
}
