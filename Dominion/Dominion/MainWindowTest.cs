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
            
           // Assert.Equals(main.currentCard, "");
           /* Assert.Equals(main.phase,"Action Phase");
            Assert.Equals(main.victoryImage.Count(),4);
            Assert.Equals(main.currencyImage.Count(), 3);
            Assert.Equals(main.actionImage.Count(), 10);
            Assert.Equals(main.handImage.Count(), 50);
            Assert.Equals(main.FieldImage.Count(), 17);
            Assert.Equals(main.victoryButton.Count(), 4);
            Assert.Equals(main.currencyButton.Count(), 3);
            Assert.Equals(main.actionButton.Count(), 10);
            Assert.Equals(main.handButton.Count(), 50);
            Assert.Equals(main.FieldButton.Count(), 17);
            for (int i=0;i<50;i++){
                Assert.Equals(main.handImage[i].Cursor,Cursors.No);
            }
            for (int i = 0; i < 17; i++) {
                Assert.Equals(main.FieldImage[i].Cursor, Cursors.Hand);
            }
            Assert.Equals(main.stacks.Count, 17);*/
        }
    } 
}
