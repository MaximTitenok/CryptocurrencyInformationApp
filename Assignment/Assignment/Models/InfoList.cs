using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{

    public class MarketsItem : INotifyPropertyChanged
    {
        public MarketsItem(string market, string currency, double price)
        {
            this.market = market;
            this.currency = currency;
            this.price = price;
        }

        public string market { get; set; }
        public string currency { get; set; }
        public double price { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
