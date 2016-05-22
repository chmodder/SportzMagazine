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

        public bool IsValidIndividualSubscriptionInfo(
            string name,
            string address,
            string emailAddress,
            string phoneNumber,
            int numberOfCopies,
            DateTime startDate,
            int duration, 
            string cardType,
            string cardHolderName,
            string cardNumber,
            DateTime cardExpirationDate)
        {
            //Return value default is set
            bool result = true;


            //Subscriptioninfo          
            IsValidatedList.Add(CheckStartdate(startDate));
            IsValidatedList.Add(CheckNumberOfCopies(numberOfCopies));
            IsValidatedList.Add(CheckDuration(duration));



            //Subscriberinfo
            IsValidatedList.Add(CheckName(name));
            IsValidatedList.Add(CheckAddress(address));
            IsValidatedList.Add(CheckEmail(emailAddress));
            IsValidatedList.Add(CheckPhoneNumber(phoneNumber));


            //creditcardinfo
            IsValidatedList.Add(CheckCreditcardName(cardHolderName));
            IsValidatedList.Add(CheckCreditCardNumber(cardNumber));
            IsValidatedList.Add(CheckCreditcardType(cardType));
            IsValidatedList.Add(CheckCreditcardExpirationDate(cardExpirationDate));



            //if all IndividualSubscription properties er true set all, then result is set to true, which means the user input is valid
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

        public bool IsValidCorporateSubscriptionInfo(string contactName, string jobTitle, string address, string emailAddress, string phoneNumber, int numberOfCopies, DateTime startDate, int duration)
        {
            //Return value default is set
            bool result = true;


            //Subscriptioninfo          
            IsValidatedList.Add(CheckStartdate(startDate));
            IsValidatedList.Add(CheckNumberOfCopies(numberOfCopies));
            IsValidatedList.Add(CheckDuration(duration));



            //Subscriberinfo
            IsValidatedList.Add(CheckName(contactName));
            IsValidatedList.Add(CheckJobTitle(jobTitle));
            IsValidatedList.Add(CheckAddress(address));
            IsValidatedList.Add(CheckEmail(emailAddress));
            IsValidatedList.Add(CheckPhoneNumber(phoneNumber));

            



            //if all IndividualSubscription properties er true set all, then result is set to true, which means the user input is valid
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


        private bool CheckCreditcardExpirationDate(DateTime cardExpirationDate)
        {
            //Sets the result (return value) to true or false depending if startDate has a value
            bool result = false;

            if (cardExpirationDate > DateTime.Today.AddDays(7))
            {
                result = true;
                return result;
            }

            //sets the error
            _errorMessageList.Add("The creditcard expiration date must be at least 1 week from today");

            //return the result value
            return result;
        }

        public StringBuilder ErrorMessagesToString()
        {
            //Creates the string which should contain all items in the ErrorMessageList
            StringBuilder errorMessageString = new StringBuilder();


            //If the ErrormessageList has items make a long string of all the errors with each item on a new line (by using "\n")
            if (ErrorMessageList.Count() != 0)
            {
                int lastItem = ErrorMessageList.Count() - 1;


                for (int i = 0; i < ErrorMessageList.Count(); i++)
                {
                    //Adds an item to the string
                    errorMessageString.Append(ErrorMessageList[i]);
                    //adds comma separation between each item. If it is the last item in the list, then a period is added.
                    errorMessageString.Append(i != lastItem ? ",\n " : ".");
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


        #endregion


        private bool CheckStartdate(DateTime startDate)
        {
            //Sets the result (return value) to true or false depending if startDate has a value
            bool result = false;

            if (startDate > DateTime.Today.AddDays(1))
            {
                result = true;
                return result;
            }

            //sets the error
            _errorMessageList.Add("The subscription start date can not be before tomorrow");

            //return the result value
            return result;
        }

        private bool CheckCreditcardType(string cardType)
        {
            //check if input is null or empty
            if (cardType == null)
            {
                _errorMessageList.Add("Credit card type is not set");
                return false;
            }

            return true;
        }

        private bool CheckDuration(int duration)
        {
            bool isNotZero = duration > 0;

            //Adds an error message to the list if duration is not set ("isNotZero" is false)
            if (!isNotZero)
            {
                _errorMessageList.Add("Duration is not set");
            }

            return isNotZero;
        }

        private bool CheckName(string name)
        {
            //check if input is null or empty
            if (name == null)
            {
                _errorMessageList.Add("A girl or a man has no name");
                return false;
            }

            //Check if input match validation rules for Name
            //if Name is validated flag true
            //else flag false
            bool isNameLengthMin = name.Length >= 2 && name.Length != 0;


            //Adds an errormessage to the list if name rules does not match input
            if (!isNameLengthMin)
            {
                _errorMessageList.Add("A girl or a man's name is too short");
            }
            return isNameLengthMin;
        }


        private bool CheckJobTitle(string jobTitle)
        {
            if (jobTitle == null)
            {
                _errorMessageList.Add("Jobtitle field is empty");
                return false;
            }

            //Check if input match validation rules for Address
            //if Name is validated flag true
            //else flag false
            bool isJobTitleLengthMin = jobTitle.Length >= 2 && jobTitle.Length != 0;


            //Adds an errormessage to the list if name rules does not match input
            if (!isJobTitleLengthMin)
            {
                _errorMessageList.Add("Jobtitle is too short");
            }
            return isJobTitleLengthMin;
        }

        private bool CheckAddress(string address)
        {
            if (address == null)
            {
                _errorMessageList.Add("Address field is empty");
                return false;
            }

            //Check if input match validation rules for Address
            //if Name is validated flag true
            //else flag false
            bool isAddressLengthMin = address.Length >= 2 && address.Length != 0;


            //Adds an errormessage to the list if name rules does not match input
            if (!isAddressLengthMin)
            {
                _errorMessageList.Add("Name is too short");
            }
            return isAddressLengthMin;
        }


        private bool CheckEmail(string emailAddress)
        {
            if (emailAddress == null)
            {
                _errorMessageList.Add("Email field is empty");
                return false;
            }

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
            if (number == null)
            {
                _errorMessageList.Add("Phone number is empty");
                return false;
            }

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
                _errorMessageList.Add("Phone number must be 8 characters and not contain symbols, letters or countrycode");
            }
            return isDkPhoneNumber;
        }



        private bool CheckCreditcardName(string cardHolderName)
        {

            bool creditCardNameIsValid = false;
            string errorMsg = "(Cardholders name field)";

            //check if input is null or empty
            if (cardHolderName == null)
            {
                errorMsg = string.Format("A girl or a man has no name {0}", errorMsg);
                _errorMessageList.Add(errorMsg);
                return creditCardNameIsValid;
            }

            //Check if input match validation rules for Name
            //if Name is validated flag true
            //else flag false
            creditCardNameIsValid = cardHolderName.Length >= 2 && cardHolderName.Length != 0;

            //Adds an errormessage to the list if name rules does not match input
            if (!creditCardNameIsValid)
            {
                errorMsg = string.Format("A girl or a man's name is too short {0}", errorMsg);
                _errorMessageList.Add(errorMsg);
            }


            return creditCardNameIsValid;
        }

        private bool CheckCreditCardNumber(string cardnumber)
        {
            if (cardnumber == null)
            {
                _errorMessageList.Add("Credit card number is empty");
                return false;
            }

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
