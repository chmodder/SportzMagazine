using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportzMagazine.Models
{
    public class IndividualSubscription : Subscription
    {
        private IndividualApplicant _individualApplicant;

        public IndividualSubscription(IndividualApplicant theApplicant, int numberOfCopies, DateTime startDate, DateTime expirationDate) : base(numberOfCopies, startDate, expirationDate)
        {
            IndividualApplicant = theApplicant;
        }

        public IndividualApplicant IndividualApplicant
        {
            get
            {
                return _individualApplicant;
            }

            set
            {
                _individualApplicant = value;
            }
        }
    }
}
