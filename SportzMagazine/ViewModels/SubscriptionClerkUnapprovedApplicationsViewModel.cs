using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SportzMagazine.Helpers;
using SportzMagazine.Models;

namespace SportzMagazine.ViewModels
{
    public class SubscriptionClerkUnapprovedApplicationsViewModel : BaseViewModel
    {
        #region instance fields

        private ObservableCollection<Subscription> _subscriptionList;

        #endregion

        public SubscriptionClerkUnapprovedApplicationsViewModel()
        {
            //test (create some dummy data)
            IndividualApplicant individualApplicant1 = new IndividualApplicant("harry", "somewhere street", "harry@mail.com", "12345678", "visa", "Harry CC name", "1234567812345678", DateTime.Today.AddMonths(2));
            IndividualApplicant individualApplicant2 = new IndividualApplicant("Hermoine", "oxford street", "hermoine@mail.com", "12345678", "mastercard", "Hermoine CC name", "1234567812345678", DateTime.Today.AddMonths(6));

            IndividualSubscription individualSubscription1 = new IndividualSubscription(individualApplicant1, 3, DateTime.Today.AddDays(8), DateTime.Today.AddMonths(6));
            IndividualSubscription individualSubscription2 = new IndividualSubscription(individualApplicant2, 9, DateTime.Today.AddDays(23), DateTime.Today.AddMonths(12));

            CorporateApplicant corporateApplicant1 = new CorporateApplicant("mr Anderson", "Piccadilly circus", "bluepill@mail.com", "12345678", "hacker");
            CorporateApplicant corporateApplicant2 = new CorporateApplicant("Morpheus", "Baker Street", "redpill@mail.com", "12345678", "mentor");

            CorporateSubscription corporateSubscription1 = new CorporateSubscription(corporateApplicant1, 4, DateTime.Today.AddDays(5), DateTime.Today.AddMonths(7));
            CorporateSubscription corporateSubscription2 = new CorporateSubscription(corporateApplicant2, 9, DateTime.Today.AddMonths(1), DateTime.Today.AddMonths(13));

            //add dummy data to the new list
            SubscriptionList = new ObservableCollection<Subscription>() { individualSubscription1, individualSubscription2 };

            VerifyCreditCardCommand = new RelayCommand(VerifyCreditCard);
        }

        private void VerifyCreditCard(object obj)
        {
            obj.GetType().ToString();
        }

        #region Properties
        public ObservableCollection<Subscription> SubscriptionList
        {
            get { return _subscriptionList; }
            set { _subscriptionList = value; OnPropertyChanged("SubscriptionList"); }
        }

        public RelayCommand VerifyCreditCardCommand
        {
            get { return _verifyCreditCardCommand; }
            set { _verifyCreditCardCommand = value; OnPropertyChanged("VerifyCreditCardCommand"); }
        }

        private RelayCommand _verifyCreditCardCommand;
        #endregion
    }
}
