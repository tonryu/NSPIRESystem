using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.Reports
{
    class ContactsReportData
    {
        public ContactsReportData() { }
        public string ReportHeader { get; set; }
        public string ReportTitle { get; set; }
        public int TotalContacts { get; set; }

         public List<ContactsReportDetail> details { get; set; }
    }
    class ContactsReportDetail
    {
        public ContactsReportDetail() { }
        public int ContactNo { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson  { get; set; }
        public string Position { get; set; }
        public string PhoneNo { get; set; }
      
    }
}
