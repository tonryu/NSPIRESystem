using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.LeadManagement.Reports
{
    class ProductsReportData
    {
        public ProductsReportData() { }
        public string ReportHeader { get; set; }
        public string ReportTitle { get; set; }
        public int TotalProducts { get; set; }

        public List<ProductsReportDetails> details { get; set; }
        public List<LeadsPerProducts> leads { get; set; }
    }

    class ProductsReportDetails
    {
        public ProductsReportDetails() { }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Cost { get; set; }
        public string Category { get; set; }
    }

    class LeadsPerProducts
    {
        public LeadsPerProducts() { }
        public string LeadName { get; set; }
    }
}
