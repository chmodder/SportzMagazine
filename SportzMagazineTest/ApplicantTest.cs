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
    public class ApplicantTest
    {
        private Applicant _applicant;

        [TestInitialize]
        public void BeforeTest()
        {
            _applicant = new Applicant("Falkoner Alle 68, 2000 Frederiksberg", "joeblack@earth.com", "12345678");
        }

        [TestMethod]
        public void IsApplicantStringPropertyPhoneNumber12345678ShouldReturnTrue()
        {
            try
            {
                Assert.ThrowsException<ArgumentException>(() => _applicant.PhoneNumber == "12345678");
            }
            catch (Exception)
            {
                Assert.AreEqual("12345678", _applicant.PhoneNumber);
            }
        }

        [TestMethod]
        public void IsApplicantStringPropertyAddressTheSameAsInConstructorShouldReturnTrue()
        {
            try
            {
                Assert.ThrowsException<ArgumentException>(() => _applicant.Address == "Falkoner Alle 68, 2000 Frederiksberg");
            }
            catch (Exception)
            {
                Assert.AreEqual("Falkoner Alle 68, 2000 Frederiksberg", _applicant.Address);
            }
        }

        [TestMethod]
        public void IsApplicantStringPropertyEmailAddressTheSameAsInConstructorShouldReturnTrue()
        {
            try
            {
                Assert.ThrowsException<ArgumentException>(() => _applicant.EmailAddress == "joeblack@earth.com");
            }
            catch (Exception)
            {
                Assert.AreEqual("joeblack@earth.com", _applicant.EmailAddress);
            }
        }

        [TestMethod]
        public void SetApplicantPhoneNumberPropertyValue11335577ShouldBeTrue()
        {
            //Arrange
            string aNewPhoneNumber = "11335577";

            //Act
            string whatItShouldBe = aNewPhoneNumber;
            _applicant.PhoneNumber = aNewPhoneNumber;


            //Assert
            Assert.AreEqual(whatItShouldBe, _applicant.PhoneNumber);
        }

        [TestMethod]
        public void SetApplicantEmailPropertyValueShouldMatchNewValue()
        {
            //Arrange
            string aNewEmailAddress = "someone@somewhere.com";

            //Act
            string whatItShouldBe = aNewEmailAddress;
            _applicant.EmailAddress = aNewEmailAddress;


            //Assert
            Assert.AreEqual(whatItShouldBe, _applicant.EmailAddress);
        }
        
        [TestMethod]
        public void SetApplicantAddressPropertyValueShouldMatchNewValue()
        {
            //Arrange
            string aNewAddress = "Bakkesvinget 66, 4000 Roskilde";

            //Act
            string whatItShouldBe = aNewAddress;
            _applicant.Address = aNewAddress;


            //Assert
            Assert.AreEqual(whatItShouldBe, _applicant.Address);
        }
    }
}
