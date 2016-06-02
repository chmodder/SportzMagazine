﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Automation.Peers;
using SportzMagazine.Models;
using SportzMagazine.Views;
using SportzMagazine.Catalogs;
using SportzMagazine.Helpers;


namespace SportzMagazine.ViewModels
{

    public class AddSubscriptionCorporateViewModel : BaseViewModel
    {
        #region Instance Fields
        private SubscriptionCatalog _sc1;
        private Subscription _newSubscription;
        private CorporateSubscription _submittedSubscriptionInfo;
        private string _notificationMessage;
        private string _contactName;
        private string _jobTitle;
        private string _address;
        private string _emailAddress;
        private string _phoneNumber;
        private int _numberOfCopies;
        private DateTime _startDate;
        private int _duration;
        private DateTime _expirationDate;


        #endregion


        #region Constructors
        public AddSubscriptionCorporateViewModel()
        {
            //Create SubscriptionCatalog because it depends on it 
            Sc1 = new SubscriptionCatalog();

            //Load SubscriptionData to from file into catalog
            Sc1.LoadSubscriptionData();


            //Create RelayCommand for súbmitting the application
            SubmitApplication = new RelayCommand(AddNewSubscription);


            //Create new observable collection
            SubscriptionList = new ObservableCollection<Subscription>();
        }
        #endregion


        #region Methods
        /// <summary>
        /// Creates a new Subscription item and saves it to a file as part of a Subscription collection
        /// </summary>
        /// <param name="obj"></param>
        private void AddNewSubscription(object obj)
        {
            //clears previous errormessages
            NotificationMessage = "";

            //Clears SubmittedSubscriptionInfo
            SubmittedSubscriptionInfo = null;

            //Validates property data from the input
            #region Validation

            CustomValidation theValidation = new CustomValidation();
            bool inputIsValid = theValidation.IsValidCorporateSubscriptionInfo(ContactName, JobTitle, Address, EmailAddress, PhoneNumber, NumberOfCopies,
                StartDate, Duration);

            //Adds validationmessages to existing message
            NotificationMessage += theValidation.ErrorMessagesToString().ToString();

            #endregion

            //Calculates ExpirationDate based on Startdate and Duration
            ExpirationDate = StartDate.AddMonths(Duration);

            //Proceeds to deserialization of existing subscriptionlist if input is validated
            if (inputIsValid)
            {
                //Create new subscription
                NewSubscription = Sc1.CreateNewSubscription(
                    ContactName,
                    JobTitle,
                    Address,
                    EmailAddress,
                    PhoneNumber,
                    NumberOfCopies,
                    StartDate,
                    ExpirationDate);

                //If Subscription is not in the list ("Contains" not working. Probably because it checks for reference equality instead of value equality)
                bool alreadyExist = Sc1.IsInSubscriptionList(NewSubscription);

                if (!alreadyExist)
                {
                    //Adds To SubscriptionList
                    Sc1.AddSubscriptionToCatalog(NewSubscription);

                    //Serialization process / saves the list
                    Sc1.SaveSubscriptionData();

                    //SubmittedInfo is what will be showed to the applicant when the Applicantion has been saved 
                    SubmittedSubscriptionInfo = NewSubscription as CorporateSubscription;

                    //sets the notification
                    NotificationMessage = "Application is Saved";
                }
                else
                {
                    //Notify Subscription Applicant that the submitted information already exists in the system
                    NotificationMessage = "The information is already in the system";
                }

            }
            else
            {
                NotificationMessage = "System was unable to validate your input. Please check your input, and try again.\n" + NotificationMessage;
            }
        }
        #endregion


        #region Properties
        public RelayCommand SubmitApplication { get; set; }

        public ObservableCollection<Subscription> SubscriptionList
        {
            get { return Sc1.ListToObservableCollectionConverter(Sc1.SubscriptionList); }
            set
            {
                Sc1.SubscriptionList = Sc1.ObservableCollectionToListConverter(value);
                OnPropertyChanged("SubscriptionList");
            }
        }

        private SubscriptionCatalog Sc1
        {
            get
            {
                return _sc1;
            }


            set
            {
                _sc1 = value;
                OnPropertyChanged("Sc1");
            }
        }

        //Set the minimum year in the DatePicker xaml controls
        public DateTime MinDate { get { return DateTime.Now; } }


        //Set the maximum year in the DatePicker xaml controls
        public DateTime MaxDate { get { return DateTime.Now.AddYears(3); } }


        public string NotificationMessage { get { return _notificationMessage; } set { _notificationMessage = value; OnPropertyChanged("NotificationMessage"); } }


        public string ContactName
        {
            get
            {
                return _contactName;
            }


            set
            {
                _contactName = value;
                OnPropertyChanged("Address");
            }
        }


        public string Address
        {
            get
            {
                return _address;
            }


            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }


        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }


            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }


        public int NumberOfCopies
        {
            get
            {
                return _numberOfCopies;
            }


            set
            {
                _numberOfCopies = value;
                OnPropertyChanged("NumberOfCopies");
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
                OnPropertyChanged("StartDate");
            }
        }


        public int Duration
        {
            get
            {
                return _duration;
            }


            set
            {
                String strValue = value.ToString();
                int intValue = int.Parse(strValue.Substring(0, 2));
                _duration = intValue;
                OnPropertyChanged("Duration");


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
                OnPropertyChanged("ExpirationDate");
            }
        }

        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }


            set
            {
                _emailAddress = value;
                OnPropertyChanged("EmailAddres");
            }
        }

        public string JobTitle { get { return _jobTitle; } set { _jobTitle = value; OnPropertyChanged("JobTitle"); } }

        public CorporateSubscription SubmittedSubscriptionInfo
        {
            get { return _submittedSubscriptionInfo; }
            set { _submittedSubscriptionInfo = value; OnPropertyChanged("SubmittedSubscriptionInfo"); }
        }

        public Subscription NewSubscription { get { return _newSubscription; } set { _newSubscription = value; OnPropertyChanged("NewSubscription"); } }

        #endregion
    }
}
