using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    /*
         * For funcNum the following are defined
         * 1: drawing only. Such as the smithy card.
         * 2: is drawing and action, such as the laboratory card
         * 3: is drawing and action and buys, such as the market card
         */
    //Card(int type, int cash, int buy, int actions, int vict, int draw, int funcNum,string cardName,string desc,int cost)
    class CardMother
    {
        public static Card Festival()
        {
            return new Card(2, 2, 1, 2, 0, 0, 6, "Festival", "+2 Actions\n+1\nBuy\n+2 Currency", 5);
        }

        public static Card Village()
        {
            return new Card(2, 0, 0, 2, 0, 1, 5, "Village", "+1 Card\n+2 Actions", 3);
        }

        public static Card Woodcutter()
        {
            return new Card(2, 2, 1, 0, 0, 0, 4, "Woodcutter", "+1 Buy\n+2 Currencys", 3);
        }

        public static Card Smithy()
        {
            return new Card(2, 0, 0, 0, 0, 3, 1, "Smithy", "+3 Cards", 4);
        }

        public static Card Estate()
        {
            return new Card(0, 0, 0, 0, 1, 0, 0, "Estate", "1 Victory Point", 2);
        }

        public static Card Duchy()
        {
            return new Card(0, 0, 0, 0, 3, 0, 0, "Duchy", "3 Victory Points", 5);
        }

        public static Card Province()
        {
            return new Card(0, 0, 0, 0, 6, 0, 0, "Province", "6 Victory Points", 8);
        }

        public static Card Copper()
        {
            return new Card(1, 1, 0, 0, 0, 0, 0, "Copper", "1 Currency Value", 0);
        }

        public static Card Silver()
        {
            return new Card(1, 2, 0, 0, 0, 0, 0, "Silver", "2 Currency Value", 3);
        }

        public static Card Gold()
        {
            return new Card(1, 3, 0, 0, 0, 0, 0, "Gold", "3 Currency Value", 6);
        }

        public static Card Laboratory()
        {
            return new Card(2, 0, 0, 1, 0, 2, 2, "Laboratory", "+2 Cards\n+1 Action", 5);
        }

        public static Card Market()
        {
            return new Card(2, 1, 1, 1, 0, 1, 3, "Market", "+1 Card\n+1 Action\n+1 Buy\n+1 Currency", 5);
        }

        public static Card Curse()
        {
            return new Card(0, 0, 0, 0, -1, 0, 0, "Curse", "-1 Victory Point", 0);
        }

        public static Card Witch()
        {
            return new Card(2, 0, 0, 0, 0, 2, 7, "Witch", "+2 Cards.\nEach other player gains a Curse card.", 5);
        }

        public static Card Remodel()
        {
            return new Card(2, 0, 0, 0, 0, 0, 8, "Remodel", "Trash a card from your hand.\nGain a a card costing up to 2 Currency\nmore than the trashed card", 4);
        }

        public static Card Feast()
        {
            return new Card(2, 0, 0, 0, 0, 0, 9, "Feast", "Trash this card and gain a card costing up to 5 Currency", 4);
        }

        public static Card Workshop()
        {
            return new Card(2, 0, 0, 0, 0, 0, 10, "Workshop", "Gain a card costing up to 4 Currency", 3);
        }

        public static Card ThroneRoom()
        {
            return new Card(2, 0, 0, 1, 0, 0, 11, "Throne Room", "Choose and Action card in your hand. \nPlay it twice", 4);
        }

        public static Card Cellar()
        {
            return new Card(2, 0, 0, 1, 0, 0, 12, "Cellar", "+1 Action\nDiscard any number of cards.\n+1 Card per card discarded", 2);
        }

        public static Card Moneylender()
        {
            return new Card(2, 0, 0, 0, 0, 0, 13, "Moneylender", "Trash a copper from your hand.\nIf you do +3 Currency", 4);
        }

        public static Card Chapel()
        {
            return new Card(2, 0, 0, 0, 0, 0, 14, "Chapel", "Trash up to 4 cards from your hand", 2);
        }

        public static Card Chancellor()
        {
            return new Card(2, 2, 0, 0, 0, 0, 15, "Chancellor", "+ 2 currency\nYou may immediatly put your\ndeck into your discard pile ", 3);
        }

        public static Card Militia()
        {
            return new Card(2, 2, 0, 0, 0, 0, 16, "Militia", "+2 Currency\nAll other players immediately discard down to three cards.", 4);
        }

        public static Card Bureaucrat()
        {
            return new Card(2, 0, 0, 0, 0, 0, 17, "Bureaucrat", "Gain a Silver card;\nput it on top of your deck.\nEach other player reveals a Victory card\nfrom his hand and puts it on his deck\n(or reveals a hand with no Victory cards).", 4);
        }

        public static Card Gardens()
        {
            return new Card(0, 0, 0, 0, 0, 0, 0, "Gardens", "Worth 1 Victory Point for every 10 cards in your deck (rounded down).", 4);
        }
        public static Card Mine()
        {
            return new Card(2, 0, 0, 0, 0, 0, 18, "Mine", "Trash a Treasure card from your hand. Gain a Treasure card costing up to 3 Coins more; put it into your hand.", 5);
        }

        public static List<Card> allBuyableCards()
        {
            List<Card> ret = new List<Card>();
            ret.Add(CardMother.Bureaucrat());
            ret.Add(CardMother.Cellar());
            ret.Add(CardMother.Chancellor());
            ret.Add(CardMother.Chapel());
            ret.Add(CardMother.Feast());
            ret.Add(CardMother.Festival());
            ret.Add(CardMother.Gardens());
            ret.Add(CardMother.Laboratory());
            ret.Add(CardMother.Market());
            ret.Add(CardMother.Moneylender());
            ret.Add(CardMother.Remodel());
            ret.Add(CardMother.Smithy());
            ret.Add(CardMother.ThroneRoom());
            ret.Add(CardMother.Village());
            ret.Add(CardMother.Witch());
            ret.Add(CardMother.Woodcutter());
            ret.Add(CardMother.Workshop());
            ret.Add(CardMother.Mine());
            return ret;
        }
    }
}
