using SportzMagazine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SportzMagazine.Helpers;

namespace SportzMagazine.Catalogs
{
    [XmlInclude(typeof(SubscriptionCatalog))]
    public class SubscriptionCatalog
    {
        #region Instance Fields
        private List<Subscription> _subscriptionList;
        private List<Applicant> _applicantList;
        private string _subscriptionListFileName = "Subscriptions.xml";
        #endregion

        #region Properties
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

        public string SubscriptionListFileName
        {
            get { return _subscriptionListFileName; }
            set { _subscriptionListFileName = value; }
        }

        #endregion

        #region Constructors
        public SubscriptionCatalog()
        {
            //This is PARAMETERLESS constructor is required by the XMLSerializeer

            //Creates new instances of the Applicant and Subscription lists
            SubscriptionList = new List<Subscription>();
            ApplicantList = new List<Applicant>();

            ////Loads the List/deserialization
            //LoadSubscriptionData();
        }
        #endregion

        #region methods
        /// <summary>
        /// Creates IndividualSubscription
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="emailAddress"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="numberOfCopies"></param>
        /// <param name="startDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="creditCardType"></param>
        /// <param name="creditCardHolderName"></param>
        /// <param name="creditCardNumber"></param>
        /// <param name="creditCardExpirationDate"></param>
        /// <returns></returns>
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
            string creditCardNumber,
            DateTime creditCardExpirationDate)
        {
            
            //Create new IndividualApplicant object and adds it to the Applicant list
            IndividualApplicant a1 = new IndividualApplicant(name, address, emailAddress, phoneNumber, creditCardType, creditCardHolderName, creditCardNumber, creditCardExpirationDate);
            ApplicantList.Add(a1);

            //Create new IndividualSubscription object and adds it to the Subscription list
            IndividualSubscription s1 = new IndividualSubscription(a1, numberOfCopies, startDate, expirationDate);

            //returns the Subscription object to the ViewModel's AddNewSubscription method from where it was invoked
            return s1;
        }


        /// <summary>
        /// Creates CorporateSubscription
        /// </summary>
        /// <param name="contactName"></param>
        /// <param name="jobTitle"></param>
        /// <param name="address"></param>
        /// <param name="emailAddress"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="numberOfCopies"></param>
        /// <param name="startDate"></param>
        /// <param name="expirationDate"></param>
        /// <returns></returns>
        public Subscription CreateNewSubscription(
            string contactName,
            string jobTitle,
            string address,
            string emailAddress,
            string phoneNumber,
            int numberOfCopies,
            DateTime startDate,
            DateTime expirationDate)
        {
            

            //Create new IndividualApplicant object and adds it to the Applicant list
            CorporateApplicant a1 = new CorporateApplicant(contactName, jobTitle, address, emailAddress, phoneNumber);
            ApplicantList.Add(a1);

            //Create new IndividualSubscription object
            CorporateSubscription s1 = new CorporateSubscription(a1, numberOfCopies, startDate, expirationDate);

            //returns the Subscription object to the ViewModel's AddNewSubscription method from where it was invoked
            return s1;
        }

        public void AddSubscriptionToCatalog(Subscription subscription)
        {
            SubscriptionList.Add(subscription);
        }

        public async void LoadSubscriptionData()
        {

            //Creates in instance of the serialization class and sets the filename
            Serialization readFile = new Serialization(SubscriptionListFileName);


            //Sets read from file task up, which should return an ObservableCollection value
            Task<ObservableCollection<Subscription>> myTask = readFile.LoadSubscriptionsFromXmlAsync();


            //this might need some refatoring and cleanup - this line is needed to run the task and receive a result
            var result = await myTask;


            //Load and set the SubscriptionList value from file if it exists - Should only add Task.Result() to SubscriptionList if file exists
            if (myTask.IsCompleted)
            {
                //Adds file data (which are previous Subscriptions) to the ObservableCollection
                SubscriptionList = ObservableCollectionToListConverter(myTask.Result);
            }
        }

        public async void LoadSubscriptionDataAlternate()
        {

            //Creates in instance of the serialization class and sets the filename
            Serialization readFile = new Serialization(SubscriptionListFileName);


            //Sets read from file task up, which should return an ObservableCollection value
            Task<ObservableCollection<Subscription>> myTask = readFile.LoadSubscriptionsFromXmlAsync();


            //this might need some refatoring and cleanup - this line is needed to run the task and receive a result
            var result = await myTask;


            //Load and set the SubscriptionList value from file if it exists - Should only add Task.Result() to SubscriptionList if file exists
            if (myTask.IsCompleted)
            {
                //Adds file data (which are previous Subscriptions) to the ObservableCollection
                SubscriptionList = ObservableCollectionToListConverter(myTask.Result);
            }
        }



        public void SaveSubscriptionData()
        {
            //Creates Serialization object
            Serialization writeFile = new Serialization(SubscriptionListFileName);
            //Serialization object save the updated Subscription List to a file
            writeFile.SaveSubscriptionsAsXmkAsync(ListToObservableCollectionConverter(SubscriptionList));
        }



        #region converters

        //might move these converter methods to a helper class later when cleaning and refactoring
        public List<Subscription> ObservableCollectionToListConverter(ObservableCollection<Subscription> theSubscriptionObservableCollection)
        {
            List<Subscription> theSubscriptionList = new List<Subscription>();

            if (theSubscriptionObservableCollection.Count > 0)
            {
                foreach (Subscription subscription in theSubscriptionObservableCollection)
                {
                    theSubscriptionList.Add(subscription);
                }
            }
            return theSubscriptionList;
        }


        public ObservableCollection<Subscription> ListToObservableCollectionConverter(List<Subscription> theSubscriptionList )
        {
            ObservableCollection<Subscription> theSubscriptionObservableCollection = new ObservableCollection<Subscription>();

            if (theSubscriptionList.Count > 0)
            {
                foreach (Subscription subscription in theSubscriptionList)
                {
                    theSubscriptionObservableCollection.Add(subscription);
                }
            }

            return theSubscriptionObservableCollection;

        }
        #endregion

        /// <summary>
        /// Checks if a Subscription object is inside a List of Subscriptions
        /// </summary>
        /// <param name="newSubscription"></param>
        /// <param name="subscriptionList"></param>
        /// <returns></returns>
        public bool IsInSubscriptionList(Subscription newSubscription)
        {
            //sets the default return value
            bool alreadyExist = false;

            //checks if input arguments are null and returns false if thats the case, because comparision of null values are not
            if (newSubscription == null || SubscriptionList == null)
            {
                return alreadyExist;
            }

            //if newSubscription is an Individual Subscription check if newSubscription proprties match an item in the subscriptionList
            if (newSubscription.GetType() == typeof(IndividualSubscription))
            {
                IndividualSubscription theSubscription = (IndividualSubscription)newSubscription;

                for (int i = 0; i < SubscriptionList.Count(); i++)
                {
                    if (SubscriptionList[i].GetType() == typeof(IndividualSubscription))
                    {
                        //casts the subscriptionList item to IndividualSubscription
                        IndividualSubscription item = (IndividualSubscription)SubscriptionList[i];

                        //if object-properties values are the same, we assume the objects are the same (more properties should probably be checked), and return true because the object is in the list
                        if (item.TheIndividualApplicant.Name == theSubscription.TheIndividualApplicant.Name)
                        {
                            alreadyExist = true;
                        }
                    }
                }
            }

            #region Corporate subscription
            //if newSubscription is a corporate subscription check if newSubscription proprties match an item in the subscriptionList
            else if (newSubscription.GetType() == typeof(CorporateSubscription))
            {
                CorporateSubscription theSubscription = (CorporateSubscription)newSubscription;

                for (int i = 0; i < SubscriptionList.Count(); i++)
                {
                    if (SubscriptionList[i].GetType() == typeof(CorporateSubscription))
                    {
                        CorporateSubscription item = (CorporateSubscription)SubscriptionList[i];

                        if (item.TheCorporateApplicant.ContactName == theSubscription.TheCorporateApplicant.ContactName)
                        {
                            alreadyExist = true;
                        }
                    }
                }
            }
            #endregion

            return alreadyExist;
        }

        #endregion
    }


}
