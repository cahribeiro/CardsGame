using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsGameAPI.Models
{
    public class CardRequest
    {
        public Guid Id { get; set; }
        public bool isHigher { get; set; }
        
    }
}
