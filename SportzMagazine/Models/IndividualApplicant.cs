using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(IndividualApplicant))]
    public class IndividualApplicant : Applicant
    {
        private string _name;
        private List<CreditCard> _creditCardList;

        public IndividualApplicant()
        {
            //This is PARAMETERLESS constructor is required by the XMLSerializeer
        }


        public IndividualApplicant(
            string name, 
            string address, 
            string emailAddress, 
            string phoneNumber, 
            string creditCardType,
            string creditCardHolderName, 
            string creditCardNumber, 
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

        public string Name { get { return _name; } set { _name = value; } }

        public List<CreditCard> CreditCardList { get { return _creditCardList; } set { _creditCardList = value; } }
    }
}
