using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SportzMagazine.Models
{
    [DataContract]
    public class IndividualApplicant : Applicant
    {
        [DataMember]
        private string _name;
        [DataMember]
        private List<CreditCard> _creditCardList;

        public List<CreditCard> CreditCardList
        {
            get
            {
                return _creditCardList;
            }

            set
            {
                _creditCardList = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public IndividualApplicant(
            string name, 
            string address, 
            string emailAddress, 
            string phoneNumber, 
            string creditCardType,
            string creditCardHolderName, 
            int creditCardNumber, 
            DateTime creditCardExpirationDate) : base(address,emailAddress,phoneNumber)
        {
            this.Name = name;
            CreditCardList = new List<CreditCard>();

            CreditCard cc1 = new CreditCard(
                creditCardType, 
                creditCardHolderName, 
                creditCardNumber, 
                creditCardExpirationDate);

            CreditCardList.Add(cc1);
        }
    }
}
