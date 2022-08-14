using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinTestTask.Models;
using XamarinTestTask.ViewModels;

namespace XamarinTestTask.Views
{
    public partial class NewItemPage : ContentPage
    {
        public ExchangeRateModel Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}