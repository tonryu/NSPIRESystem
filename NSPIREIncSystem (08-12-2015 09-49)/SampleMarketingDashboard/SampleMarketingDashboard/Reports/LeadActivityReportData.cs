using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.Reports
{
    class LeadActivityReportData
    {
        public LeadActivityReportData() { }
        public string ReportHeader { get; set; }
        public string ReportTitle { get; set; }
        public int TotalActivities { get; set; }

        public List<LeadActivityReportDetail> details { get; set; }
    }
    class LeadActivityReportDetail
    {
        public LeadActivityReportDetail() { }
        public int ActivityNo { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string ActivityDate { get; set; }
        public string ActivityTime { get; set; }
        public double Cost { get; set; }
        public string ClientResponse { get; set; }
        public string DetailsOfTransaction { get; set; }
        public string SalesRep { get; set; }
        public string MarketingVoucherNo { get; set; }
        public string NextStep { get; set; }
        public string DueDateOfNextStep { get; set; }
        public string ContactPerson { get; set; }
    }
}
