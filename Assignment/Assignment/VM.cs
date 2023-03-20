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

namespace Assignment
{
    public class VM : INotifyPropertyChanged
    {
        public VM()
        {
            LoadCoinsList();
        }


        public ObservableCollection<CoinsItem> CoinsList { get; set; } = new ObservableCollection<CoinsItem>();
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
                //Error error = new Error();
                //error.reason = "Request is failed!";
                //error.Show();
                return;
            }

            var m = JsonConvert.DeserializeObject<List<CoinsItem>>(await response.Content.ReadAsStringAsync());

            foreach(var i in m)
            {
                CoinsList.Add(i);
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
