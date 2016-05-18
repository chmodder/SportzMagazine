using SportzMagazine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Catalogs
{
    [XmlInclude(typeof(SubscriptionCatalog))]
    public class SubscriptionCatalog
    {
        private List<Subscription> _subscriptionList;
        private List<Applicant> _applicantList;

        public List<Subscription> SubscriptionList
        {
            get
            {
                return _subscriptionList;
            }

            set
            {
                _subscriptionList = value;
            }
        }

        public List<Applicant> ApplicantList
        {
            get
            {
                return _applicantList;
            }

            set
            {
                _applicantList = value;
            }
        }

        public SubscriptionCatalog()
        {
            //This is PARAMETERLESS constructor is required by the XMLSerializeer
        }

        public Subscription CreateNewSubscription(
            string name,
            string address,
            string emailAddress,
            string phoneNumber,
            int numberOfCopies,
            DateTime startDate,
            DateTime expirationDate,
            string creditCardType,
            string creditCardHolderName,
            int creditCardNumber,
            DateTime creditCardExpirationDate)
        {
            //Creates new instances of the Applicant and Subscription lists
            SubscriptionList = new List<Subscription>();
            ApplicantList = new List<Applicant>();

            //Create new IndividualApplicant object and adds it to the Applicant list
            IndividualApplicant a1 = new IndividualApplicant(name, address, emailAddress, phoneNumber, creditCardType, creditCardHolderName, creditCardNumber, creditCardExpirationDate);
            ApplicantList.Add(a1);

            //Create new IndividualSubscription object and adds it to the Subscription list
            IndividualSubscription s1 = new IndividualSubscription(a1, numberOfCopies, startDate, expirationDate);
            SubscriptionList.Add(s1);

            //returns the Subscription object to the ViewModel's AddNewSubscription method from where it was invoked
            return s1;
        }
    }
}
