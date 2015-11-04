using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.LeadManagement.Reports
{
    class CustomersReportData
    {
        public CustomersReportData() { }
        public string ReportHeader { get; set; }
        public string ReportTitle { get; set; }
        public int TotalCustomers { get; set; }

        public List<CustomersReportDetail> details { get; set; }
    }
    
    class CustomersReportDetail
    {
        public CustomersReportDetail() { }
        public int CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string DateSigned { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FromLead { get; set; }
    }
}
