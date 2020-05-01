using System;
using System.Linq;

namespace BlackJack
{
    // Game rules
    public static class BlackJackRules
    {
        // Card values
        private static string[] ids = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "A", "J", "K", "Q"};
        // Card suits
        private static string[] suits = {"C", "D", "H", "S"};
        
        // Returns a new deck
        private static Deck NewDeck
        {
            get
            {
                var deck = new Deck();
                int value;

                foreach (var suit in suits)
                {
                    foreach (var id in ids)
                    {
                        value = Int32.TryParse(id, out value) ? value : id == "A" ? 1 : 10;
                        deck.Push(new Card(id, suit, value));
                    }
                }

                return deck;
            }
        }
        // Returns a shuffled deck
        public static Deck ShuffledDeck
        {
            get
            {
                return new Deck(NewDeck.OrderBy(card => System.Guid.NewGuid()).ToArray());
            }
        }
        // Calculate the value of a hand.
        public static double HandValue(Deck deck)
        {
            var val1 = deck.Sum(c => c.Value);

            var aces = deck.Count(c => c.Suit == "A");
            var val2 = aces > 0 ? val1 + (10 * aces) : val1;

            return new double[] {val1, val2}
                .Select(handValue => new
                {
                    handValue, weight = Math.Abs(handValue - 21) + (handValue > 21 ? 100 : 0)
                })
                .OrderBy(n => n.weight)
                .First().handValue;
        }
        
        // A few more rules
        // Assume Dealer will always stand on 17 and not hit on soft 17.
        public static bool CanDealerHit(Deck deck, int standLimit)
        {
            return deck.Value < standLimit;
        }
        // No point hitting above 21.

        public static bool CanPlayerHit(Deck deck)
        {
            return deck.Value < 21;
        }
        // Return game state win, lose or draw given players' hands
        public static GameResult GetResult(Players player, Players dealer)
        {
            var res = GameResult.Win;

            var playerValue = HandValue(player.Hand);
            var dealerValue = HandValue(dealer.Hand);
            // Player could be winner if ...
            if (playerValue <= 21)
            {
                //and ..
                if (playerValue != dealerValue)
                {
                    var closestValue = new double[] {playerValue, dealerValue}
                        .Select(handVal => new {handVal, weight = Math.Abs(handVal - 21) + (handVal > 21 ? 100 : 0)})
                        .OrderBy(n => n.weight)
                        .First().handVal;

                    res = playerValue == closestValue ? GameResult.Win : GameResult.Lose;
                }
                else
                {
                    res = GameResult.Draw;
                }
            }
            else
            {
                res = GameResult.Lose;
            }

            return res;
        }
    }
}