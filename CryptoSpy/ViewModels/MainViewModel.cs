using CoinCapHandler;
using CryptoSpy.Infrastructure;
using CryptoSpy.Models;
using CryptoSpy.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CryptoSpy.ViewModels
{
    public class MainViewModel
    {
        private CoinCapApiHandler _handler;

        public ObservableCollection<Cryptocurrency> Cryptocurrencies { get; set; } = new ObservableCollection<Cryptocurrency>();

        public Cryptocurrency Detailed { get; set; }

        public Page TopCurrenciesPage { get; set; }

        public Page DetailsPage { get; set; }

        public string Request { get; set; }

        public ICommand Search { get; private set; }

        public MainViewModel()
        {
            _handler = new CoinCapApiHandler();

            ConfigurePages();
            InitCommands();
            LoadCurrencies();
        }

        private void InitCommands()
        {
            Search = new RelayCommand(async x =>
            {
                JObject response = await _handler.GetJson($"{_handler.Url}/{Request.ToLower()}");
                Detailed = ParseJsonCurrency(response);
            });
        }

        private void ConfigurePages()
        {
            TopCurrenciesPage = new TopCurrencies();
            TopCurrenciesPage.DataContext = this;
            DetailsPage = new CurrencyDetails();
            DetailsPage.DataContext = this;
        }

        private async Task LoadCurrencies()
        {
            JObject result = await _handler.GetJson($"{_handler.Url}/assets?limit=10");
            
            List<Cryptocurrency> currencies = ParseJsonCurrencyCollection(result).ToList();

            foreach(var item in currencies)
            {
                Cryptocurrencies.Add(item);
            }
        }

        private Cryptocurrency ParseJsonCurrency(JObject json)
        {
            var jsonResponse = json["data"];
            return JsonConvert.DeserializeObject<Cryptocurrency>(jsonResponse.ToString());
        }

        private IEnumerable<Cryptocurrency> ParseJsonCurrencyCollection(JObject json)
        {
            var jsonCurrencies = json["data"].Children().ToList();
            
            List<Cryptocurrency> currencies = new List<Cryptocurrency>();
            foreach (var jsonCurrency in jsonCurrencies)
            {
                currencies.Add(JsonConvert.DeserializeObject<Cryptocurrency>(jsonCurrency.ToString()));
            }
            return currencies;

            //var aaa = JsonConvert.DeserializeObject<ObservableCollection<Cryptocurrency>>(jsonCurrencies.ToString());
            //return aaa;
        }
    }
}
