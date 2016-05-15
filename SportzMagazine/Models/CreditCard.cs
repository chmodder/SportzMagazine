using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportzMagazine.Models
{
    [DataContract]
    public class CreditCard
    {
        [DataMember]
        private string _cardType;
        [DataMember]
        private int _cardNumber;
        [DataMember]
        private string _cardHolderName;
        [DataMember]
        private DateTime _expirationDate;

        public string CardType
        {
            get
            {
                return _cardType;
            }

            set
            {
                _cardType = value;
            }
        }

        public int CardNumber
        {
            get
            {
                return _cardNumber;
            }

            set
            {
                _cardNumber = value;
            }
        }

        public string CardHolderName
        {
            get
            {
                return _cardHolderName;
            }

            set
            {
                _cardHolderName = value;
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return _expirationDate;
            }

            set
            {
                _expirationDate = value;
            }
        }

        public CreditCard(string cardType,string cardholderName,int cardNumber,DateTime expirationDate)
        {
            CardType = cardType;
            CardHolderName = cardholderName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
        }
    }
}
