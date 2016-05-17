using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportzMagazine.Models
{
    public class Subscription
    {
        private int _numberOfCopies;
        private DateTime _startDate;
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
