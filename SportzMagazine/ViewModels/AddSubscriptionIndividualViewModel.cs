﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportzMagazine.Models;
using SportzMagazine.Views;
using SportzMagazine.Catalogs;
using SportzMagazine.Helpers;

namespace SportzMagazine.ViewModels
{
    public class AddSubscriptionIndividualViewModel : BaseViewModel
    {
        #region Instance Fields
        private SubscriptionCatalog _sc1;
        private ObservableCollection<Subscription> _subscriptionList;
        private string _errorMessage;
        private string _name;
        private string _address;
        private string _emailAddress;
        private string _phoneNumber;
        private int _numberOfCopies;
        private DateTime _startDate;
        private int _duration;
        private DateTime _expirationDate;
        private string _creditCardType;
        private string _creditCardHolderName;
        private int _creditCardNumber;
        private DateTime _creditCardExpirationDate;
        private const string FileName = "Subscriptions.xml";

        #endregion

        #region Constructors
        public AddSubscriptionIndividualViewModel()
        {
            //Create SubscriptionCatalog because it depends on it 
            Sc1 = new SubscriptionCatalog();

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
        private async void AddNewSubscription(object obj)
        {
            ExpirationDate = StartDate.AddMonths(Duration);

            string name = Name;
            string address = Address;
            string emailAddress = EmailAddress;
            string phoneNumber = PhoneNumber;
            int numberOfCopies = NumberOfCopies;
            DateTime startDate = StartDate;
            DateTime expirationDate = ExpirationDate;
            string creditCardType = CreditCardType;
            string creditCardHolderName = CreditCardHolderName;
            int creditCardNumber = CreditCardNumber;
            DateTime creditCardExpirationDate = CreditCardExpirationDate;

            #region  Deserialization process

            //Creates in instance of the serialization class and sets the filename
            Serialization readFile = new Serialization(FileName);

            //Sets read from file task up, which should return an ObservableCollection value
            Task<ObservableCollection<Subscription>> myTask = readFile.LoadSubscriptionsFromXmlAsync();

            //this might need some refatoring and cleanup - this line is needed to run the task and receive a result
            var result = await myTask;

            //Load and set the SubscriptionList value from file if it exists - Should only add Task.Result() to SubscriptionList if file exists
            if (myTask.IsCompleted)
            {
                //Adds file data (which are previous Subscriptions) to the ObservableCollection
                SubscriptionList = myTask.Result;
            }
            #endregion

            //Validate needs implementation (should validate input from textboxes)
            if (true)
            {
                //Create new subscription
                Subscription s1 = Sc1.CreateNewSubscription(
                    name,
                    address,
                    emailAddress,
                    phoneNumber,
                    numberOfCopies,
                    startDate,
                    expirationDate,
                    creditCardType,
                    creditCardHolderName,
                    creditCardNumber,
                    creditCardExpirationDate);

                //If Subscription is not in the list
                if (!SubscriptionList.Contains(s1))
                {
                    //Adds the Subscription object to the observablecollection before sreializing/saving to file
                    SubscriptionList.Add(s1);

                    #region Serialization process
                    //Creates Serialization object
                    Serialization writeFile = new Serialization(FileName);
                    //Serialization object save the updated Subscription List to a file
                    writeFile.SaveSubscriptionsAsXmkAsync(SubscriptionList);
                    #endregion

                    //This part is cleaning the list after the data has been saved...
                    SubscriptionList.Clear();
                    //... and then adds the latest item (The newly created Subscription) to the ObservableCollection list again
                    SubscriptionList.Add(s1);
                    ErrorMessage = "Application is Saved";
                }
                else
                {
                    //Notify Subscription Applicant that the submitted information already exists in the system
                    ErrorMessage = "The information is already in the system";
                }


            }
            else
            {
                ErrorMessage = "System was unable to process your input. Please check your input, and try again.";
            }
        }
        #endregion

        #region Properties
        public RelayCommand SubmitApplication { get; set; }

        //Set the minimum year in the DatePicker xaml controls
        public DateTime MinDate { get { return DateTime.Now; } }

        //Set the maximum year in the DatePicker xaml controls
        public DateTime MaxDate { get { return DateTime.Now.AddYears(3); } }

        public ObservableCollection<Subscription> SubscriptionList
        {
            get { return _subscriptionList; }
            set
            {
                _subscriptionList = value;
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

        public string ErrorMessage { get { return _errorMessage; } set { _errorMessage = value; OnPropertyChanged("ErrorMessage"); } }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
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

        public string CreditCardType
        {
            get
            {
                return _creditCardType;
            }

            set
            {
                _creditCardType = value;
                OnPropertyChanged("CreditCardType");
            }
        }

        public string CreditCardHolderName
        {
            get
            {
                return _creditCardHolderName;
            }

            set
            {
                _creditCardHolderName = value;
                OnPropertyChanged("CreditCardHolderName");
            }
        }

        public int CreditCardNumber
        {
            get
            {
                return _creditCardNumber;
            }

            set
            {
                _creditCardNumber = value;
                OnPropertyChanged("CreditCardNumber");
            }
        }

        public DateTime CreditCardExpirationDate
        {
            get
            {
                return _creditCardExpirationDate;
            }

            set
            {
                _creditCardExpirationDate = value;
                OnPropertyChanged("CreditCardExpirationDate");
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

        #endregion
    }
}
