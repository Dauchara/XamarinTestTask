using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using XamarinTestTask.Models;
using XamarinTestTask.Views;

namespace XamarinTestTask.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ExchangeRateModel _selectedItem;

        public ObservableCollection<ExchangeRateModel> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<ExchangeRateModel> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Exchange Rates";
            Items = new ObservableCollection<ExchangeRateModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<ExchangeRateModel>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public ExchangeRateModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(ExchangeRateModel item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Cur_ID}");
        }
    }
}