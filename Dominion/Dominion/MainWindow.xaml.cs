using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace Dominion {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow(Game game,String lang) {
            InitializeComponent();
            myGame = game;
            this.language = lang;
            Initialize();
        }
        Game myGame;
        Button _button = null;
        Stopwatch stopWatch = new Stopwatch();
        /*************************
        1.)add tooltips and tab indecies and maybe tool tips which phase we are in
         * refershhand instead of window
         * set selected card to right dimentions. 
         * redue cards to eliminate white rightin at bottom of card
         * i18n button contents 
         * i18n status
         * i18n phase
        *************************/
        Player player;
        List<CardStack> stacks;
        string language;
        public string currentCard, phase, actiondone = "";
        public List<Image> victoryImage, currencyImage, handImage, actionImage, FieldImage, HilightedImages;
        public List<Button> currencyButton, victoryButton, handButton, actionButton, FieldButton;
        List<Card> specialusecards=new List<Card>();
        int totalplayers;
        public Locale loc;
        private void Play_Click(object sender, RoutedEventArgs e) {
            if (actiondone.Contains("Gain")) {
                StatusObject status = player.trashForGain(CardStackFromHilighted(currentCard).getCard());
                if (status.wasTrashedCorrectly()) {
                    GainCards();
                }
            } else if (actiondone.Equals("Discard Many")) {
                List<Card> cards = GetCardListFromHilighted();
                StatusObject status = player.discardCardsAndDrawSameAmount(cards);
                if (status.wasDiscardedAndDrawnSuccessfully()) {
                    ResetSpecialAction();
                }
            } else if (actiondone.Equals("Trash Copper")) {
                StatusObject status = player.trashACopperForCurrencyBonus(CardMother.Copper());
                if (status.needToTrashCoppersForCurrency()) {
                    ReadyToTrashCopper();
                    RefreshHand();
                    return;
                }
                ResetSpecialAction();
                RefreshWindow();
            } else if (actiondone.Equals("Trash Many")) {
                List<Card> cards = GetCardListFromHilighted();
                StatusObject status = player.trashCards(cards);
                if (status.wasTrashedCorrectly()) {
                    ResetSpecialAction();
                }
                if (status.needToTrashCards()) {
                    Trash();
                    return;
                }
            } else if (actiondone.Equals("Discard Deck")) {
                chancellor();
            }else if(actiondone.Equals("Mine")){
                List<Card> cards = GetCardListFromHilighted();
                StatusObject status = player.mineATreasureCard(cards[0]);
                if (status.wasMinedCorrectly()) {
                    ResetSpecialAction();
                }
                if (status.needToDisardDeck()) {
                    YesNo();
                    actiondone = "Mine";
                    Play.ToolTip = "Click To 'Upgrade' Currency";
                    RefreshHand();
                    return;
                }
            } else if(actiondone.Equals("Militia Many")){
                List<Card> cards = GetCardListFromHilighted();
                StatusObject status = player.militiaDiscardEffect(cards);
                if (!status.wasMilitiaPlayed()) {
                    if (status.needToContinueWithDelayedFunctions()) {
                        status = player.callDelayedFunctions();
                        if (status.wasMilitiaPlayed()) {
                            militia();
                            RefreshHand();
                            return;
                        }
                        if (!status.needToContinueWithDelayedFunctions()) {
                            ResetSpecialAction();
                        }
                    } else {
                        ResetSpecialAction();
                    }
                }
                
            }else if(actiondone.Equals("Spy Many")){
                List<Card> spyCards = new List<Card>();
                int count = 0;
                for (int i = 0; i < specialusecards.Count; i++) {
                    if (specialusecards[i] != null) {
                        if (StripImageSource(handImage[count].Source.ToString(), false).Contains("1")) {
                            spyCards.Add(CardFromString(StripImageSource(handImage[i].Source.ToString(), true)));
                        } else {
                            spyCards.Add(null);
                        }
                        count++;
                    } else {
                        spyCards.Add(null);
                    }
                }
                StatusObject status = player.keepOrDiscardSpiedCards(spyCards);
                if (status.playerSpiedSuccessfully()) {
                    specialusecards=new List<Card>();
                    ResetSpecialAction();
                    RefreshWindow();
                }

            }else if(actiondone.Equals("Thief Many")){
                List<Card> thiefTrash = new List<Card>();
                int count = 0;
                for (int i = 0; i <myGame.getPlayers().Count - 1; i++) {
                    if (specialusecards[2 * i] != null) {
                        if (specialusecards[2 * i + 1] != null) {
                            //player has 2 currency
                            if (StripImageSource(handImage[count].Source.ToString(), false).Contains("1") && StripImageSource(handImage[count + 1].Source.ToString(), false).Contains("1")) {
                                ResetHilightedCards();
                                return;
                            } else {
                                if (StripImageSource(handImage[count].Source.ToString(), false).Contains("1")) {
                                    thiefTrash.Add(CardFromString(StripImageSource(handImage[count].Source.ToString(), true)));
                                } else if (StripImageSource(handImage[count + 1].Source.ToString(), false).Contains("1")) {
                                    thiefTrash.Add(CardFromString(StripImageSource(handImage[count + 1].Source.ToString(), true)));
                                } else {
                                    ResetHilightedCards();
                                    return;
                                }
                            }
                            count += 2;
                        } else {
                            if (StripImageSource(handImage[count].Source.ToString(), false).Contains("1")) {
                                thiefTrash.Add(CardFromString(StripImageSource(handImage[count].Source.ToString(), true)));
                            }
                            //player only had 1 currency
                            count++;
                        }
                    } else {
                        //player has no currency
                        thiefTrash.Add(null);
                    }
                }
                StatusObject status = player.validateThiefStolenCards(thiefTrash);
                if (!status.selectTrashFromThief()) {
                    specialusecards = new List<Card>();
                    specialusecards=player.getPossibleCardsToKeepFromThief();
                    actiondone = "Pick Trash Many";
                    count = 0;
                    for (int i = 0; i < specialusecards.Count; i++) {
                        SetPicture(specialusecards[i].getName() + ".jpg", handImage[i]);
                        count = i+1;
                    }
                    for (int j = count; j < handImage.Count; j++) {
                        SetPicture("blank.jpg", handImage[j]);
                        handButton[j].Cursor = Cursors.Arrow;
                    }
                    End_Phase.IsEnabled = false;
                    End_Turn.IsEnabled = false;
                    HilightedImages = new List<Image>();
                    return;
                }
                return;
            }else if(actiondone.Equals("Pick Trash Many")){
                List<Card> thiefPick = new List<Card>();
                for (int i = 0; i < specialusecards.Count; i++) {
                    if (StripImageSource(handImage[i].Source.ToString(), false).Contains("1")) {
                        specialusecards.Add(CardFromString(StripImageSource(handImage[i].Source.ToString(), true)));
                    }
                }
                StatusObject status = player.keepCardsFromThief(thiefPick);
                if (status.needToKeepThief()) {
                    specialusecards = new List<Card>();
                    return;
                } else {
                    ResetSpecialAction();
                    RefreshWindow();
                    End_Phase.IsEnabled = true;
                }

            }else {
                StatusObject status = player.play(CardStackFromHilighted(currentCard).getCard());
                //SelectCardDescription.Content = status.wasPlayedProperly();
                Play.IsEnabled = false;
                if (status.trashForGainCheck()) {
                    TrashAndGain();
                    return;
                } else if (status.wasTrashedCorrectly()) {
                    GainCards();
                    return;
                } else if (status.needToDiscardCardsFromHandToDrawSameNumber()) {
                    actiondone = "Discard Many";
                    Play.Content = "Discard";
                    Play.ToolTip = "Discard Selected Cards";
                } else if (status.needToTrashCoppersForCurrency()) {
                    ReadyToTrashCopper();
                    RefreshHand();
                    return;
                } else if (status.needToTrashCards()) {
                    Trash();
                } else if (status.needToDisardDeck()) {
                    YesNo();
                    actiondone = "Discard Deck";
                    Play.ToolTip = "Click To Place Your Deck In The Discard Pile";
                    Buy.ToolTip = "Click To Not Place Your Deck In The Discard Pile";
                    RefreshHand();
                    return;
                } else if (status.needToMine()) {
                    actiondone = "Mine";
                    Play.ToolTip = "Click To 'Upgrade' Currency";
                    RefreshHand();
                } else if (status.canSpyOnDeck()) {
                    List<Card> SpiedCards = player.spyOnDecks();
                    specialusecards = SpiedCards;
                    actiondone = "Spy Many";
                    Play.Content = "Discard";
                    Play.ToolTip = "Discard Selected Cards";
                    End_Turn.IsEnabled = false;
                    //Buy.IsEnabled = false;
                    ResetHilightedCards();
                    int count = 0;
                    for(int i=0;i<myGame.getPlayers().Count;i++){
                        if (SpiedCards[i] != null) {
                            SetPicture(SpiedCards[i].getName() + ".jpg", handImage[count]);
                            count++;
                        }
                    }
                    for (int j = count; j < handImage.Count; j++) {
                        SetPicture("blank.jpg", handImage[j]);
                        handButton[j].Cursor = Cursors.Arrow;

                    }
                    int panelsize = 400 + (myGame.getPlayers().Count - 5) * 80;
                    stackpan.Width = panelsize;
                    return;
                }else if(status.selectTrashFromThief()){//1.)seperate thief cards from eachother by blanks
                    List<List<Card>> ThiefList = player.getThiefList();
                    int count = 0;
                    for (int i = 0; i < myGame.getPlayers().Count-1; i++) {
                        for (int j = 0; j < 2; j++) {
                            if (ThiefList[i].Count == 0||(ThiefList[i].Count<2&&j==1)) {
                                specialusecards.Add(null);
                            } else {
                                specialusecards.Add(ThiefList[i][j]);
                                SetPicture(ThiefList[i][j].getName() + ".jpg", handImage[count]);
                                count++;
                            }
                        }
                        //1.)put blank set card cursor to arrow
                        //count++;
                    }
                    if (count == 0) {
                        specialusecards=new List<Card>();
                        specialusecards.Add(null);
                        player.validateThiefStolenCards(specialusecards);
                        RefreshWindow();
                        ResetSpecialAction();
                        return;
                    }
                    actiondone = "Thief Many";
                    HilightedImages=new List<Image>();
                    for (int j = count; j < handImage.Count; j++) {
                        SetPicture("blank.jpg", handImage[j]);
                        handButton[j].Cursor = Cursors.Arrow;
                    }
                    End_Phase.IsEnabled = false;
                    End_Turn.IsEnabled = false;
                    return;
                }
                RefreshWindow();
                //1.)
                //    Play.Content = "Play";
                //   Play.ToolTip = "Plays The Selected Card";
            }
        }
        private void ResetSpecialAction() {
            actiondone = "";
            Play.Content = "Play";
            Buy.Content = "Buy";
            Play.ToolTip = "Plays The Selected Card";
            //1.)
            Buy.ToolTip = "";
            End_Turn.IsEnabled = true;
            RefreshWindow();
        }
        private void chancellor() {
            StatusObject status = player.discardDeck(true);
            if (status.wasDeckDiscardedCorrectly()) {
                ResetSpecialAction();
            }
            if (status.needToDisardDeck()) {
                YesNo();
                actiondone = "Discard Deck";
                Play.ToolTip = "Click To Place Your Deck In The Discard Pile";
                Buy.ToolTip = "Click To Not Place Your Deck In The Discard Pile";
                RefreshHand();
            }     
        }
        private void ReadyToTrashCopper() {
            YesNo();
            actiondone = "Trash Copper";
            Play.ToolTip = "Click To Discard A Copper";
            Buy.ToolTip = "Click To Not Discard A Copper";
        }
        private void militia() {
            actiondone = "Militia Many";
            Play.Content = "Discard";
            End_Phase.IsEnabled = false;
            End_Turn.IsEnabled = false;
            SetFieldardsToNo();
            SetHandButtonsToNormal();
        }
        private void Trash() {
            actiondone = "Trash Many";
            Play.Content = "Trash";
            Play.ToolTip = "Trashes The Selected Cards";
            End_Turn.IsEnabled = false;
            RefreshWindow();
        }
        private void YesNo() {
            Play.Content = "Yes";
            Buy.Content = "No";
            Play.IsEnabled = true;
            Buy.IsEnabled = true;
            End_Phase.IsEnabled = false;
            End_Turn.IsEnabled = false;
            SetHandButtonsToNo();
        }
        private void TrashAndGain() {
            actiondone = "TrashGain";
            Play.Content = "Trash";
            Play.ToolTip = "Trashes The Selected Card";
            End_Turn.IsEnabled = false;
            End_Phase.IsEnabled = false;
            RefreshWindow();
        }
        private void GainCards() {
            Play.Content = "Play";
            Play.ToolTip = "Plays The Selected Card";
            actiondone = "Gain";
            Buy.Content = "Gain";
            Gain_Label.Content = "Gain:            " + player.getCurrencyForGain();
            End_Phase.IsEnabled = false;
            End_Turn.IsEnabled = false;
            RefreshWindow();
            SetFieldCardsToNormal();
            SetHandButtonsToNo();
        }
        //1.) change refreshwindows to refreshhand if can
        private Boolean RefreshHand() {
            Boolean actioncard = false;
            Hand myHand = player.getHand();
            int length = myHand.getHand().Count();
            int panelsize = 400 + (length - 5) * 80;
            stackpan.Width = panelsize;
            for (int i = 0; i < length; i++) {
                if(CardFromString(myHand.getHand()[i].getEnglishName()).getPlayable()&&!CardFromString(myHand.getHand()[i].getEnglishName()).getName().Equals("Throne Room")){
                    actioncard = true;
                }
                string name = myHand.getHand()[i].getEnglishName() + ".jpg";
                SetPicture(name, handImage[i]);
            }
            HilightedImages = new List<Image>();
            return actioncard;
        }
        private void RefreshWindow() {
            ResetHilightedCards();
            
            _button = null;
            game_message.Text = myGame.getGameStatus();
            Play.IsEnabled = false;
            Boolean actioncard = RefreshHand();
            if ((!actioncard || player.getActionsLeft() == 0) && actiondone.Equals("") && !phase.Equals("Buy Phase")) {
                player.getCurrency();
                phase = "Buy Phase";
                End_Phase.IsEnabled = false;
            }
            //if (!currentCard.Equals("Throne Room")) {
            //}
            currentCard = "";
            Actions_Label.Content = player.getActionsLeft();
            Buys_Label.Content = player.getBuysLeft();
            Phase_Label.Content = phase;
            if (phase.Equals("Action Phase")) {
                SetHandButtonsToNormal();
                SetFieldardsToNo();
                Currency_Label.Content = player.getCurrency();
            } else {
                SetHandButtonsToNo();
                SetFieldCardsToNormal();
                Currency_Label.Content = player.getCurrencyValue();
            }
        }
        private void Buy_Click(object sender, RoutedEventArgs e) {
            if (actiondone.Equals("")) {
                Boolean work = false;
                int length = stacks.Count();
                CardStack cardstack = CardStackFromHilighted(currentCard);
                work = player.buy(cardstack);
                if (!work) {
                    Description.Content = "buy failed " + player.getCurrencyValue();
                } else {
                    Description.Content = "Buy Sucessful";
                    string name = currentCard + ".jpg";
                    SetPicture(name, Hand_Card);
                    if (cardstack.cardsRemaining() == 0) {
                        buyout();
                    }
                }
            } else if (actiondone.Equals("Gain")) {
                int length = stacks.Count();
                CardStack cardstack = CardStackFromHilighted(StripImageSource(HilightedImages[0].Source.ToString(), true));
                //1.) this should be checked by caleb
                if (cardstack.cardsRemaining() == 0) {
                    Description.Content = "None Left For Gain Why did it get here";
                    return;
                }
                //
                StatusObject status = player.gainCard(cardstack);
                if (status.getGainedProperly()) {
                    ResetHilightedCards();
                    End_Turn.IsEnabled = true;
                    Buy.Content = "Buy";
                    actiondone = "";
                    Gain_Label.Content = "";
                    if (cardstack.cardsRemaining() == 0) {
                        buyout();
                    }
                    if (status.trashForGainCheck()) {
                        TrashAndGain();
                        return;
                    } else if ((status.wasTrashedCorrectly())) {
                        GainCards();
                        return;
                    }
                } else {
                    return;
                }
            } else if (actiondone.Equals("Trash Copper")) {
                StatusObject status = player.trashACopperForCurrencyBonus(null);
                //.1) throne room play twice if choose no first time?
                if (status.needToTrashCoppersForCurrency()) {
                    ReadyToTrashCopper();
                } else {
                    ResetSpecialAction();
                }
            } else if (actiondone.Equals("Discard Deck")) {
                chancellor();
            }
            RefreshWindow();
            if (myGame.isGameOver()) {
                MessageBox.Show(myGame.getGameOverStatus());
                this.Close();
            }
        }
        private void buyout() {
            for (int i = 0; i < FieldImage.Count; i++) {
                if (StripImageSource(FieldImage[i].Source.ToString(), true).Equals(StripImageSource(HilightedImages[HilightedImages.Count - 1].Source.ToString(), true))) {
                    FieldButton[i].IsEnabled = false;
                    FieldButton.Remove(FieldButton[i]);
                    FieldImage.Remove(FieldImage[i]);

                }
            }
        }
        private void SetFieldCardsToNormal() {
            for (int i = 0; i < FieldButton.Count(); i++) {
                FieldButton[i].Cursor = Cursors.Hand;
            }
        }
        private void SetFieldardsToNo() {
            for (int i = 0; i < FieldButton.Count(); i++) {
                FieldButton[i].Cursor = Cursors.No;
            }
        }
        private void SetHandButtonsToNormal() {
            for (int i = 0; i < handButton.Count(); i++) {
                handButton[i].Cursor = Cursors.Hand;
            }
        }
        private void SetHandButtonsToNo() {
            for (int i = 0; i < handButton.Count(); i++) {
                handButton[i].Cursor = Cursors.No;
            }
        }
        private void End_Turn_Click(object sender, RoutedEventArgs e) {
            player.cleanUp();
            this.Hide();
            PrepScreen prep = new PrepScreen(myGame.nextPlayerName(), this);
            prep.Show();
            player = myGame.nextTurnPlayer();


            //   SetPicture("blank.jpg", Selected_Card);
            actiondone = "";
            phase = "Action Phase";
            Player_Label.Content = player.getName() + "'s";
            Phase_Label.Content = phase;
            End_Phase.IsEnabled = true;
            player.getCurrency();
            
          //  RefreshWindow();
            ResetHilightedCards();
            Turn_Label.Content = myGame.getTurnsPassed();
            RefreshWindow();
            StatusObject status = player.callDelayedFunctions();
            if (status.wasMilitiaPlayed()) {
                militia();
                //actiondone = "Militia Many";
                //Play.Content = "Discard";
                //End_Phase.IsEnabled = false;
                //End_Turn.IsEnabled = false;
                //SetFieldardsToNo();
                //SetHandButtonsToNormal();   
                return;
            }
            
        }
        private void EndPhase_Click(object sender, RoutedEventArgs e) {
            //1.)set tool tips based on phase
            if (actiondone.Equals("Trash Many")) {
                player.trashCards(new List<Card>());
                ResetSpecialAction();
            }else if(actiondone.Equals("Spy Many")){
                ResetSpecialAction();
                RefreshHand();
            } else if (actiondone.Equals("Pick Thief Many")) {
                ResetSpecialAction();
                RefreshHand();
                specialusecards = new List<Card>();
           }
            Buy.IsEnabled = false;
            phase = "Buy Phase";
            Phase_Label.Content = phase;
            ResetHilightedCards();
            SetPicture("blank.jpg", Selected_Card);
            End_Phase.IsEnabled = false;
            SetHandButtonsToNo();
            SetFieldCardsToNormal();
            
        }
        private void CurrencyImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, currencyButton, currencyImage, false);
        }
        private void VictoryImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, victoryButton, victoryImage, false);
        }
        private void HandImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, handButton, handImage, true);
        }
        private void ActionImage_Click(object sender, RoutedEventArgs e) {
            Image_Click(sender, actionButton, actionImage, false);
        }
        private void Image_Click(object sender, List<Button> buttons, List<Image> images, Boolean handcard) {//somehow sometimes _button is getting set when a button in unhilighted at some point
            Button obj = (Button)sender;
            if (obj.Cursor == Cursors.Hand) {
                if (_button == obj) {
                    stopWatch.Stop();
                    if (stopWatch.Elapsed.Milliseconds < 300 && stopWatch.Elapsed.Ticks < 10000000) {
                        _button = null;
                        if (handcard) {
                            Play_Click(null, null);
                        } else {
                            Buy_Click(null, null);
                        }
                    } else {
                        _button = null;
                        Image image = GetImageFromButton(obj, buttons, images);
                        HilightCard(image, handcard);
                    }
                } else {
                    stopWatch.Reset();
                    stopWatch.Start();
                    _button = obj;
                    Image image = GetImageFromButton(obj, buttons, images);
                    HilightCard(image, handcard);
                }
            }
        }
        private Image GetImageFromButton(object obj, List<Button> buttons, List<Image> images) {
            for (int i = 0; i < buttons.Count(); i++) {
                if (buttons[i] == obj) {
                    Boolean stripmore=false;
                    if (images[i].Source.ToString().Contains("i.jpg")) {
                        stripmore = true;
                    }
                        currentCard = StripImageSource(images[i].Source.ToString(), stripmore);
                    return images[i];
                }
            }
            return null;
        }
        private void HilightImage(Image image) {
            String card = StripImageSource(image.Source.ToString(), false);
            if (!card.Contains("1")) {
                if (language.Equals("en_US")) {
                    SetPicture(card + ".jpg", Selected_Card);
                } else {
                    SetPicture(card + "i.jpg", Selected_Card);
                }
                card = card + "1.jpg";
                SetPicture(card, image);
            }
        }
        private void SetSelected() {
            List<Card> cards = GetCardListFromHilighted();
            Card card = cards[cards.Count - 1];
            SelectCardDescription.Text = card.getDescription();
            SelectCardName.Text = card.getName();
            SelectCardType.Text = card.getName();
        }
        private void UnHilightImage(Image image) {
            String card = StripImageSource(image.Source.ToString(), true) + ".jpg";
            SetPicture(card, image);
            SelectCardDescription.Text = "";
            SelectCardName.Text = "";
            SetPicture("blank.jpg", Selected_Card);
        }
        private List<Card> GetCardListFromHilighted() {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < HilightedImages.Count; i++) {
                cards.Add(CardFromString(StripImageSource(HilightedImages[i].Source.ToString(), true)));
            }
            return cards;
        }
        private void HilightCard(Image image, Boolean ishandCard) {
            //1.)buy/play work in this case?
            if (HilightedImages.Contains(image)) {
                Buy.IsEnabled = false;
                HilightedImages.Remove(image);
                if (HilightedImages.Count == 0) {
                    Play.IsEnabled = false;
                }
                UnHilightImage(image);
                return;
            }
            if (!actiondone.Contains("Many")) {
                if (HilightedImages.Count != 0) {
                    //1.)extend for multiple selected 
                    ResetHilightedCards();
                }
            }
            HilightedImages.Add(image);

            for (int i = 0; i < HilightedImages.Count; i++) {
                HilightImage(HilightedImages[i]);
            }
            if (handButton[0].Cursor == Cursors.Hand) {
                Play.IsEnabled = true;
                Buy.IsEnabled = false;
            } else {
                Buy.IsEnabled = true;
                Play.IsEnabled = false;
            }
            //1.) fit all on screen
            //List<Card> cards = GetCardListFromHilighted();
            //Card card = cards[cards.Count - 1];
            SetSelected();
            //SelectCardDescription.Content = card.getDescription();
            //SelectCardName.Content = card.getName();
        }
        /*
         * currently gets send one string going to need to make funciton to get list of strings that are hilighted for thef card
         * returns all cardstacks that are currently selected
         */
        private Card CardFromString(String str) {
            int length = stacks.Count();
            Card card = null;
            for (int i = 0; i < length; i++) {
                if (stacks[i].getCard().getEnglishName().Equals(str)) {
                    card = stacks[i].getCard();
                }
            }
            return card;
        }

        private CardStack CardStackFromHilighted(String str) {
            int length = stacks.Count();
            for (int i = 0; i < length; i++) {
                if (stacks[i].getCard().getEnglishName().Equals(str)) {
                    return stacks[i];
                }
            }
            return null;
        }

        private string StripImageSource(string str, Boolean isHilighted) {
            int remove = 46;
            if (isHilighted) {
                remove = 47;
            }
            int length = str.Count();
            return str.Substring(42, length - remove);
        }
        private void SetPicture(string str, Image pic) {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(str, UriKind.RelativeOrAbsolute);
            image.EndInit();
            pic.Source = image;
        }
        private void ResetHilightedCards() {
            Buy.IsEnabled = false;
            Play.IsEnabled = false;
            for (int i = 0; i < HilightedImages.Count; i++) {
                UnHilightImage(HilightedImages[i]);
            }
            SetPicture("blank.jpg", Selected_Card);
            HilightedImages = new List<Image>();
        }
        /*********************************************************
         add playerinit and change start_click to initializegame
         ********************************************************/
        private void Save_Game(object sender, RoutedEventArgs e) {
            myGame.Save();
        }
        private void Initialize() {
            player = myGame.getCurrentPlayer();
            stacks = myGame.getBuyables();
            currentCard = "";
            phase = "Action Phase";
            totalplayers = myGame.getPlayers().Count();
            InitializeButtonImages();
            Player_Label.Content = player.getName() + "'s";
            int size = stacks.Count(), i;
            for (i = 0; i < currencyButton.Count; i++) {
                string name = stacks[i].getCard().getEnglishName() + ".jpg";
                SetPicture(name, currencyImage[i]);
            }
            for (i = 0; i < victoryButton.Count; i++) {
                string name = stacks[i + currencyButton.Count].getCard().getEnglishName() + ".jpg";
                SetPicture(name, victoryImage[i]);
            }
            for (i = 0; i < actionButton.Count; i++) {
                string name = stacks[i + currencyButton.Count + victoryButton.Count].getCard().getEnglishName() + ".jpg";
                SetPicture(name, actionImage[i]);
            }
            if (language.Equals("en_US")) {
               /* var margin = SelectCardName.Margin;
                margin.Top = 10;
                SelectCardName.Margin = margin;
                margin = SelectCardDescription.Margin;
                margin.Top = 325;
                SelectCardDescription.Margin = margin;
                SelectCardName.Foreground = Brushes.White;
                SelectCardType.Foreground = Brushes.Transparent;
                SelectCardDescription.Foreground = Brushes.White;*/
                SelectCardName.Foreground = Brushes.Transparent;
                SelectCardDescription.Foreground=Brushes.Transparent;
                SelectCardType.Foreground=Brushes.Transparent;
            }
            player.getCurrency();
            RefreshWindow();

            loc = new Locale(language.Substring(0,2),language.Substring(3,2));
            Internationalizer.setLocale(loc);
           // Phase_Label.Content = Internationalizer.getMessage("Buy Phase");
        }
        private void InitializeButtonImages() {
            //MainGrid
          //  myGame.randomizeBuyables();
            HilightedImages = new List<Image>();
            victoryButton = new List<Button>();
            currencyButton = new List<Button>();
            actionButton = new List<Button>();
            currencyImage = new List<Image>();
            victoryImage = new List<Image>();
            actionImage = new List<Image>();
            handImage = new List<Image>();
            handButton = new List<Button>();
            FieldImage = new List<Image>();
            FieldButton = new List<Button>();
            FieldImage.Add(VictoryImage1);
            FieldImage.Add(VictoryImage2);
            FieldImage.Add(VictoryImage3);
            FieldImage.Add(VictoryImage4);
            FieldImage.Add(CurrencyImage1);
            FieldImage.Add(CurrencyImage2);
            FieldImage.Add(CurrencyImage3);
            FieldImage.Add(ActionImage1);
            FieldImage.Add(ActionImage2);
            FieldImage.Add(ActionImage3);
            FieldImage.Add(ActionImage4);
            FieldImage.Add(ActionImage5);
            FieldImage.Add(ActionImage6);
            FieldImage.Add(ActionImage7);
            FieldImage.Add(ActionImage8);
            FieldImage.Add(ActionImage9);
            FieldImage.Add(ActionImage10);
            FieldButton.Add(VictoryButton1);
            FieldButton.Add(VictoryButton2);
            FieldButton.Add(VictoryButton3);
            FieldButton.Add(VictoryButton4);
            FieldButton.Add(CurrencyButton1);
            FieldButton.Add(CurrencyButton2);
            FieldButton.Add(CurrencyButton3);
            FieldButton.Add(ActionButton1);
            FieldButton.Add(ActionButton2);
            FieldButton.Add(ActionButton3);
            FieldButton.Add(ActionButton4);
            FieldButton.Add(ActionButton5);
            FieldButton.Add(ActionButton6);
            FieldButton.Add(ActionButton7);
            FieldButton.Add(ActionButton8);
            FieldButton.Add(ActionButton9);
            FieldButton.Add(ActionButton10);
            victoryImage.Add(VictoryImage1);
            victoryImage.Add(VictoryImage2);
            victoryImage.Add(VictoryImage3);
            victoryImage.Add(VictoryImage4);
            victoryButton.Add(VictoryButton1);
            victoryButton.Add(VictoryButton2);
            victoryButton.Add(VictoryButton3);
            victoryButton.Add(VictoryButton4);
            currencyImage.Add(CurrencyImage1);
            currencyImage.Add(CurrencyImage2);
            currencyImage.Add(CurrencyImage3);
            currencyButton.Add(CurrencyButton1);
            currencyButton.Add(CurrencyButton2);
            currencyButton.Add(CurrencyButton3);
            actionImage.Add(ActionImage1);
            actionImage.Add(ActionImage2);
            actionImage.Add(ActionImage3);
            actionImage.Add(ActionImage4);
            actionImage.Add(ActionImage5);
            actionImage.Add(ActionImage6);
            actionImage.Add(ActionImage7);
            actionImage.Add(ActionImage8);
            actionImage.Add(ActionImage9);
            actionImage.Add(ActionImage10);
            actionButton.Add(ActionButton1);
            actionButton.Add(ActionButton2);
            actionButton.Add(ActionButton3);
            actionButton.Add(ActionButton4);
            actionButton.Add(ActionButton5);
            actionButton.Add(ActionButton6);
            actionButton.Add(ActionButton7);
            actionButton.Add(ActionButton8);
            actionButton.Add(ActionButton9);
            actionButton.Add(ActionButton10);
            handImage.Add(HandImage1);
            handImage.Add(HandImage2);
            handImage.Add(HandImage3);
            handImage.Add(HandImage4);
            handImage.Add(HandImage5);
            handImage.Add(HandImage6);
            handImage.Add(HandImage7);
            handImage.Add(HandImage8);
            handImage.Add(HandImage9);
            handImage.Add(HandImage10);
            handImage.Add(HandImage11);
            handImage.Add(HandImage12);
            handImage.Add(HandImage13);
            handImage.Add(HandImage14);
            handImage.Add(HandImage15);
            handImage.Add(HandImage16);
            handImage.Add(HandImage17);
            handImage.Add(HandImage18);
            handImage.Add(HandImage19);
            handImage.Add(HandImage20);
            handImage.Add(HandImage21);
            handImage.Add(HandImage22);
            handImage.Add(HandImage23);
            handImage.Add(HandImage24);
            handImage.Add(HandImage25);
            handImage.Add(HandImage26);
            handImage.Add(HandImage27);
            handImage.Add(HandImage28);
            handImage.Add(HandImage29);
            handImage.Add(HandImage30);
            handImage.Add(HandImage31);
            handImage.Add(HandImage32);
            handImage.Add(HandImage33);
            handImage.Add(HandImage34);
            handImage.Add(HandImage35);
            handImage.Add(HandImage36);
            handImage.Add(HandImage37);
            handImage.Add(HandImage38);
            handImage.Add(HandImage39);
            handImage.Add(HandImage40);
            handImage.Add(HandImage41);
            handImage.Add(HandImage42);
            handImage.Add(HandImage43);
            handImage.Add(HandImage44);
            handImage.Add(HandImage45);
            handImage.Add(HandImage46);
            handImage.Add(HandImage47);
            handImage.Add(HandImage48);
            handImage.Add(HandImage49);
            handImage.Add(HandImage50);
            handButton.Add(HandButton1);
            handButton.Add(HandButton2);
            handButton.Add(HandButton3);
            handButton.Add(HandButton4);
            handButton.Add(HandButton5);
            handButton.Add(HandButton6);
            handButton.Add(HandButton7);
            handButton.Add(HandButton8);
            handButton.Add(HandButton9);
            handButton.Add(HandButton10);
            handButton.Add(HandButton11);
            handButton.Add(HandButton12);
            handButton.Add(HandButton13);
            handButton.Add(HandButton14);
            handButton.Add(HandButton15);
            handButton.Add(HandButton16);
            handButton.Add(HandButton17);
            handButton.Add(HandButton18);
            handButton.Add(HandButton19);
            handButton.Add(HandButton20);
            handButton.Add(HandButton21);
            handButton.Add(HandButton22);
            handButton.Add(HandButton23);
            handButton.Add(HandButton24);
            handButton.Add(HandButton25);
            handButton.Add(HandButton26);
            handButton.Add(HandButton27);
            handButton.Add(HandButton28);
            handButton.Add(HandButton29);
            handButton.Add(HandButton30);
            handButton.Add(HandButton31);
            handButton.Add(HandButton32);
            handButton.Add(HandButton33);
            handButton.Add(HandButton34);
            handButton.Add(HandButton35);
            handButton.Add(HandButton36);
            handButton.Add(HandButton37);
            handButton.Add(HandButton38);
            handButton.Add(HandButton39);
            handButton.Add(HandButton40);
            handButton.Add(HandButton41);
            handButton.Add(HandButton42);
            handButton.Add(HandButton43);
            handButton.Add(HandButton44);
            handButton.Add(HandButton45);
            handButton.Add(HandButton46);
            handButton.Add(HandButton47);
            handButton.Add(HandButton48);
            handButton.Add(HandButton49);
            handButton.Add(HandButton50);
        }
        private void End_Game_Click(object sender, RoutedEventArgs e) {
        }
        /*  private void End_Game_Click(object sender, RoutedEventArgs e) {
              myGame.getPlayers()[0].setVictoryPts();
              int highscore = myGame.getPlayers()[0].getVictoryPts();
              List<int> highplayer = new List<int>();
              highplayer.Add(0);
              for (int i = 1; i < totalplayers; i++) {
                  int score;
                  myGame.getPlayers()[i].setVictoryPts();
                  score = myGame.getPlayers()[i].getVictoryPts();
                  if (score > highscore) {
                      myGame.getPlayers()[i].setVictoryPts();
                      highscore = myGame.getPlayers()[i].getVictoryPts();
                      highplayer = new List<int>();
                      highplayer.Add(myGame.getPlayers()[i].getID());
                  } else if (score == highscore) {
                      highplayer.Add(myGame.getPlayers()[i].getID());
                  }
              }
              if (highplayer.Count == 1) {
                  MessageBox.Show(myGame.getPlayers()[highplayer[0]].getName() + " wins with " + highscore + " victory points");
              } else {
                  String victoryPlayers = myGame.getPlayers()[highplayer[0]].getName();
                  for (int i = 1; i < totalplayers; i++) {
                      victoryPlayers += " and " + myGame.getPlayers()[highplayer[i]].getName();
                  }
                  MessageBox.Show(victoryPlayers + " tied with " + highscore + " victory points");
              }
              for (int i = 0; i < totalplayers; i++) {
                  myGame.getPlayers()[i].setVictoryPts();
                  int score = myGame.getPlayers()[i].getVictoryPts();
                  int thisplayer = myGame.getPlayers()[i].getID();
                  MessageBox.Show(myGame.getPlayers()[thisplayer].getName() + " has " + score + " victory points");
              }
              this.Close();
          }*/
        //1.)shuffle cards well (like from start)
        //
        //http://stackoverflow.com/questions/4151380/wpf-image-control-with-click-event
        //http://stackoverflow.com/questions/5090435/image-button-on-visual-studio-2010
    }
}
