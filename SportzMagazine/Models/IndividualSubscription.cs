using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(IndividualSubscription))]
    public class IndividualSubscription : Subscription
    {
        private IndividualApplicant _theIndividualApplicant;

        public IndividualSubscription()
        {
            //This PARAMETERLESS constructor is required by the XMLSerializeer
        }

        public IndividualSubscription(IndividualApplicant theApplicant, int numberOfCopies, DateTime startDate, DateTime expirationDate) : base(numberOfCopies, startDate, expirationDate)
        {
            TheIndividualApplicant = theApplicant;
        }


        public IndividualApplicant TheIndividualApplicant
        {
            get { return _theIndividualApplicant; }
            set { _theIndividualApplicant = value; }
        }
    }
}
