using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using SportzMagazine.Models;


namespace SportzMagazine.Helpers
{
    public class CustomValidation
    {
        #region Instance Fields
        private IndividualSubscription _theSubscriptionToBeValidated;
        private List<bool> _isValidatedList;
        private List<string> _errorMessageList;
        #endregion


        public CustomValidation()
        {


            _isValidatedList = new List<bool>();


            _errorMessageList = new List<string>();
        }


        #region public methods
        public bool IsValidIndividualSubscriptionInfo()
        {
            bool result = true;




            //Subscriptioninfo
            IsValidatedList.Add(CheckNumberOfCopies(_theSubscriptionToBeValidated.NumberOfCopies));


            //Subscriberinfo
            IsValidatedList.Add(CheckName(_theSubscriptionToBeValidated.TheIndividualApplicant.Name));
            IsValidatedList.Add(CheckAddress(_theSubscriptionToBeValidated.TheIndividualApplicant.Address));
            IsValidatedList.Add(CheckEmail(_theSubscriptionToBeValidated.TheIndividualApplicant.EmailAddress));
            IsValidatedList.Add(CheckPhoneNumber(_theSubscriptionToBeValidated.TheIndividualApplicant.PhoneNumber));




            //creditcardinfo
            IsValidatedList.Add(CheckName(_theSubscriptionToBeValidated.TheIndividualApplicant.CreditCardList[0].CardHolderName));
            IsValidatedList.Add(CheckCreditCardNumber(_theSubscriptionToBeValidated.TheIndividualApplicant.CreditCardList[0].CardNumber));


            //if all IndividualSubscription properties er true set allValidated to true
            foreach (bool b in _isValidatedList)
            {
                if (b == false)
                {
                    result = false;
                    break;
                }
            }


            return result;
        }
        public bool IsValidIndividualSubscriptionInfo(string name, string address, string emailAddress, string phoneNumber, int numberOfCopies, DateTime startDate, string cardHolderName, string cardNumber)
        {
            bool result = true;




            //Subscriptioninfo
            IsValidatedList.Add(CheckNumberOfCopies(numberOfCopies));


            //Subscriberinfo
            IsValidatedList.Add(CheckName(name));
            IsValidatedList.Add(CheckAddress(address));
            IsValidatedList.Add(CheckEmail(emailAddress));
            IsValidatedList.Add(CheckPhoneNumber(phoneNumber));




            //creditcardinfo
            IsValidatedList.Add(CheckName(cardHolderName));
            IsValidatedList.Add(CheckCreditCardNumber(cardNumber));


            //if all IndividualSubscription properties er true set allValidated to true
            foreach (bool b in _isValidatedList)
            {
                if (b == false)
                {
                    result = false;
                    break;
                }
            }


            return result;
        }
        public StringBuilder ErrorMessagesToString()
        {
            //Creates the string which should contain all items in the ErrorMessageList
            StringBuilder errorMessageString = new StringBuilder();


            //If the ErrormessageList has items make a long string of all the errors with each item on a new line (by using "\n")
            if (ErrorMessageList.Count() != 0)
            {
                string lastItem = ErrorMessageList.Last();


                foreach (string s in ErrorMessageList)
                {
                    //Adds an item to the string
                    errorMessageString.Append(s);
                    //adds comma separation between each item. If last item period is added.
                    errorMessageString.Append(s != lastItem ? ",\n " : ".");
                }


            }
            return errorMessageString;
        }
        #endregion


        #region private methods
        /// <summary>
        /// Checks if number of copies is a positive number and under 100. 
        /// </summary>
        /// <param name="numberOfCopies"></param>
        /// <returns></returns>
        private bool CheckNumberOfCopies(int numberOfCopies)
        {
            bool isBetween0And100 = numberOfCopies < 100 && numberOfCopies >= 0;
            if (!isBetween0And100)
            {
                _errorMessageList.Add("The number of copies needs to be between 0 and 100.");
            }


            return isBetween0And100;
        }


        #region dev info
        //A thought: this could maybe be refactored to have only one "checkItem" method with 2 parameters:
        //- object parameter for the object that needs checking
        //- rule parameter. eg. email which then import the email rules from another class called ValidationRules




        //public bool CheckStartDate()
        //{


        //}


        //public bool CheckNumberOfCopies(string numberOfCopies)
        //{


        //}
        #endregion


        private bool CheckName(string name)
        {
            //Check if input match validation rules for Name
            //if Name is validated flag true
            //else flag false
            bool isNameLengthMin = name.Length >= 2 || name.Length != 0;


            //Adds an errormessage to the list if name rules does not match input
            if (!isNameLengthMin)
            {
                _errorMessageList.Add("Name is too short");
            }
            return isNameLengthMin;
        }


        private bool CheckAddress(string address)
        {
            //Check if input match validation rules for Address
            //if Name is validated flag true
            //else flag false
            bool isAddressLengthMin = address.Length >= 2 || address.Length != 0;


            //Adds an errormessage to the list if name rules does not match input
            if (!isAddressLengthMin)
            {
                _errorMessageList.Add("Name is too short");
            }
            return isAddressLengthMin;
        }


        private bool CheckEmail(string emailAddress)
        {
            //Check if input match validation rules for Email
            //if Name is validated flag true
            //else flag false
            bool isEmail = Regex.IsMatch(emailAddress,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);


            //Regex found here http://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp


            //adds an errormessage to the list if email rules does not match input
            if (!isEmail)
            {
                _errorMessageList.Add("Email input was not recognized as an emailaddress");
            }


            return isEmail;
        }


        private bool CheckPhoneNumber(string number)
        {
            //output variable as integer from int.Parse
            int intNumber;
            //Tries to parse the string to integer and sets the boolean value according to its succes in parsing the string
            bool isNumber = int.TryParse(number, out intNumber);
            //bool isPhoneNumber = Regex.IsMatch(number, "^[0-9]$");


            //checks if number consists of exactly 8 characters
            bool isRightNumberLenght = intNumber.ToString().Length == 8;
            //Checks true if both statements above is true
            bool isDkPhoneNumber = isNumber && isRightNumberLenght;


            //Adds an errormessage to the errormessagelist if the input is not a phonenumber
            if (!isDkPhoneNumber)
            {
                _errorMessageList.Add("The phone number must be 8 characters and not contain countrycode");
            }
            return isDkPhoneNumber;
        }




        private bool CheckCreditCardNumber(string cardnumber)
        {


            //checks if number consists of exactly 8 characters
            bool isRightNumberLenght = cardnumber.Length == 16;


            //Adds an errormessage to the errormessagelist if the input is not a phonenumber
            if (!isRightNumberLenght)
            {
                _errorMessageList.Add("The creditcard number must be 16 characters");
            }
            return isRightNumberLenght;
        }




        //CreditCardExpirationDate, SubscriptionDuration and SubscriptionStartDate are not validated since the values should be predefined in the right format comboboxes. 
        #endregion


        #region properties
        public IndividualSubscription TheSubscriptionToBeValidated
        {
            get { return _theSubscriptionToBeValidated; }
            set { _theSubscriptionToBeValidated = value; }
        }


        private List<string> ErrorMessageList { get { return _errorMessageList; } set { _errorMessageList = value; } }


        private List<bool> IsValidatedList { get { return _isValidatedList; } set { _isValidatedList = value; } }
        #endregion




    }
}
