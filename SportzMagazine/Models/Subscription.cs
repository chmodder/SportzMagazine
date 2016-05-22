using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(Subscription))]
    [XmlInclude(typeof(IndividualSubscription))]
    [XmlInclude(typeof(CorporateSubscription))]
    public class Subscription
    {
        private int _numberOfCopies;
        private DateTime _startDate;
        private DateTime _expirationDate;

        public Subscription()
        {
            //This empty PARAMETERLESS constructor is required by the XmlSerialiser
        }

        public Subscription(int numberOfCopies, DateTime startDate, DateTime expirationDate)
        {
            this.NumberOfCopies = numberOfCopies;
            this.StartDate = startDate;
            this.ExpirationDate = expirationDate;
        }


        public int NumberOfCopies { get { return _numberOfCopies; } set { _numberOfCopies = value; } }

        public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }

        public DateTime ExpirationDate { get { return _expirationDate; } set { _expirationDate = value; } }
    }
}
