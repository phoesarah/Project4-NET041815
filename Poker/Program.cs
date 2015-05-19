using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
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


    internal class Program
    {

        public static void Main(string[] args)
        {
            //make a player class wiht a hand property of a collection, and then a collection of collections for all the suits and ccards inside the suits. 
            List<Card> deck = CreateDeck();
            for (int i = 0; i < deck.Count; i++)
            {

                Console.WriteLine( + deck[i].Cardnumber + " of " + deck[i].Suit + "  " );
            }
            Console.ReadLine();
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
    }


    class Player
    {
        public List<Card> Hand { get; set; }
        public double Wallet { get; set; }
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

       
       
        
	
		 
	
      //  Card AceofSpades = new Card(14, "Spades");
       // Card TwoofSpades = new Card(2, "Spades");
       // Card ThreeofSpades = new Card(3, "Spades");

    }

       
    
}
