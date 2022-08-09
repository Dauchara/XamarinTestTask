using System.ComponentModel;
using Xamarin.Forms;
using XamarinTestTask.ViewModels;

namespace XamarinTestTask.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}