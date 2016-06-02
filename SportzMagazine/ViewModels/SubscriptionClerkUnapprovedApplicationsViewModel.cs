using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SportzMagazine.Catalogs;
using SportzMagazine.Helpers;
using SportzMagazine.Models;

namespace SportzMagazine.ViewModels
{
    public class SubscriptionClerkUnapprovedApplicationsViewModel : BaseViewModel
    {
        #region instance fields

        private string _userNotification;
        private ObservableCollection<Subscription> _subscriptionList;
        private ObservableCollection<IndividualSubscription> _individualSubscriptionList;
        private CreditCard _theCreditCard;
        private IndividualSubscription _selectedIndividualSubscription;
        private SubscriptionCatalog _theSubscriptionCatalog;
        private RelayCommand _verifyCreditCardCommand;
        private const string FileName = "Subscriptions.xml";

        #endregion

        public SubscriptionClerkUnapprovedApplicationsViewModel()
        {
            #region Dummy data until LoadSubscriptionData() has been fixed
            ////test (create some dummy data)
            IndividualApplicant individualApplicant1 = new IndividualApplicant("harry", "somewhere street", "harry@mail.com", "12345678", "visa", "Harry CC name", "1234567812345678", DateTime.Today.AddMonths(2));
            IndividualApplicant individualApplicant2 = new IndividualApplicant("Hermoine", "oxford street", "hermoine@mail.com", "12345678", "mastercard", "Hermoine CC name", "1234567812345678", DateTime.Today.AddMonths(6));

            IndividualSubscription individualSubscription1 = new IndividualSubscription(individualApplicant1, 3, DateTime.Today.AddDays(8), DateTime.Today.AddMonths(6));
            IndividualSubscription individualSubscription2 = new IndividualSubscription(individualApplicant2, 9, DateTime.Today.AddDays(23), DateTime.Today.AddMonths(12));

            CorporateApplicant corporateApplicant1 = new CorporateApplicant("mr Anderson", "Piccadilly circus", "bluepill@mail.com", "12345678", "hacker");
            CorporateApplicant corporateApplicant2 = new CorporateApplicant("Morpheus", "Baker Street", "redpill@mail.com", "12345678", "mentor");

            CorporateSubscription corporateSubscription1 = new CorporateSubscription(corporateApplicant1, 4, DateTime.Today.AddDays(5), DateTime.Today.AddMonths(7));
            CorporateSubscription corporateSubscription2 = new CorporateSubscription(corporateApplicant2, 9, DateTime.Today.AddMonths(1), DateTime.Today.AddMonths(13));

             #endregion

            //Create new subscription catalog
            TheSubscriptionCatalog = new SubscriptionCatalog();

            SubscriptionList = new ObservableCollection<Subscription>();

            //create new Relay command for Verify CreditCard button
            VerifyCreditCardCommand = new RelayCommand(VerifyCreditCard);

            #region This needs fixing - will use dummy data to present the Subscription Clerk use case (Approve subscription)
            //Not working unless in debug mode. It seems that LoadSubscriptionData() is not finished before FilterUnapprovedIndividualSubscriptions() runs
            //The problem is the FilterUnapprovedIndividualSubscriptions() is depending on the data loaded in LoadSubscriptionData()
            //The result is that it tries to filter an empty list (SubscriptionCatalog.SubscriptionList<Subscription>) because the data has not finished loading. 

            ////Load SubscriptionData from file
            //TheSubscriptionCatalog.LoadSubscriptionData();

            //add dummy data to the new list until LoadSubscriptionData() has been fixed
            SubscriptionList = new ObservableCollection<Subscription>() { individualSubscription1, individualSubscription2, corporateSubscription1, corporateSubscription2 };
            #endregion

            //Filter Individual subscriptions which are Unapproved
            IndividualSubscriptionList = FilterUnapprovedIndividualSubscriptions();

        }


        #region methods
        private void VerifyCreditCard(object obj)
        {
            UserNotification = "";
            //test if obj is null
            if (obj != null)
            {
                //Verify the card


                TheCreditCard = obj as CreditCard;

                bool IsApproved = false;

                //Checks the Individual Supscription list for subscription/applicant containing the selected CreditCard
                foreach (IndividualSubscription subscription in IndividualSubscriptionList)
                {
                    if (subscription.TheIndividualApplicant.CreditCardList.Contains(TheCreditCard))
                    {
                        SelectedIndividualSubscription = subscription;
                        IsApproved = TheCreditCard.SendCreditCardVerification(SelectedIndividualSubscription.AccountNumber, SelectedIndividualSubscription.TheIndividualApplicant.EmailAddress, SelectedIndividualSubscription.StartDate);
                        break;
                    }
                }

                //Updates the Subscription approval status on the Subscription to true if creditcard was approved
                if (IsApproved)
                {
                    foreach (Subscription subscription in SubscriptionList)
                    {
                        if (subscription.AccountNumber == SelectedIndividualSubscription.AccountNumber)
                        {
                            subscription.IsApproved = true;
                            TheSubscriptionCatalog.SaveSubscriptionData();
                            break;
                        }
                    }
                }


                //Filter Individual subscriptions which are Unapproved
                IndividualSubscriptionList = FilterUnapprovedIndividualSubscriptions();

                UserNotification = IsApproved ? "Card has been approved and Subscription status is updated to Approved" : "Card was not approved";
            }
            else
            {
                UserNotification = "Make a selection before clicking the button";
            }
        }

        #region Should maybe put these in a helper class or in the Catalog
        private ObservableCollection<IndividualSubscription> FilterUnapprovedIndividualSubscriptions()
        {
            ObservableCollection<IndividualSubscription> theIndividualSubscriptions = new ObservableCollection<IndividualSubscription>();

            foreach (Subscription subscription in SubscriptionList)
            {
                if (subscription.GetType() == typeof(IndividualSubscription) && !subscription.IsApproved)
                {

                    theIndividualSubscriptions.Add(subscription as IndividualSubscription);
                }
            }
            return theIndividualSubscriptions;
        }

        private ObservableCollection<IndividualSubscription> FilterIndividualSubscriptions()
        {
            ObservableCollection<IndividualSubscription> theIndividualSubscriptions = new ObservableCollection<IndividualSubscription>();

            foreach (Subscription subscription in SubscriptionList)
            {
                if (subscription.GetType() == typeof(IndividualSubscription))
                {
                    theIndividualSubscriptions.Add((IndividualSubscription)subscription);
                }
            }
            return theIndividualSubscriptions;
        }
        #endregion

        #endregion

        #region Properties
        public ObservableCollection<Subscription> SubscriptionList
        {
            get { return TheSubscriptionCatalog.ListToObservableCollectionConverter(TheSubscriptionCatalog.SubscriptionList); }
            set
            {
                if (value != null)
                {
                    TheSubscriptionCatalog.SubscriptionList = TheSubscriptionCatalog.ObservableCollectionToListConverter(value);
                    OnPropertyChanged("SubscriptionList");
                }
            }
        }

        public RelayCommand VerifyCreditCardCommand
        {
            get { return _verifyCreditCardCommand; }
            set { _verifyCreditCardCommand = value; OnPropertyChanged("VerifyCreditCardCommand"); }
        }

        public string UserNotification { get { return _userNotification; } set { _userNotification = value; OnPropertyChanged("UserNotification"); } }

        public SubscriptionCatalog TheSubscriptionCatalog
        {
            get { return _theSubscriptionCatalog; }
            set { _theSubscriptionCatalog = value; OnPropertyChanged("TheSubscriptionCatalog"); }
        }

        public ObservableCollection<IndividualSubscription> IndividualSubscriptionList
        {
            get { return _individualSubscriptionList; }
            set { _individualSubscriptionList = value; OnPropertyChanged("IndividualSubscriptionList"); }
        }

        public CreditCard TheCreditCard { get { return _theCreditCard; } set { _theCreditCard = value; OnPropertyChanged("TheCreditCard"); } }

        public IndividualSubscription SelectedIndividualSubscription
        {
            get { return _selectedIndividualSubscription; }
            set { _selectedIndividualSubscription = value; OnPropertyChanged("SelectedIndividualSubscription"); }
        }

        #endregion
    }
}
