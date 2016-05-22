using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(CorporateSubscription))]
    public class CorporateSubscription : Subscription
    {
        private CorporateApplicant _theCorporateApplicant;

        public CorporateSubscription()
        {
            //This PARAMETERLESS constructor is required by the XMLSerializeer
        }

        public CorporateSubscription(CorporateApplicant theApplicant, int numberOfCopies, DateTime startDate, DateTime expirationDate) : base(numberOfCopies, startDate, expirationDate)
        {
            TheCorporateApplicant = theApplicant;
        }


        public CorporateApplicant TheCorporateApplicant
        {
            get { return _theCorporateApplicant; }
            set { _theCorporateApplicant = value; }
        }
    }
}
