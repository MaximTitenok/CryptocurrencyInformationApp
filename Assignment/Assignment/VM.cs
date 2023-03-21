using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Windows;

namespace Assignment
{
    public class VM : INotifyPropertyChanged
    {
        public static readonly DependencyProperty TitleProperty;
        public VM(string backgroundColor)
        {
            YourColorProperty = backgroundColor;
            
            ButtonConvert = new RelayCommand(new Action<object>(ShowConvertingPage));
            ButtonSearchPage = new RelayCommand(new Action<object>(ShowSearchPage));
            ButtonConverting = new RelayCommand(new Action<object>(GetConverting));
            ButtonSearch = new RelayCommand(new Action<object>(ToFindCoin));
            ButtonTheme = new RelayCommand(new Action<object>(ChangeTheme));
            ButtonViewInformation = new RelayCommand(new Action<object>(ShowInformationPage));
            ButtonGoWebsite = new RelayCommand(new Action<object>(GoToWebsite));
            
        }


        public ObservableCollection<CoinsItem> CoinsList { get; set; } = new ObservableCollection<CoinsItem>();
        public ObservableCollection<TrendsItem> TrendsList { get; set; } = new ObservableCollection<TrendsItem>();
        public ObservableCollection<SearchItem> SearchList { get; set; } = new ObservableCollection<SearchItem>();
        public ObservableCollection<MarketsItem> MarketsList { get; set; } = new ObservableCollection<MarketsItem>();
        public ObservableCollection<double> PriceList { get; set; } = new ObservableCollection<double>();
        public string[] vs_currencies { get; set; } =
        {
            "btc","eth","ltc","bch","bnb","eos","xrp","xlm",
            "link","dot","yfi","usd","aed","ars","aud","bdt","bhd",
            "bmd","brl","cad","chf","clp","cny","czk","dkk","eur",
            "gbp","hkd","huf","idr","ils","inr","jpy","krw","kwd",
            "lkr","mmk","mxn","myr","ngn","nok","nzd","php","pkr",
            "pln","rub","sar","sek","sgd","thb","try","twd","uah",
            "vef","vnd","zar","xdr","xag","xau","bits","sats"
        };

        int selectedField;

        public string YourColorProperty { get; set; } = "#FFA0A0A0";

        public string ConvertingAmountCoins { get; set; }
        public string ConvertingCoinID { get; set; }
        public string ConvertingCurrency { get; set; }
        public string ConvertingResult { get; set; }

        public string SearchField { get; set; }

        public string InfoCoinID { get; set; }
        public string InfoName { get; set; }
        public string InfoSymbol { get; set; }
        public string InfoPrice { get; set; }
        public string InfoVolume { get; set; }
        public string InfoMarkCap { get; set; }
        public string InfoDescription { get; set; }
        public string InfoWebsite { get; set; }

        public string ErrorReason { get; set; }

        public int SelectedField
        {
            get { return selectedField; }
            set
            {
                selectedField = value;
            }
        }

        public ICommand ButtonConvert { get; set; }
        public ICommand ButtonConverting { get; set; }
        public ICommand ButtonSearchPage { get; set; }
        public ICommand ButtonSearch { get; set; }
        public ICommand ButtonTheme { get; set; }
        public ICommand ButtonViewInformation { get; set; }
        public ICommand ButtonGoWebsite { get; set; }

        public void StartMain()
        {
            LoadCoinsList();
            LoadTrend();
        }

        public void ShowConvertingPage(object obj)
        {
            ConvertingPage convertingPage = new ConvertingPage(YourColorProperty);
            convertingPage.Show();
        }
        public void ShowSearchPage(object obj)
        {
            
            SearchPage searchPage = new SearchPage(YourColorProperty);
            searchPage.Show();
        }
        public void ShowInformationPage(object obj)
        {
            if (CoinsList.Count != 0)
            {
                InformationPage informationPage = new InformationPage(YourColorProperty, CoinsList[SelectedField].id ?? "");
                informationPage.Show();
            }    
        }
        public void GoToWebsite(object obj)
        {
            if (InfoWebsite != null)
            {
                System.Diagnostics.Process.Start(InfoWebsite);
            }
            else
            {
                Error error = new Error(YourColorProperty, "Invalid link!");
                error.Show();
                return;
            }
        }
        
        public async void LoadCoinsList()
        {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false");
        var response = await client.SendAsync(request);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            Error error = new Error(YourColorProperty, "Request is failed!");
            error.Show();
            return;
        }

        var m = JsonConvert.DeserializeObject<List<CoinsItem>>(await response.Content.ReadAsStringAsync());

        foreach (var i in m)
        {
            CoinsList.Add(i);
        }


        }

        public async void LoadTrend()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coingecko.com/api/v3/search/trending");
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                Error error = new Error(YourColorProperty, "Request is failed!");
                error.Show();
                return;
            }

            var m = JsonConvert.DeserializeObject<Trends>(await response.Content.ReadAsStringAsync());

            foreach (var i in m.coins)
            {
                TrendsList.Add(i.item);
            }


        }

        public async void GetConverting(object obj)
        {
            if (!string.IsNullOrEmpty(ConvertingAmountCoins) && !string.IsNullOrEmpty(ConvertingCoinID) && !string.IsNullOrEmpty(ConvertingCurrency))
            {
                ConvertingAmountCoins = ConvertingAmountCoins.Replace(".", ",");
                bool isCorrect = double.TryParse(ConvertingAmountCoins, out double amount_coins);
                if (isCorrect == false)
                {
                    Error error = new Error(YourColorProperty, "Incorrect data!");
                    error.Show();
                    return;
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coingecko.com/api/v3/simple/price?ids=" + ConvertingCoinID + "&vs_currencies=" + ConvertingCurrency);
                var response = await client.SendAsync(request);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    Error error = new Error(YourColorProperty, "Request is failed!");
                    error.Show();
                    return;
                }
                string result = await response.Content.ReadAsStringAsync();

                int indexOfPrice = result.IndexOf(ConvertingCurrency) + ConvertingCurrency.Length + 2;
                string price = Convert.ToString(result.Substring(indexOfPrice).Replace("}}", ""));
                price = price.Replace(".", ",");

                ConvertingResult = Convert.ToString(Convert.ToDouble(price) * amount_coins);
                OnPropertyChanged("ConvertingResult");
            }
            else
            {
                Error error = new Error(YourColorProperty, "Fill all the fields!");
                error.Show();
                return;
            }
        }

        public async void ToFindCoin(object obj)
        {
            if (SearchField == "")
            {
                Error error = new Error(YourColorProperty, "Fill the field!");
                error.Show();
                return;
            }
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coingecko.com/api/v3/search?query=" + SearchField);
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                Error error = new Error(YourColorProperty, "Request is failed!");
                error.Show();
                return;
            }
            SearchList.Clear();
            JObject info = JObject.Parse(await response.Content.ReadAsStringAsync());
            foreach (var i in info["coins"])
            {

                SearchList.Add(new SearchItem(i["name"].ToString() ?? "", i["symbol"].ToString() ?? "", i["market_cap_rank"].ToString() ?? ""));
            }

        }

        public void ChangeTheme(object obj)
        {
            if (YourColorProperty == "#FFA0A0A0")
            {
                YourColorProperty = "#FFFFFFFF";
            }
            else
            {
                YourColorProperty = "#FFA0A0A0";
            }
            OnPropertyChanged("YourColorProperty");
        }

        public async void LoadInfoPage(string id)
        {
            InfoCoinID = id;
            if (InfoCoinID == null || InfoCoinID == "")
            {
                Error error = new Error(YourColorProperty, "Unknown coin!");
                error.Show();
                return;
            }
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coingecko.com/api/v3/coins/" + InfoCoinID);
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                Error error = new Error(YourColorProperty, "Request is failed!");
                error.Show();
                return;
            }
            JObject result = JObject.Parse(await response.Content.ReadAsStringAsync());
            
            InfoName = result["name"].ToString() ?? "";
            OnPropertyChanged("InfoName");
            InfoCoinID = result["id"].ToString() ?? "";
            OnPropertyChanged("InfoCoinID");
            InfoSymbol = result["symbol"].ToString() ?? "";
            OnPropertyChanged("InfoSymbol");
            InfoPrice = result["market_data"]["current_price"]["usd"].ToString() + " $" ?? "";
            OnPropertyChanged("InfoPrice");
            InfoVolume = result["market_data"]["total_volume"]["usd"].ToString() ?? "";
            OnPropertyChanged("InfoVolume");
            InfoMarkCap = result["market_data"]["market_cap"]["usd"].ToString() ?? "";
            OnPropertyChanged("InfoMarkCap");
            InfoDescription = result["description"]["en"].ToString() ?? result["description"]["en"].ToString();
            OnPropertyChanged("InfoDescription");
            InfoWebsite = result["links"]["homepage"][0].ToString();

            foreach (var i in result["tickers"])
            {
                MarketsList.Add(new MarketsItem(i["market"]["name"].ToString(), i["target"].ToString(),Math.Round(Convert.ToDouble(i["converted_last"]["usd"]),2)));            
            }


            var requestPriceChange = new HttpRequestMessage(HttpMethod.Get, "https://api.coingecko.com/api/v3/coins/" + InfoCoinID + "/market_chart?vs_currency=usd&days=1");
            var responsePriceChange = await client.SendAsync(requestPriceChange);
            try
            {
                responsePriceChange.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                Error error = new Error(YourColorProperty, "Request is failed!");
                error.Show();
                return;
            }
            
            JObject history = JObject.Parse(await responsePriceChange.Content.ReadAsStringAsync());
            foreach (var i in history["prices"])
            {
                PriceList.Add(Math.Round(Convert.ToDouble(i[1]), 4, MidpointRounding.AwayFromZero));
            }
            OnPropertyChanged("PriceList");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
