using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dominion{
    [TestFixture()]
    class MainWindowTest {
        /// <summary>
        /// Tests that a mainwindow successfully initializes
        /// </summary>
        [Test()]
        public void testInintializes() {
            //MainWindow main=null;
           // PrepScreen prep = new PrepScreen("", main);
           // Image hi;
           // SetPicture("blank.jpg", hi);

//            MainWindow main = new MainWindow();
            MainWindow main = new MainWindow(new Game(1));
            Assert.AreEqual(main.currentCard, "");
            Assert.AreEqual(main.phase,"Buy Phase");
            Assert.AreEqual(main.victoryImage.Count(),4);
            Assert.AreEqual(main.currencyImage.Count(), 3);
            Assert.AreEqual(main.actionImage.Count(), 10);
            Assert.AreEqual(main.handImage.Count(), 50);
            Assert.AreEqual(main.FieldImage.Count(), 17);
            Assert.AreEqual(main.victoryButton.Count(), 4);
            Assert.AreEqual(main.currencyButton.Count(), 3);
            Assert.AreEqual(main.actionButton.Count(), 10);
            Assert.AreEqual(main.handButton.Count(), 50);
            Assert.AreEqual(main.FieldButton.Count(), 17);
            for (int i=0;i<50;i++){
                Assert.AreEqual(main.handImage[i].Cursor,Cursors.No);
            }
            for (int i = 0; i < 17; i++) {
                Assert.AreEqual(main.FieldImage[i].Cursor, Cursors.Hand);
            }
          //  Assert.AreEqual(main.stacks.Count, 17);
        }
    } 
}
