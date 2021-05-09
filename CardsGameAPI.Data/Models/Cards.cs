using System;
using System.Collections.Generic;
using System.Text;

namespace CardsGameAPI.Data
{
    public class Cards
    {
        public Guid ID { get; set; }
        public enum Suits { Hearts, Spades, Diamonds, Clubs };
        public enum Kinds { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
        public Suits Suit { get; set; }
        public Kinds Kind { get; set; }        

        public override string ToString()
        {
            return $"{Kind} of {Suit}";
        }

        public Cards(Suits suit, Kinds kind)
        {
            ID = Guid.NewGuid();
            this.Suit = suit;
            this.Kind = kind;
        }
    }
}
