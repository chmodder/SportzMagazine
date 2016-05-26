using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SportzMagazine.Helpers;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(CreditCard))]
    public class CreditCard
    {
        #region Instance Fields
        private string _cardType;
        private string _cardNumber;
        private string _cardHolderName;
        private DateTime _expirationDate;
        #endregion

        #region Constructors
        public CreditCard()
        {
            //This PARAMETERLESS constructor is required by the XMLSerializeer
        }


        public CreditCard(string cardType,string cardholderName,string cardNumber,DateTime expirationDate)
        {
            CardType = cardType;
            CardHolderName = cardholderName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sends the credit card information to Payment System and then sends an Approved or Declined message to Applicant depending on Payment Systems return value
        /// </summary>
        /// <param name="accountNumber">The subscription account number</param>
        /// <param name="emailAddress">Applicants email address</param>
        /// <param name="subscriptionStartDate">The subscription startdate</param>
        /// <returns>bool</returns>
        public bool SendCreditCardVerification(string accountNumber, string emailAddress, DateTime subscriptionStartDate)
        {   
            bool isVerified = false;

            isVerified = SimulatedPaymentSystem.VerifyCreditCard(this.CardHolderName, this.CardNumber, this.CardType, this.ExpirationDate);

            if (isVerified)
            {
                //Creates the message, which will be send via email
                string subscriptionApprovedMessage = "The subscription has been approved";

                //Creates new instance of Approval Message and sends the email to the applicant
                EmailApproval theEmail = new EmailApproval(subscriptionApprovedMessage, accountNumber, emailAddress);
                theEmail.SendMessageToApplicant(subscriptionStartDate);
            }
            else
            {
                //takes the 4 last charaters in the cardnumber and adds it to 12 stars...
                //...which represents the first 12 characters in credit card number for security reasons.
                string cardNumber = "**** **** **** " + this.CardNumber.Substring(11, 4);

                //Creates the complete message, which will be send via email
                string subscriptionDeclinedMessage = $"the cardnumber {cardNumber} has been declined.Please review your credit card details and apply for a new subscription.";

                //Creates new instance of Approval Message and sends the email to the applicant
                EmailApproval theEmail = new EmailApproval(subscriptionDeclinedMessage, accountNumber, emailAddress);
                theEmail.SendMessageToApplicant();
            }

            return isVerified;
        }
        #endregion

        #region Properties
        public string CardType { get { return _cardType; } set { _cardType = value; } }

        public string CardNumber { get { return _cardNumber; } set { _cardNumber = value; } }

        public string CardHolderName { get { return _cardHolderName; } set { _cardHolderName = value; } }

        public DateTime ExpirationDate { get { return _expirationDate; } set { _expirationDate = value; } }
        #endregion
    }
}
