using System;

namespace BlackJack
{
    static class Program
    {
        private static void ShowStats(BlackJack bj)
        {
            Console.WriteLine("Dealer");
            foreach (var c in bj.Dealer.Hand)
            {
                Console.WriteLine("Card:");
                Console.WriteLine(c.Id, c.Suit);
            }

            Console.WriteLine("Total value: ");
                Console.WriteLine(bj.Dealer.Hand.Value);

                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("Player");
                foreach (var c in bj.Player.Hand)
                {
                    Console.WriteLine("Card:");
                    Console.WriteLine(c.Id, c.Suit);
                }

                Console.WriteLine("Total value: ");
                Console.WriteLine(bj.Player.Hand.Value);

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Another card? Press 'H' for hit.");
                Console.WriteLine("Want to stop? Press 'S' for stay.");

        }
        static void Main(string[] args)
        {
            var input = "";
            
            BlackJack bj = new BlackJack(17);

            ShowStats(bj);
            while (bj.Result == GameResult.Pending)
            {
                input = Console.ReadLine();

                if (input != null && input.ToLower() == "h")
                {
                    Console.WriteLine("You chose to hit!");
                    bj.Hit();
                    ShowStats(bj);
                }
                else
                {
                    Console.WriteLine("You chose to stand!");
                    bj.Stand();
                    ShowStats(bj);
                }
            }

            Console.WriteLine("You: ");
            Console.WriteLine(bj.Result);
            Console.ReadLine();
        }
    }
}