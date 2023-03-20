using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Roi
    {
        public double times { get; set; }
        public string currency { get; set; }
        public double percentage { get; set; }
    }

    public class CoinsItem : INotifyPropertyChanged
    {
        double current_price_rounded;
        double price_change_percentage_24h_rounded;
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public double current_price {
            get
            {
                return current_price_rounded;
            }
            set
            {
                current_price_rounded = Math.Round(value, 2);  
            }

        }
        public object market_cap { get; set; }
        public int market_cap_rank { get; set; }
        public object fully_diluted_valuation { get; set; }
        public object total_volume { get; set; }
        public double high_24h { get; set; }
        public double low_24h { get; set; }
        public double price_change_24h { get; set; }
        public double price_change_percentage_24h {
            get
            {
                return price_change_percentage_24h_rounded;
            }
            set
            {
                price_change_percentage_24h_rounded = Math.Round(value, 2);
            }
        }
        public object market_cap_change_24h { get; set; }
        public double market_cap_change_percentage_24h { get; set; }
        public double circulating_supply { get; set; }
        public double? total_supply { get; set; }
        public double? max_supply { get; set; }
        public double ath { get; set; }
        public double ath_change_percentage { get; set; }
        public DateTime ath_date { get; set; }
        public double atl { get; set; }
        public double atl_change_percentage { get; set; }
        public DateTime atl_date { get; set; }
        public Roi roi { get; set; }
        public DateTime last_updated { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }

}
