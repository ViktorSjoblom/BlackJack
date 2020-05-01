using System.Collections.Generic;

namespace BlackJack
{

    public enum GameResult
    {
        Win = 1,
        Lose = -1,
        Draw = 0,
        Pending = 2
    };
    public class Card
    {
        public string Id { get; private set; }
        public string Suit { get; private set; }
        public int Value { get; private set; }

        public Card(string id, string suit, int value)
        {
            Id = id;
            Suit = suit;
            Value = value;
        }
    }

    public class Deck : Stack<Card>
    {
        public Deck(IEnumerable<Card> collection) : base(collection) { }
        public Deck() : base(52) { }

        public Card this[int index]
        {
            get
            {
                Card item;

                if (index >= 0 && index <= this.Count - 1)
                {
                    item = this.ToArray()[index];
                }
                else
                {
                    item = null;
                }

                return item;
            }
        }
        // Value of deck

        public double Value => BlackJackRules.HandValue(this);
    }
}