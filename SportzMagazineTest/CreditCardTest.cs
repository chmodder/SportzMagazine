using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SportzMagazine.Models;

namespace SportzMagazineTest
{
    

    [TestClass]
    public class CreditCardTest
    {
        private CreditCard _creditCard;

        [TestInitialize]
        public void BeforeTest()
        {
            _creditCard = new CreditCard("MasterCard","John Doe","1234567887654321",new DateTime(2016,03,21));
        }

        [TestMethod]
        public void IsCardTypePropertyValueMasterCardShouldBeTrue()
        {
            try
            {
                Assert.ThrowsException<ArgumentException>(() => _creditCard.CardType == "MasterCard");
            }
            catch (Exception ex)
            {
                
                Assert.AreEqual("MasterCard",_creditCard.CardType);
            }
        }

        [TestMethod]
        public void IsCardNumberPropertyValue1234567887654321ShouldBeTrue()
        {
            try
            {
                Assert.ThrowsException<ArgumentException>(() => _creditCard.CardNumber == "1234567887654321");
            }
            catch (Exception ex)
            {

                Assert.AreEqual("1234567887654321", _creditCard.CardNumber);
            }
        }

        [TestMethod]
        public void IsCardHolderNamePropertyValueJohnDoeShouldBeTrue()
        {

            try
            {
                Assert.ThrowsException<ArgumentException>(() => _creditCard.CardHolderName == "John Doe");
            }
            catch (Exception ex)
            {

                Assert.AreEqual("John Doe", _creditCard.CardHolderName);
            }
        }

        [TestMethod]
        public void SetCardNumberPropertyValue1111222233334444ShouldBeTrue()
        {
            //Arrange
            string aNewNumber = "1111222233334444";

            //Act
            string whatItShouldBe = aNewNumber;
            _creditCard.CardNumber = aNewNumber;


            //Assert
            Assert.AreEqual(whatItShouldBe, _creditCard.CardNumber);
        }

        [TestMethod]
        public void SetExpirationDatePropertyValue20161028ShouldBeTrue()
        {
            //Arrange
            DateTime aNewDateTime = new DateTime(2016,10,28);

            //Act
            DateTime whatItShouldBe = aNewDateTime;
            _creditCard.ExpirationDate = aNewDateTime;

            //Assert
            Assert.AreEqual(whatItShouldBe, _creditCard.ExpirationDate);
        }

        [TestMethod]
        public void IsNewEmptyCreditCardInstanceCreated()
        {
            //arrange
            CreditCard aNewInstanceCreditCard = new CreditCard();

            //Assert
            Assert.IsInstanceOfType(aNewInstanceCreditCard,typeof(CreditCard));
        }

    }
}
