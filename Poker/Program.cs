using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{

    internal class Program
    {
        public static Random rand = new Random();

        public static void Main(string[] args)
        {

            //make a player class wiht a hand property of a collection, and then a collection of collections for all the suits and ccards inside the suits. 
            List<Card> deck = CreateDeck();

            Player player1 = new Player();
            Player player2 = new Player();
            Player player3 = new Player();
            Player player4 = new Player();

            player1.Hand = Deal(deck);
            deck = removehandfromdeck(player1.Hand, deck);
            player2.Hand = Deal(deck);
            deck = removehandfromdeck(player2.Hand, deck);
            player3.Hand = Deal(deck);
            deck = removehandfromdeck(player3.Hand, deck);
            player4.Hand = Deal(deck);

            Console.ReadLine();

            makesureworking(player1hand);
            makesureworking(player2hand);
            makesureworking(player3hand);
            makesureworking(player4hand);
            // makesureworking(deck);
            Console.ReadLine();

            CompareHands(player1hand, player2hand, player3hand, player4hand);

        }

        private static void CompareHands(List<Card> player1hand, List<Card> player2hand, List<Card> player3hand, List<Card> player4hand)
        {

            cardcombination player1has = getnameofhand(player1hand);
            cardcombination player2has = getnameofhand(player2hand);
            cardcombination player3has = getnameofhand(player3hand);
            cardcombination player4has = getnameofhand(player4hand);
            string winner;
            if (player1has > player2has && player1has > player3has && player1has > player4has)
            {
                winner = "Player 1 is the winner";

            }


            Console.WriteLine("Player 1 has: " + player1has);
            Console.WriteLine("Player 2 has: " + player2has);
            Console.WriteLine("Player 3 has: " + player3has);
            Console.WriteLine("Player 4 has: " + player4has);
            Console.ReadLine();
        }

        private static cardcombination getnameofhand(List<Card> playerhand)
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
                return cardcombination.StraightFlush;
            }

            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[0].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[0].Cardnumber == sequenced[3].Cardnumber || sequenced[4].Cardnumber == sequenced[1].Cardnumber && sequenced[4].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[4].Cardnumber == sequenced[3].Cardnumber)
            {
                return cardcombination.FourofaKind;

            }

            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[0].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[3].Cardnumber == sequenced[4].Cardnumber || sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[4].Cardnumber == sequenced[2].Cardnumber &&
            sequenced[3].Cardnumber == sequenced[4].Cardnumber)
            {
                return cardcombination.FullHouse;

            }

            else if (sequenced[1].Cardnumber == (sequenced[0].Cardnumber + 1) && sequenced[2].Cardnumber == (sequenced[1].Cardnumber + 1) &&
                sequenced[3].Cardnumber == (sequenced[2].Cardnumber + 1) && sequenced[4].Cardnumber == (sequenced[3].Cardnumber + 1))
            {
                return cardcombination.Straight;

            }


            else if (playerhand[0].Suit == playerhand[1].Suit && playerhand[0].Suit == playerhand[2].Suit &&
                     playerhand[0].Suit == playerhand[3].Suit &&
                     playerhand[0].Suit == playerhand[4].Suit)
                return cardcombination.Flush;

            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[0].Cardnumber == sequenced[2].Cardnumber ||
                sequenced[1].Cardnumber == sequenced[2].Cardnumber && sequenced[1].Cardnumber == sequenced[3].Cardnumber ||
                sequenced[2].Cardnumber == sequenced[3].Cardnumber && sequenced[2].Cardnumber == sequenced[4].Cardnumber)
            {
                return cardcombination.ThreeofaKind;

            }

            else if ((sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[2].Cardnumber == sequenced[3].Cardnumber) ||
                (sequenced[0].Cardnumber == sequenced[1].Cardnumber && sequenced[3].Cardnumber == sequenced[4].Cardnumber) ||
                (sequenced[1].Cardnumber == sequenced[2].Cardnumber && sequenced[3].Cardnumber == sequenced[4].Cardnumber))
            {
                return cardcombination.TwoPair;

            }

            else if (sequenced[0].Cardnumber == sequenced[1].Cardnumber || sequenced[1].Cardnumber == sequenced[2].Cardnumber ||
                sequenced[2].Cardnumber == sequenced[3].Cardnumber || sequenced[3].Cardnumber == sequenced[4].Cardnumber)
            {
                return cardcombination.Pair;

            }

            else return cardcombination.Nothing;
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




        private static void makesureworking(List<Card> deck)
        {
            for (int i = 0; i < deck.Count; i++)
            {

                Console.WriteLine(cardnumber(deck[i].Cardnumber) + " of " + deck[i].Suit + "  ");
            }
            Console.ReadLine();
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
                        return "Two";
                    }
                case 3:
                    {
                        return "Three";
                    }
                case 4:
                    {
                        return "Four";

                    }
                case 5:
                    {
                        return "Five";

                    }
                case 6:
                    {
                        return "Six";

                    }
                case 7:
                    {
                        return "Seven";

                    }
                case 8:
                    {
                        return "Eight";

                    }
                case 9:
                    {
                        return "Nine";

                    }
                case 10:
                    {
                        return "Ten";

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




        // public int CompareTo(Player other)
        // {
        // this.Deal() returns the hand   if returns 0 equal, if returns negative -1 then this is less than other, if plus, this is more than other
        // }
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
