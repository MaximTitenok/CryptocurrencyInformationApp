using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class SearchItem
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
    }
}
