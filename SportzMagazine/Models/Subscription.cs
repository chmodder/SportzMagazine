using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportzMagazine.Models
{
    [DataContract]
    public class Subscription
    {
        [DataMember]
        private int _numberOfCopies;
        [DataMember]
        private DateTime _startDate;
        [DataMember]
        private DateTime _expirationDate;

        public int NumberOfCopies
        {
            get
            {
                return _numberOfCopies;
            }

            set
            {
                _numberOfCopies = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
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

        public Subscription(int numberOfCopies, DateTime startDate,DateTime expirationDate)
        {
            this.NumberOfCopies = numberOfCopies;
            this.StartDate = startDate;
            this.ExpirationDate = expirationDate;
        }
    }
}
