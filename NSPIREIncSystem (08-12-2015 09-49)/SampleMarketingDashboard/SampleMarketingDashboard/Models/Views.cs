using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.Models
{
    // Views

    public class UsersLists
    {
        public UsersLists() { }
        public string UserAccountId { get; set; }
        public string EmployeeName { get; set; }
        public string LeadManagementAccess { get; set; }
        public string TaskManagementAccess { get; set; }
        public string CustomerServiceManagementAccess { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class EmployeeView
    {
        public EmployeeView() { }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string FullAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Territory { get; set; }
    }

    public class LeadsView
    {
        public LeadsView() { }
        public int LeadId { get; set; }
        public string ContactPerson { get; set; }
        public string Position { get; set; }
        public string TerritoryName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNo { get; set; }
        public string SalesStageStatus { get; set; }
    }

    public class ActivityView
    {
        public ActivityView() { }
        public int ActivityId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string ActivityDate { get; set; }
        public string ActivityTime { get; set; }
        public double Cost { get; set; }
        public string ClientResponse { get; set; }
        public string TransactionDetails { get; set; }
        public string SalesRep { get; set; }
        public string MarketingVoucher { get; set; }
        public string NextStep { get; set; }
        public string NextStepDueDate { get; set; }
        public string ContactPerson { get; set; }
        public bool IsFinalized { get; set; }
    }

    public class ContactView
    {
        public ContactView() { }
        public int ContactId { get; set; }
        public string ContactPersonName { get; set; }
        public string PhoneNo { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
    }

    public class SalesStagesView 
    {
        public SalesStagesView() { }
        public int SalesStageID { get; set; }
        public string SalesStageName { get; set; }
        public int RankNo { get; set; }

    }

    public class LeadsListBox
    {
        public LeadsListBox() { }
        public string CompanyName { get; set; }
    }

    public class CustomerAccountsView
    {
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

    // Classes

    public class UserAccount
    {
        [Key]
        public string UserAccountId { get; set; }
        public int EmployeeId { get; set; }
        public string Password { get; set; }
        public string LeadManagementAccess { get; set; }
        public string TaskManagementAccess { get; set; }
        public string CustomerServiceAccess { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Territory { get; set; }
    }

    public class Lead
    {
        [Key]
        public int LeadID { get; set; }
        public int TerritoryID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Status { get; set; }
        public string DateAdded { get; set; }
        public bool IsActive { get; set; }
        public int MarketingStrategyId { get; set; }
    }

    public class Territory
    {
        [Key]
        public int TerritoryID { get; set; }
        public string TerritoryName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }

    public class LeadActivity
    {
        [Key]
        public int ActivityID { get; set; }
        public int LeadID { get; set; }
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
        public int ContacId { get; set; }
        public bool IsFinalized { get; set; }
    }

    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public int LeadId { get; set; }
        public string ContactPersonName { get; set; }
        public string Position { get; set; }
        public string PhoneNo { get; set; }
    }

    public class SalesStage
    {
        [Key]
        public int SalesStageID { get; set; }
        public string SalesStageName { get; set; }
        public int RankNo { get; set; }
    }

    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public int LeadID { get; set; }
        public string DateSigned { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }

    public class MarketingStrategy
    {
        [Key]
        public int MarketingStrategyId { get; set; }
        public string Description { get; set; }
    }

    public class CustomerAccount
    {
        [Key]
        public string AccountNumber { get; set; }
        public int CustomerID { get; set; }
        public int TerritoryID { get; set; }
        public int ProductID { get; set; }
        public string ModeOfPayment { get; set; }
        public string Gross { get; set; }
        public string Discount { get; set; }
        public string ServiceCharge { get; set; }
        public string NetValue { get; set; }
    }

    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Cost { get; set; }
        public int CategoryID { get; set; }
    }
}
