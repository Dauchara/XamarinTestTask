using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using XamarinTestTask.Models;

namespace XamarinTestTask.Services
{
    public class MockDataStore : IDataStore<ExchangeRateModel>
    {
        readonly List<ExchangeRateModel> items;

        public MockDataStore()
        {
            ExchangeRateService exchangeRateService = new ExchangeRateService();
            try
            {
                items = JsonConvert.DeserializeObject<List<ExchangeRateModel>>(exchangeRateService.SendRequest($"periodicity=0&ondate={DateTime.Now:yyyy-MM-dd}"));
            }
            catch (WebException ex)
            {
                using (StreamReader r = new StreamReader(((HttpWebResponse)ex.Response).GetResponseStream()))
                {
                    //result = r.ReadToEnd();
                    // save to log
                }
            }
            catch (Exception ex)
            {
                //result = ex.Message;
                // save to log
            }
        }

        public async Task<bool> AddItemAsync(ExchangeRateModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ExchangeRateModel item)
        {
            var oldItem = items.Where((ExchangeRateModel arg) => arg.Cur_ID == item.Cur_ID).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((ExchangeRateModel arg) => arg.Cur_ID == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ExchangeRateModel> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Cur_ID == id));
        }

        public async Task<IEnumerable<ExchangeRateModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}