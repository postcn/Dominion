using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    /*
         * For funcNum the following are definedf
         * 1: drawing only. Such as the smithy card.
         * 2: is drawing and action, such as the laboratory card
         * 3: is drawing and action and buys, such as the market card
         */
    //Card(int type, int cash, int buy, int actions, int vict, int draw, int funcNum,string cardName,string desc,int cost)
    class CardMother
    {
        public static Card Festival()
        {
            return new Card(2, 2, 1, 2, 0, 0, 6, Internationalizer.getMessage("Festival"), Internationalizer.getMessage("FestivalDesc"), 5);
        }

        public static Card Village()
        {
            return new Card(2, 0, 0, 2, 0, 1, 5, Internationalizer.getMessage("Village"), Internationalizer.getMessage("VillageDesc"), 3);
        }

        public static Card Woodcutter()
        {
            return new Card(2, 2, 1, 0, 0, 0, 4, Internationalizer.getMessage("Woodcutter"), Internationalizer.getMessage("WoodcutterDesc"), 3);
        }

        public static Card Smithy()
        {
            return new Card(2, 0, 0, 0, 0, 3, 1, Internationalizer.getMessage("Smithy"), Internationalizer.getMessage("SmithyDesc"), 4);
        }

        public static Card Estate()
        {
            return new Card(0, 0, 0, 0, 1, 0, 0, Internationalizer.getMessage("Estate"), Internationalizer.getMessage("EstateDesc"), 2);
        }

        public static Card Duchy()
        {
            return new Card(0, 0, 0, 0, 3, 0, 0, Internationalizer.getMessage("Duchy"), Internationalizer.getMessage("DuchyDesc"), 5);
        }

        public static Card Province()
        {
            return new Card(0, 0, 0, 0, 6, 0, 0, Internationalizer.getMessage("Province"), Internationalizer.getMessage("ProvinceDesc"), 8);
        }

        public static Card Copper()
        {
            return new Card(1, 1, 0, 0, 0, 0, 0, Internationalizer.getMessage("Copper"), Internationalizer.getMessage("CopperDesc"), 0);
        }

        public static Card Silver()
        {
            return new Card(1, 2, 0, 0, 0, 0, 0, Internationalizer.getMessage("Silver"), Internationalizer.getMessage("SilverDesc"), 3);
        }

        public static Card Gold()
        {
            return new Card(1, 3, 0, 0, 0, 0, 0, Internationalizer.getMessage("Gold"), Internationalizer.getMessage("GoldDesc"), 6);
        }

        public static Card Laboratory()
        {
            return new Card(2, 0, 0, 1, 0, 2, 2, Internationalizer.getMessage("Laboratory"), Internationalizer.getMessage("LaboratoryDesc"), 5);
        }

        public static Card Market()
        {
            return new Card(2, 1, 1, 1, 0, 1, 3, Internationalizer.getMessage("Market"), Internationalizer.getMessage("MarketDesc"), 5);
        }

        public static Card Curse()
        {
            return new Card(0, 0, 0, 0, -1, 0, 0, Internationalizer.getMessage("Curse"), Internationalizer.getMessage("CurseDesc"), 0);
        }

        public static Card Witch()
        {
            return new Card(2, 0, 0, 0, 0, 2, 7, Internationalizer.getMessage("Witch"), Internationalizer.getMessage("WitchDesc"), 5);
        }

        public static Card Remodel()
        {
            return new Card(2, 0, 0, 0, 0, 0, 8, Internationalizer.getMessage("Remodel"), Internationalizer.getMessage("RemodelDesc"), 4);
        }

        public static Card Feast()
        {
            return new Card(2, 0, 0, 0, 0, 0, 9, Internationalizer.getMessage("Feast"), Internationalizer.getMessage("FeastDesc"), 4);
        }

        public static Card Workshop()
        {
            return new Card(2, 0, 0, 0, 0, 0, 10, Internationalizer.getMessage("Workshop"), Internationalizer.getMessage("WorkshopDesc"), 3);
        }

        public static Card ThroneRoom()
        {
            return new Card(2, 0, 0, 1, 0, 0, 11, Internationalizer.getMessage("ThroneRoom"), Internationalizer.getMessage("ThroneRoomDesc"), 4);
        }

        public static Card Cellar()
        {
            return new Card(2, 0, 0, 1, 0, 0, 12, Internationalizer.getMessage("Cellar"), Internationalizer.getMessage("CellarDesc"), 2);
        }

        public static Card Moneylender()
        {
            return new Card(2, 0, 0, 0, 0, 0, 13, Internationalizer.getMessage("Moneylender"), Internationalizer.getMessage("MoneylenderDesc"), 4);
        }

        public static Card Chapel()
        {
            return new Card(2, 0, 0, 0, 0, 0, 14, Internationalizer.getMessage("Chapel"), Internationalizer.getMessage("ChapelDesc"), 2);
        }

        public static Card Chancellor()
        {
            return new Card(2, 2, 0, 0, 0, 0, 15, Internationalizer.getMessage("Chancellor"), Internationalizer.getMessage("ChancellorDesc"), 3);
        }

        public static Card Militia()
        {
            return new Card(2, 2, 0, 0, 0, 0, 16, Internationalizer.getMessage("Militia"), Internationalizer.getMessage("MilitiaDesc"), 4);
        }

        public static Card Bureaucrat()
        {
            return new Card(2, 0, 0, 0, 0, 0, 17, Internationalizer.getMessage("Bureaucrat"), Internationalizer.getMessage("BureaucratDesc"), 4);
        }

        public static Card Gardens()
        {
            return new Card(0, 0, 0, 0, 0, 0, 0, Internationalizer.getMessage("Gardens"), Internationalizer.getMessage("GardensDesc"), 4);
        }
        public static Card Mine()
        {
            return new Card(2, 0, 0, 0, 0, 0, 18, Internationalizer.getMessage("Mine"), Internationalizer.getMessage("MineDesc"), 5);
        }

        public static Card CouncilRoom()
        {
            return new Card(2, 0, 1, 0, 0, 4, 19, Internationalizer.getMessage("CouncilRoom"), Internationalizer.getMessage("CouncilRoomDesc"), 5);
        }

        public static Card Spy()
        {
            return new Card(2, 0, 0, 1, 0, 1, 20, Internationalizer.getMessage("Spy"), Internationalizer.getMessage("SpyDesc"), 4);
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
            ret.Add(CardMother.CouncilRoom());
            ret.Add(CardMother.Spy());
            return ret;
        }
    }
}
