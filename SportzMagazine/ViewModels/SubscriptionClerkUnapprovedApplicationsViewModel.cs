﻿using System;
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
        private SubscriptionCatalog _theSubscriptionCatalog;
        private RelayCommand _verifyCreditCardCommand;
        private const string FileName = "Subscriptions.xml";

        #endregion

        public SubscriptionClerkUnapprovedApplicationsViewModel()
        {
            #region Dummy data
            ////test (create some dummy data)
            //IndividualApplicant individualApplicant1 = new IndividualApplicant("harry", "somewhere street", "harry@mail.com", "12345678", "visa", "Harry CC name", "1234567812345678", DateTime.Today.AddMonths(2));
            //IndividualApplicant individualApplicant2 = new IndividualApplicant("Hermoine", "oxford street", "hermoine@mail.com", "12345678", "mastercard", "Hermoine CC name", "1234567812345678", DateTime.Today.AddMonths(6));

            //IndividualSubscription individualSubscription1 = new IndividualSubscription(individualApplicant1, 3, DateTime.Today.AddDays(8), DateTime.Today.AddMonths(6));
            //IndividualSubscription individualSubscription2 = new IndividualSubscription(individualApplicant2, 9, DateTime.Today.AddDays(23), DateTime.Today.AddMonths(12));

            //CorporateApplicant corporateApplicant1 = new CorporateApplicant("mr Anderson", "Piccadilly circus", "bluepill@mail.com", "12345678", "hacker");
            //CorporateApplicant corporateApplicant2 = new CorporateApplicant("Morpheus", "Baker Street", "redpill@mail.com", "12345678", "mentor");

            //CorporateSubscription corporateSubscription1 = new CorporateSubscription(corporateApplicant1, 4, DateTime.Today.AddDays(5), DateTime.Today.AddMonths(7));
            //CorporateSubscription corporateSubscription2 = new CorporateSubscription(corporateApplicant2, 9, DateTime.Today.AddMonths(1), DateTime.Today.AddMonths(13));

            ////add dummy data to the new list
            //SubscriptionList = new ObservableCollection<Subscription>() { individualSubscription1, individualSubscription2 };
            #endregion

            //Create new subscription catalog
            TheSubscriptionCatalog = new SubscriptionCatalog();

            SubscriptionList = new ObservableCollection<Subscription>();
            
            //create new Relay command for Verify CreditCard button
            VerifyCreditCardCommand = new RelayCommand(VerifyCreditCard);

            //Load SubscriptionData from file
            TheSubscriptionCatalog.LoadSubscriptionData();

            //Filter Individual subscriptions
            IndividualSubscriptionList = FilterIndividualSubscriptions();

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


        #region methods
        private void VerifyCreditCard(object obj)
        {
            UserNotification = "";
            //test if obj is null
            if (obj != null)
            {
                //Verify the card
                string test = obj.GetType().ToString();
                UserNotification = "<Should say verified depending on return value>";
            }
            else
            {
                UserNotification = "Make a selection before clicking the button";
            }
        }
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
            set { _theSubscriptionCatalog = value; }
        }

        public ObservableCollection<IndividualSubscription> IndividualSubscriptionList
        {
            get { return _individualSubscriptionList; }
            set { _individualSubscriptionList = value; }
        }

        #endregion
    }
}
