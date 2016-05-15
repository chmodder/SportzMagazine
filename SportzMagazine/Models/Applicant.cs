using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace SportzMagazine.Models
{
    [DataContract]
    public class Applicant
    {
        [DataMember]
        private string _address;
        [DataMember]
        private string _emailAddress;
        [DataMember]
        private string _phoneNumber;

        public Applicant(string address, string emailAddress, string phoneNumber)
        {
            this.Address = address;
            this.EmailAddress = emailAddress;
            this.PhoneNumber = phoneNumber;
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
            }
        }
    }
}
