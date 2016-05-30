using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(Subscription))]
    [XmlInclude(typeof(IndividualSubscription))]
    [XmlInclude(typeof(CorporateSubscription))]
    public class Subscription
    {
        private string _accountNumber;
        private bool _isApproved;
        private int _numberOfCopies;
        private DateTime _startDate;
        private DateTime _expirationDate;

        public Subscription()
        {
            //This empty PARAMETERLESS constructor is required by the XmlSerialiser
        }

        public Subscription(int numberOfCopies, DateTime startDate, DateTime expirationDate)
        {
            this.NumberOfCopies = numberOfCopies;
            this.StartDate = startDate;
            this.ExpirationDate = expirationDate;
            CreateNewAccountNumber();
            IsApproved = false;
        }

        #region Methods

        public void CreateNewAccountNumber()
        {
            Guid g = Guid.NewGuid();

            AccountNumber = GuidToStringUsingStringAndParse(g);
        }
        private static string GuidToStringUsingStringAndParse(Guid value)
        {
            var guidBytes = string.Format("0{0:N}", value);
            var bigInteger = BigInteger.Parse(guidBytes, NumberStyles.HexNumber);
            return bigInteger.ToString("N0", CultureInfo.InvariantCulture);
        }

        #endregion


        #region Properties
        public int NumberOfCopies { get { return _numberOfCopies; } set { _numberOfCopies = value; } }

        public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }

        public DateTime ExpirationDate { get { return _expirationDate; } set { _expirationDate = value; } }

        public string AccountNumber { get { return _accountNumber; } set { _accountNumber = value; } }

        public bool IsApproved { get { return _isApproved; } set { _isApproved = value; } }

        #endregion
    }
}
