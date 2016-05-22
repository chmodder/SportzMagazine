using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SportzMagazine.Models
{
    [XmlInclude(typeof(CorporateApplicant))]

    public class CorporateApplicant : Applicant
    {
        private string _contactName;
        private string _jobTitle;

        public CorporateApplicant()
        {
            //This is PARAMETERLESS constructor is required by the XMLSerializeer
        }


        public CorporateApplicant(
            string contactName,
            string address,
            string emailAddress,
            string phoneNumber,
            string jobTitle) : base(address, emailAddress, phoneNumber)
        {
            this.ContactName = contactName;
            this.JobTitle = jobTitle;




        }


        public string ContactName { get { return _contactName; } set { _contactName = value; } }

        public string JobTitle { get { return _jobTitle; } set { _jobTitle = value; } }
    }
}
