using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(CreditCard))]
    public class CreditCard
    {
        private string _cardType;
        private int _cardNumber;
        private string _cardHolderName;
        private DateTime _expirationDate;

        public CreditCard()
        {
            //This PARAMETERLESS constructor is required by the XMLSerializeer
        }


        public CreditCard(string cardType,string cardholderName,int cardNumber,DateTime expirationDate)
        {
            CardType = cardType;
            CardHolderName = cardholderName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
        }

        public string CardType { get { return _cardType; } set { _cardType = value; } }

        public int CardNumber { get { return _cardNumber; } set { _cardNumber = value; } }

        public string CardHolderName { get { return _cardHolderName; } set { _cardHolderName = value; } }

        public DateTime ExpirationDate { get { return _expirationDate; } set { _expirationDate = value; } }
    }
}
