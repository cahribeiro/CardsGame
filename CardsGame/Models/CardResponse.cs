using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsGameAPI.Models
{
    public class CardResponse
    {
        public string result { get; set; }
        public string actualCard { get; set; }
        public string nextCard { get; set; }
    }
}
