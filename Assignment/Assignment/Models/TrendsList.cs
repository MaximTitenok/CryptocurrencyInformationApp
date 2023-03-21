using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class TrendsCoins 
    {
        public TrendsItem item { get; set; }

        
    }
    public class TrendsItem : INotifyPropertyChanged
    {
        public string id { get; set; }
        public int coin_id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public int? market_cap_rank { get; set; }
        public string thumb { get; set; }
        public string small { get; set; }
        public string large { get; set; }
        public string slug { get; set; }
        public double? price_btc { get; set; }
        public int score { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class Trends
    {
        public List<TrendsCoins> coins { get; set; }
        public List<object> exchanges { get; set; }
    }
}
