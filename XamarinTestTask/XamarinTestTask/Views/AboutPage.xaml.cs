using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTestTask.Models;
using XamarinTestTask.Services;

namespace XamarinTestTask.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            ExchangeRateService exchangeRateService = new ExchangeRateService();
            var test = JsonConvert.DeserializeObject<List<ExchangeRateModel>>(exchangeRateService.SendRequest($"periodicity=0&ondate={DateTime.Now:yyyy-MM-dd}"));
        }
    }
}