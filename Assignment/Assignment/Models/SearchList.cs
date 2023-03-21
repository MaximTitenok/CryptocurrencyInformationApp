using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class SearchItem : INotifyPropertyChanged
    {
        public SearchItem(string name,string symbol,string market_cap_rank)
        {
            this.name = name;
            this.symbol = symbol;
            this.market_cap_rank = market_cap_rank;
        }
        public string name { get; set; }
        public string symbol { get; set; }
        public string market_cap_rank { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
