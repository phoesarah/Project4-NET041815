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

        
        public static void Main(string[] args)
        {
            

            //make a player class wiht a hand property of a collection, and then a collection of collections for all the suits and ccards inside the suits. 
            List<Card> deck = CreateDeck();
            // makesureworking(deck);              //this makes sure the deck worked. 
            Player player1 = new Player();
            Player player2 = new Player();
            Player player3 = new Player();
            Player player4 = new Player();
            player1.hand = player1.Deal(deck);
            deck = removehandfromdeck(player1.hand);
            player2.hand = player2.Deal(deck);
            deck = removehandfromdeck(player2.hand);
            player3.hand = player3.Deal(deck);
            deck = removehandfromdeck(player3.hand);
            player4.hand = player4.Deal(deck);
            makesureworking(player1.hand);
            makesureworking(player2.hand);
            makesureworking(player3.hand);
            makesureworking(player4.hand);
            
        }

        private static List<Card> removehandfromdeck(List<Card> player1Hand)
        {
            List<Card> newdeck = CreateDeck();
            newdeck = newdeck.Except(player1Hand).ToList();
            return newdeck;
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


        private static void makesureworking(List<Card> deck)
        {
            for (int i = 0; i < deck.Count; i++)
            {

                Console.WriteLine(cardnumber(deck[i].Cardnumber) + " of " + deck[i].Suit + "  ");
            }
            Console.ReadLine();
        }


    }

    class Player
    {
        public List<Card> hand { get; set; } 
        public double Wallet { get; set; }

        

        public  List<Card> Deal(List<Card> deck)
        {
            List<Card> Hand = hand;
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                
                int index = rand.Next(deck.Count());
                Hand.Add(deck[index]);
                deck.Remove(deck[index]);
            }

            return hand;


        }
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


    public enum cards
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    } 
    
}
