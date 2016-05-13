using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportzMagazine.Models
{
    public class CreditCard
    {
        private string _cardType;
        private int _cardNumber;
        private string _cardHolderName;
        private DateTime _expirationDate;

        public CreditCard(string cardType,string cardholderName,int cardNumber,DateTime expirationDate)
        {
            _cardType = cardType;
            _cardHolderName = cardholderName;
            _cardNumber = cardNumber;
            _expirationDate = expirationDate;
        }
    }
}
