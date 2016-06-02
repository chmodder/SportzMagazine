using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportzMagazine.Models
{
    public class EmailApproval
    {
        #region Instance fields
        private string _email;
        private string _accountNumber;
        private DateTime _firstIssueDeliveryDate;
        private string _messageToApplicant;
        #endregion

        #region Constructors

        /// <summary>
        /// If 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="accountNumber"></param>
        /// <param name="emailAddress"></param>
        public EmailApproval(string message, string accountNumber, string emailAddress)
        {
            MessageToApplicant = message;
            AccountNumber = accountNumber;
            Email = emailAddress;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sends message to Applicant with AccountNumber
        /// </summary>
        public void SendMessageToApplicant()
        {
            //Send the email to the user using MessageToApplicant, AccountNumber and Email 
        }

        /// <summary>
        /// Sends Approvalmessage to Applicant with date for delivery of next issue and ACcountNumber
        /// </summary>
        /// <param name="subscriptionStartDate"></param>
        public void SendMessageToApplicant(DateTime subscriptionStartDate)
        {
            //Some logic could be implemeted here to calculate FirstIssueDeliveryDate, when we know which dates the magazine is releasing new issues.
            FirstIssueDeliveryDate = subscriptionStartDate;

            //Send the email to the user using MessageToApplicant, FirstIssueDeliveryDate, AccountNumber and Email 
        }
        #endregion

        #region Properties
        public string Email { get { return _email; } set { _email = value; } }

        public string AccountNumber { get { return _accountNumber; } set { _accountNumber = value; } }

        public string MessageToApplicant { get { return _messageToApplicant; } set { _messageToApplicant = value; } }

        public DateTime FirstIssueDeliveryDate
        {
            get { return _firstIssueDeliveryDate; }
            set { _firstIssueDeliveryDate = value; }
        }

        #endregion
    }
}
