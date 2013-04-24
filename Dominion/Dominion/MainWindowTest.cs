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
            MainWindow main = new MainWindow(new Game(1));
            Assert.AreEqual("",main.currentCard, "");
            Assert.AreEqual("Buy Phase",main.phase,"Buy Phase");
            Assert.AreEqual(4,main.victoryImage.Count());
            Assert.AreEqual(3,main.currencyImage.Count());
            Assert.AreEqual(10,main.actionImage.Count());
            Assert.AreEqual(50,main.handImage.Count());
            Assert.AreEqual(17,main.FieldImage.Count());
            Assert.AreEqual(4,main.victoryButton.Count());
            Assert.AreEqual(3,main.currencyButton.Count());
            Assert.AreEqual(10,main.actionButton.Count());
            Assert.AreEqual(50,main.handButton.Count());
            Assert.AreEqual(17,main.FieldButton.Count());
            for (int i=0;i<50;i++){
                Assert.AreEqual(Cursors.No,main.handButton[i].Cursor);
            }
            for (int i = 0; i < 17; i++) {
                Assert.AreEqual(Cursors.Hand,main.FieldButton[i].Cursor);
            }
        }
    } 
}
