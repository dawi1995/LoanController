﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoanControllerApp
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            var assembly = typeof(LoanControllerApp.App).GetTypeInfo().Assembly;
            var imageSource = ImageSource.FromResource(Source, assembly);

            return imageSource;
        }
    }
}
