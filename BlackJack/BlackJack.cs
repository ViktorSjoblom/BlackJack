namespace BlackJack
{
    public class BlackJack
    {
        public Players Dealer = new Players();
        public Players Player = new Players();
        public GameResult Result { get; set; }

        private Deck _mainDeck;

        private int StandLimit { get; set; }
        
        public BlackJack(int dealerStandLimit)
        {
            // Setup a blackjack game...

            Result = GameResult.Pending;

            StandLimit = dealerStandLimit;
            
            // Throw a new shuffled deck on table

            _mainDeck = BlackJackRules.ShuffledDeck;
            
            // Clear Player & Dealer hands (and sleeves h3h3).
            Dealer.Hand.Clear();
            Player.Hand.Clear();
            
            // Deal the first two cards to player & dealer
            for (var i = 0; ++i < 3;)
            {
                Dealer.Hand.Push(_mainDeck.Pop());
                Player.Hand.Push(_mainDeck.Pop());
            }
        }
            // Allow player to hit. Dealer automatically hits when user stands.
        public void Hit()
        {
            if (BlackJackRules.CanPlayerHit(Player.Hand) && Result == GameResult.Pending)
            {
                Player.Hand.Push(_mainDeck.Pop());
            }
        }
        // When user stands, allow the dealer to continue hitting until standlimit or bust.
        // Then go ahead and set the game result.
        public void Stand()
        {
            if (Result == GameResult.Pending)
            {

                while (BlackJackRules.CanDealerHit(Dealer.Hand, StandLimit))
                {
                    Dealer.Hand.Push(_mainDeck.Pop());
                }

                Result = BlackJackRules.GetResult(Player, Dealer);
            }
        }
    }
}