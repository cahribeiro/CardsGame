using System;
using System.Collections.Generic;
using System.Text;

namespace CardsGameAPI.Data
{
    public class Room
    {
        public Guid ID { get; set; }
        public int CardsPlayed { get; set; }        
        public List<Cards> DeckCards { get; set; }
    }
}
