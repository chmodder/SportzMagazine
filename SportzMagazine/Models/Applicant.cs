using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(Applicant))]
    [XmlInclude(typeof(IndividualApplicant))]
    [XmlInclude(typeof(CorporateApplicant))]
    public class Applicant
    {
        private string _address;
        private string _emailAddress;
        private string _phoneNumber;

        public Applicant()
        {
            //This PARAMETERLESS constructor is required by the XMLSerializeer
        }

        public Applicant(string address, string emailAddress, string phoneNumber)
        {
            this.Address = address;
            this.EmailAddress = emailAddress;
            this.PhoneNumber = phoneNumber;
        }


        public string Address { get { return _address; } set { _address = value; } }

        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }

        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
    }
}
