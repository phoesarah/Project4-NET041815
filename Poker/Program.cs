using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{

    internal class Program
    {
        static Player player1 = new Player();
        static Player player2 = new Player();
       static Player player3 = new Player();
        static Player player4 = new Player();
        static Player player5 = new Player();
        

        public static Random rand = new Random();
        public static double pot;
        public static void Main(string[] args)
        {
            FirstMenu();

        }

        public static void FirstMenu()
        {
            Console.WriteLine("Welcome to Sarah's Poker Room! ");
            Console.WriteLine("1. Start");
            Console.WriteLine("2. Exit");
            string startgame = Console.ReadLine();
            if (startgame == "1")
            {
                Console.Clear();
                SecondMenu();
            }
            else if (startgame == "2")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("I'm sorry, you're going to have to speak up, I didn't hear you");
            }

        }

        public static void SecondMenu()
        {
            Console.WriteLine("Your Name:");
            string playername = Console.ReadLine();
            Console.WriteLine("How many players would you like to play against? 1, 2, 3, or 4");
            int numberofplayers = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How big would you like the ante to be?");
            Double antesize = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("How much money would you like to start with?");
            Double walletsize = Convert.ToDouble(Console.ReadLine());
            if (walletsize > antesize * 3)
            {
                SetupGame(playername, numberofplayers, antesize, walletsize);
            }
            else
            {
                Console.WriteLine("You don't have enough money to play with that ante size, " +
                                  "please choose a wallet size at least 3 times as large as the ante" +
                                  "you would like to play with \n-------------------------------------------------------------------" +
                                  "------------");

                SecondMenu();
            }
        }

        public static void SetupGame(string playermname, int numberofplayers, double antesize, double walletsize)
        {
            pot = antesize*numberofplayers;

            List<Card> deck = CreateDeck();

            

            if (numberofplayers == 1)
            {
                player1.Hand = Deal(deck);
                deck = removehandfromdeck(player1.Hand, deck);
                player2.Hand = Deal(deck);
                player1.Wallet = walletsize-antesize;
                player2.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player2.Wallet -= antesize;
            }
            else if (numberofplayers == 2)
            {
                player1.Hand = Deal(deck);
                deck = removehandfromdeck(player1.Hand, deck);
                player2.Hand = Deal(deck);
                deck = removehandfromdeck(player2.Hand, deck);
                player3.Hand = Deal(deck);
                player1.Wallet = walletsize - antesize;
                player2.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player3.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player2.Wallet -= antesize;
                player3.Wallet -= antesize;
            }
            else if (numberofplayers == 3)
            {
                player1.Hand = Deal(deck);
                deck = removehandfromdeck(player1.Hand, deck);
                player2.Hand = Deal(deck);
                deck = removehandfromdeck(player2.Hand, deck);
                player3.Hand = Deal(deck);
                deck = removehandfromdeck(player3.Hand, deck);
                player4.Hand = Deal(deck);
                player1.Wallet = walletsize - antesize;
                player2.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player3.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player4.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player2.Wallet -= antesize;
                player3.Wallet -= antesize;
                player4.Wallet -= antesize;

            }
            else if (numberofplayers == 4)
            {
                player1.Hand = Deal(deck);
                deck = removehandfromdeck(player1.Hand, deck);
                player2.Hand = Deal(deck);
                deck = removehandfromdeck(player2.Hand, deck);
                player3.Hand = Deal(deck);
                deck = removehandfromdeck(player3.Hand, deck);
                player4.Hand = Deal(deck);
                deck = removehandfromdeck(player4.Hand, deck);
                player5.Hand = Deal(deck);
                player1.Wallet = walletsize-antesize;
                player2.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player3.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player4.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player5.Wallet = Convert.ToDouble(rand.Next((Convert.ToInt32(antesize*3)), Convert.ToInt32(antesize*20)));
                player2.Wallet -= antesize;
                player3.Wallet -= antesize;
                player4.Wallet -= antesize;
                player5.Wallet -= antesize;
            }
            else
            {
                Console.WriteLine("You entered a stupid amount of players, go back and do it right, please");
                SecondMenu();
            }


            Console.WriteLine("Your Hand:");
            printlisttoscreen(player1.Hand);
            Console.WriteLine("Your Money: $" + player1.Wallet);
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Player 2    [x] [x] [x] [x] [x] " + player2.Wallet);
            Console.WriteLine("--------------------------------------------------------------");
            if (player3.Hand != null)
            {
                Console.WriteLine("Player 3    [x] [x] [x] [x] [x] " + player3.Wallet);
                Console.WriteLine("--------------------------------------------------------------");

                if (player4.Hand != null)
                {
                    Console.WriteLine("Player 4   [x] [x] [x] [x] [x] " + player4.Wallet);
                    Console.WriteLine("--------------------------------------------------------------");
                }


                if (player5.Hand != null)
                {
                    Console.WriteLine("Player 5   [x] [x] [x] [x] [x] " + player5.Wallet);
                    Console.WriteLine("--------------------------------------------------------------");
                }

                
            }
            Console.WriteLine("Would you like to Bet, pass, or fold?");
            Console.WriteLine("1 [BET]   2. [PASS] 3. [FOLD]");
            Console.ReadLine();
            CompareHands(player1.Hand, player2.Hand, player3.Hand, player4.Hand, player5.Hand);
            Console.ReadLine();
        }

        private static void printlisttoscreen(List<Card> deck)
        {
            deck = deck.OrderBy(x => x.Cardnumber).ToList();
            for (int i = 0; i < deck.Count; i++)
            {
                Console.Write("[" + cardnumber(deck[i].Cardnumber) + " of " + deck[i].Suit + "]  ");
            }
            
        }

        public static void CompareHands(List<Card> player1hand, List<Card> player2hand, List<Card> player3hand, List<Card> player4hand, List <Card> player5hand )
        {
            List<cardcombination> cardcombos= new List<cardcombination>();
            var enuminorder = cardcombos.OrderBy(x => x).ToList();

            if (player3hand == null)
            {
                cardcombination player1combo = getnameofhand(player1hand, player1);
                cardcombination player2combo = getnameofhand(player1hand, player2);
                if (player1combo == player2combo)
                {
                    Console.WriteLine("Tie");
                    printlisttoscreen(player2hand);
                    Console.ReadLine();
                }
                else if (player1combo > player2combo)
                {
                    Console.WriteLine("Player 1 wins with" + player1);
                    printlisttoscreen(player2hand);
                    Console.ReadLine();
                }
                else 
                {
                    Console.WriteLine("Player 2 wins with" + player2);
                    printlisttoscreen(player2hand);
                    Console.ReadLine();
                }
            }
           
        }
      
        private static cardcombination getnameofhand(List<Card> playerhand, Player player)
        {
            var sequenced = playerhand.OrderBy(x => x.Cardnumber).ToList();

            if ((sequenced[1].Cardnumber == (sequenced[0].Cardnumber + 1) &&
                 sequenced[2].Cardnumber == (sequenced[1].Cardnumber + 1) &&
                 sequenced[3].Cardnumber == (sequenced[2].Cardnumber + 1) &&
                 sequenced[4].Cardnumber == (sequenced[3].Cardnumber + 1)) &&
                sequenced[4].Cardnumber == 14 && (playerhand[0].Suit == playerhand[1].Suit && playerhand[0].Suit == playerhand[2].Suit &&
              playerhand[0].Suit == playerhand[3].Suit &&
               playerhand[0].Suit == playerhand[4].Suit))
            {
                return cardcombination.RoyalFlush;
            }
            else if ((sequenced[1].Cardnumber == (sequenced[0].Cardnumber + 1) &&
                 sequenced[2].Cardnumber == (sequenced[1].Cardnumber + 1) &&
                 sequenced[3].Cardnumber == (sequenced[2].Cardnumber + 1) &&
                 sequenced[4].Cardnumber == (sequenced[3].Cardnumber + 1)) &&
                (playerhand[0].Suit == playerhand[1].Suit && playerhand[0].Suit == playerhand[2].Suit &&
              playerhand[0].Suit == playerhand[3].Suit &&
               playerhand[0].Suit == playerhand[4].Suit))
            {
                player.HighCard = sequenced[4].Cardnumber;
                player.HandComparisonEvaluator = sequenced[4].Cardnumber;
                return cardcombination.StraightFlush;
                
            }

            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[0].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[0].Cardnumber == sequenced[3].Cardnumber || sequenced[4].Cardnumber == sequenced[1].Cardnumber && sequenced[4].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[4].Cardnumber == sequenced[3].Cardnumber)
            {
                player.HandComparisonEvaluator = sequenced[2].Cardnumber;
                player.HighCard = sequenced[4].Cardnumber;
                return cardcombination.FourofaKind;

            }

            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[0].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[3].Cardnumber == sequenced[4].Cardnumber || sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[4].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[3].Cardnumber == sequenced[4].Cardnumber)
            {
                player.HandComparisonEvaluator = sequenced[4].Cardnumber;
                player.HighCard = sequenced[0].Cardnumber;
                return cardcombination.FullHouse;

            }

            else if (sequenced[1].Cardnumber == (sequenced[0].Cardnumber + 1) && sequenced[2].Cardnumber == (sequenced[1].Cardnumber + 1) &&
                sequenced[3].Cardnumber == (sequenced[2].Cardnumber + 1) && sequenced[4].Cardnumber == (sequenced[3].Cardnumber + 1))
            {
                player.HandComparisonEvaluator = sequenced[4].Cardnumber;
                player.HighCard = sequenced[4].Cardnumber;
                return cardcombination.Straight;

            }


            else if (playerhand[0].Suit == playerhand[1].Suit && playerhand[0].Suit == playerhand[2].Suit &&
                     playerhand[0].Suit == playerhand[3].Suit &&
                     playerhand[0].Suit == playerhand[4].Suit)
            {
                player.HandComparisonEvaluator = sequenced[4].Cardnumber;
                player.HighCard = sequenced[4].Cardnumber;
                return cardcombination.Flush;
            }
            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber &&
                     sequenced[0].Cardnumber == sequenced[2].Cardnumber ||
                     sequenced[1].Cardnumber == sequenced[2].Cardnumber &&
                     sequenced[1].Cardnumber == sequenced[3].Cardnumber ||
                     sequenced[2].Cardnumber == sequenced[3].Cardnumber &&
                     sequenced[2].Cardnumber == sequenced[4].Cardnumber)
            {
                player.HandComparisonEvaluator = sequenced[2].Cardnumber;
                player.HighCard = sequenced[4].Cardnumber;
                return cardcombination.ThreeofaKind;

            }

            else if ((sequenced[0].Cardnumber == sequenced[1].Cardnumber &&
                      sequenced[2].Cardnumber == sequenced[3].Cardnumber) ||
                     (sequenced[0].Cardnumber == sequenced[1].Cardnumber &&
                      sequenced[3].Cardnumber == sequenced[4].Cardnumber) ||
                     (sequenced[1].Cardnumber == sequenced[2].Cardnumber &&
                      sequenced[3].Cardnumber == sequenced[4].Cardnumber))
            {
                if (sequenced[3].Cardnumber == sequenced[4].Cardnumber)
                {
                    player.HandComparisonEvaluator = sequenced[4].Cardnumber;
                    if (sequenced[2].Cardnumber == sequenced[1].Cardnumber)
                    {
                        player.HighCard = sequenced[2].Cardnumber;
                    }
                    else
                    {
                        player.HighCard = sequenced[0].Cardnumber;
                    }

                }
                else if (sequenced[3].Cardnumber == sequenced[2].Cardnumber)
                {
                    player.HandComparisonEvaluator = sequenced[3].Cardnumber;
                    player.HighCard = sequenced[1].Cardnumber;
                }
                return cardcombination.TwoPair;

            }

            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber ||
                     sequenced[1].Cardnumber == sequenced[2].Cardnumber ||
                     sequenced[2].Cardnumber == sequenced[3].Cardnumber ||
                     sequenced[3].Cardnumber == sequenced[4].Cardnumber)
            {
                player.HighCard = sequenced[4].Cardnumber;
                if (sequenced[3].Cardnumber == sequenced[4].Cardnumber)
                {
                    player.HandComparisonEvaluator = sequenced[4].Cardnumber;
                }
                else if (sequenced[2].Cardnumber == sequenced[3].Cardnumber)
                {
                    player.HandComparisonEvaluator = sequenced[3].Cardnumber;
                }
                else if (sequenced[1].Cardnumber == sequenced[2].Cardnumber)
                {
                    player.HandComparisonEvaluator = sequenced[2].Cardnumber;
                }
                else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber)
                {
                    player.HandComparisonEvaluator = sequenced[1].Cardnumber;
                }
                return cardcombination.Pair;

            }

            else
            {
                player.HandComparisonEvaluator = sequenced[4].Cardnumber;
                player.HighCard = sequenced[3].Cardnumber;
                return cardcombination.Nothing;
            }
        }

        private static List<Card> removehandfromdeck(List<Card> player1Hand, List<Card> deck)
        {
            List<Card> newdeck = deck;
            newdeck = newdeck.Except(player1Hand).ToList();
            return newdeck;
        }

        public enum cardcombination
        {
            Nothing,
            Pair,
            TwoPair,
            ThreeofaKind,
            Straight,
            Flush,
            FullHouse,
            FourofaKind,
            StraightFlush,
            RoyalFlush,
        }

        public static List<Card> CreateDeck()
        {

            List<Card> deck = new List<Card>();
            for (int i = 0; i < 4; i++)
            {

                string suit = "Spades";
                if (i == 1)
                    suit = "Hearts";
                if (i == 2)
                    suit = "Clubs";
                if (i == 3)
                    suit = "Diamonds";
                for (int j = 0; j < 13; j++)
                {
                    int num = 0;
                    Card card = new Card(num, suit);
                    card.Cardnumber = j + 2;
                    deck.Add(card);
                }
            }

            return deck;

        }




        

        public static List<Card> Deal(List<Card> deck)
        {
            List<Card> Hand = new List<Card>();

            for (int i = 0; i < 5; i++)
            {
                int index = Program.rand.Next(deck.Count());
                Hand.Add(deck[index]);
                deck.Remove(deck[index]);
            }
            return Hand;

        }
        public static string cardnumber(int cardnum)
        {
            switch (cardnum)
            {
                case 2:
                    {
                        return "2";
                    }
                case 3:
                    {
                        return "3";
                    }
                case 4:
                    {
                        return "4";

                    }
                case 5:
                    {
                        return "5";

                    }
                case 6:
                    {
                        return "6";

                    }
                case 7:
                    {
                        return "7";

                    }
                case 8:
                    {
                        return "8";

                    }
                case 9:
                    {
                        return "9";

                    }
                case 10:
                    {
                        return "10";

                    }
                case 11:
                    {
                        return "Jack";

                    }
                case 12:
                    {
                        return "Queen";

                    }
                case 13:
                    {
                        return "King";

                    }
                case 14:
                    {
                        return "Ace";

                    }
                default:
                    {
                        return " ";
                    }

            }
        }
    }

    class Player
    {
        //public List<Card> hand { get; set; } 
        public double Wallet { get; set; }
        public List<Card> Hand { get; set; }
        public  int HighCard { get; set; }
        public  int HandComparisonEvaluator { get; set; }

    }


    class Card
    {
        public string Suit { get; set; }
        public int Cardnumber { get; set; }

        public Card(int cardnumber, string suit) //maybe use enums so can assign AKQJ and call them that but they have numbers as 11 12 13?
        //Check to see if cacn display that but have #  https://msdn.microsoft.com/en-us/library/vstudio/bb534336%28v=vs.100%29.aspx 
        //can do an array inside of a list of cards. 
        {
            Suit = suit;
            Cardnumber = cardnumber;
        }


    }

}
