using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.SalesManagement.Reports
{
    class CustomerAccountsReportData
    {
        public CustomerAccountsReportData() { }
        public string ReportHeader { get; set; }
        public string ReportTitle { get; set; }
        public int TotalCustomerAccounts { get; set; }

        public List<CustomerAccountsReportDetail> details { get; set; }
    }

    class CustomerAccountsReportDetail
    {
        public CustomerAccountsReportDetail() { }
        public string AccountNumber { get; set; }
        public string Customer { get; set; }
        public string Territory { get; set; }
        public string Product { get; set; }
        public string ModeOfPayment { get; set; }
        public string Gross { get; set; }
        public string Discount { get; set; }
        public string ServiceCharge { get; set; }
        public string NetValue { get; set; }
    }
}
