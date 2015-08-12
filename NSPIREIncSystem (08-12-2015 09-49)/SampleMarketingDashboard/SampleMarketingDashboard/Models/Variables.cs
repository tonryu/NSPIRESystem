using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSPIREIncSystem.Models
{
    class Variables
    {
        public static string SchoolYear { get; set; }

        //timer
        public static bool toClose { get; set; }
        //for promissory
        //public static string pStudentNumber { get; set; }

        //public static string DeptName { get; set; }
        public static string Name { get; set; }
        public static string ULastName { get; set; }
        public static string UFirstName { get; set; }
        public static string UEmpNo { get; set; }
        //public static string img { get; set; }
        public static bool yesClicked { get; set; }
        public static bool outreachClicked { get; set; }
        public static bool targetClicked { get; set; }
        //Student Info
        //public static string StudentNumber { get; set; }
        //public static string LastName { get; set; }
        //public static string Firstname { get; set; }
        //public static string MiddleName { get; set; }
        //public static string Gender { get; set; }
        //public static string Birthday { get; set; }
        //public static string Age { get; set; }
        //public static string BirthPlace { get; set; }
        //public static string FathersName { get; set; }
        //public static string FathersOccupation { get; set; }
        //public static string MothersName { get; set; }
        //public static string MothersOccupation { get; set; }

        ////Contact Info
        //public static string Address1 { get; set; }
        //public static string Address2 { get; set; }
        //public static string City { get; set; }
        //public static string Province { get; set; }
        //public static string Zip { get; set; }
        //public static string Landline { get; set; }
        //public static string Mobile { get; set; }

        ////PCCE
        //public static string CName { get; set; }
        //public static string CRelationship { get; set; }
        //public static string CLandline { get; set; }
        //public static string CMobile { get; set; }

        ////SY
        //public static string SYId { get; set; }
        //public static string SYDesc { get; set; }
        //public static string StartDate { get; set; }
        //public static string EndDate { get; set; }

        ////after login
        ////From Blain's Variables added July 18,2014
        //public static string SystemUserName { get; set; }
        public static string LeadManagementAccess { get; set; }
        public static string CustomerServiceAccess { get; set; }
        public static string TaskManagementAccess { get; set; }
        //public static string GradesAccess { get; set; }
        public static string SystemParameterAccess { get; set; }
        public static bool IsAdmin { get; set; }



        //for animation of menu
        //From Blain's Variables added July 18,2014
        public static bool collapseUserContent { get; set; }
        public static bool collapseMenu { get; set; }

        public static bool transactionStarted { get; set; }

        //USER
    //    public static string EmpNo { get; set; }
    //    public static string EmpFullName { get; set; }
    //    public static bool FullControl { get; set; }
    //    public static bool Admin { get; set; }
    //    public static bool Registrar { get; set; }
    //    public static bool Cashier { get; set; }
    //    public static bool Teacher { get; set; }

    //    //book
    //    public static int selectedItemId { get; set; }

    //    //for browsing image
    //    public static bool hasSearched { get; set; }
    }

    //public class UserEmployee
    //{
    //    public string ueFullName { get; set; }
    //    public string ueEmpNo { get; set; }
    //    public string ueDept { get; set; }
    //    public string ueDeptId { get; set; }
    //    public string ueRegistrarAccess { get; set; }
    //    public string ueCashierAccess { get; set; }
    //    public string ueAdmissionAccess { get; set; }
    //    public string ueGradesAccess { get; set; }
    //    public string ueSysParAccess { get; set; }
    //    public string ueIsAdmin { get; set; }
    //}

    //public class FeeTable
    //{
    //    public int sFeeId { get; set; }
    //    public int sFeeCatId { get; set; }
    //    public string sFeeCatDesc { get; set; }
    //    public string sFeeDesc { get; set; }
    //    public string sApplication { get; set; }
    //    public string sLabCode { get; set; }
    //    public int sProgramId { get; set; }
    //    public string sProgramName { get; set; }
    //}

    //public class FeeCatTable
    //{
    //    public int fcFeeCatId { get; set; }
    //    public string fcFeeCatDesc { get; set; }
    //}

    //public class AnnualFees
    //{
    //    public int aFeesTermYearId { get; set; }
    //    public string aSchoolYearId { get; set; }
    //    public string aSchoolYearDescription { get; set; }
    //    public int aFeeCode { get; set; }
    //    public string aFeeDesc { get; set; }
    //    public string aAmount { get; set; }
    //    public string AcadProgram { get; set; }
    //}

    //class StudentInformationViews
    //{
    //    public StudentInformationViews() { }
    //    public string StudentNumber { get; set; }
    //    public string FullName { get; set; }
    //    public string SchoolLevelDescription { get; set; }
    //    public string Address { get; set; }
    //    public string BirthDate { get; set; }
    //    public int Age { get; set; }
    //    public string BirthPlace { get; set; }
    //    public string Gender { get; set; }
    //    public string Religion { get; set; }
    //    public string MobileNumber { get; set; }
    //    public string LandlineNumber { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string FatherName { get; set; }
    //    public string FatherOccupation { get; set; }
    //    public string MotherName { get; set; }
    //    public string MotherOccupation { get; set; }
    //    public string RelativeName { get; set; }
    //    public string Relationship { get; set; }
    //    public string ContactNumber { get; set; }
    //    public string RelativeAddress { get; set; }
    //    public string ImagePath { get; set; }
    //    public string Status { get; set; }
    //    public string EnrollmentDate { get; set; }
    //    public string ProgramName { get; set; }
    //    public string SectionName { get; set; }
    //    public string SchoolYearId { get; set; }
    //}

    //class CurriculumView
    //{
    //    public CurriculumView() { }
    //    public int CurriculumId { get; set; }
    //    public string CurriculumName { get; set; }
    //    public string Description { get; set; }
    //    public string SchoolYearId { get; set; }
    //    public string SchoolYearDescription { get; set; }
    //}

    //class CardView
    //{
    //    public CardView() { }
    //    public string CardId { get; set; }
    //    public string ServesAs { get; set; }
    //    public string Status { get; set; }
    //    public string IsActive { get; set; }
    //    public string ExpiryDate { get; set; }
    //}

    //class SectionView
    //{
    //    public SectionView() { }
    //    public int SectionId { get; set; }
    //    public string SectionName { get; set; }
    //    public string ProgramName { get; set; }
    //    public string ProgramSectionName { get; set; }
    //    public string Adviser { get; set; }
    //    public string Capacity { get; set; }


    //}

    //class ProgramView
    //{
    //    public ProgramView() { }
    //    public int ProgramId { get; set; }
    //    public string ProgramName { get; set; }
    //    public string AcademicDepartmentName { get; set; }
    //    public string AcademicDepartmentProgramName { get; set; }
    //    public string Remarks { get; set; }
    //}

    //class InquiriesView
    //{
    //    public InquiriesView() { }
    //    public string iInqNumber { get; set; }
    //    public string iFullName { get; set; }
    //    public string iBirthDate { get; set; }
    //    public string iBirthPlace { get; set; }
    //    public string iGender { get; set; }
    //    public string iAddress { get; set; }
    //    public string iLandLine { get; set; }
    //    public string iMobile { get; set; }
    //    public string iEmail { get; set; }
    //    public string iInquiryDate { get; set; }
    //    public string iInquiryType { get; set; }
    //    public string iSchool { get; set; }
    //    public string iYear { get; set; }
    //    public string iProgram { get; set; }
    //    public string iContactPerson { get; set; }
    //    public string iRelationship { get; set; }
    //    public string iContactNumber { get; set; }
    //    public string iContactAddress { get; set; }

    //}

    //class CollectionTable
    //{
    //    public CollectionTable() { }
    //    public string cSchoolYearId { get; set; }
    //    public string cSchoolYearDesc { get; set; }
    //    public string cTransactionNo { get; set; }
    //    public string cORNo { get; set; }
    //    public DateTime cPaymentDate { get; set; }
    //    public string cPaymentTime { get; set; }
    //    public string cStudentNumber { get; set; }
    //    public string cStudentFullName { get; set; }
    //    public string cTransactionType { get; set; }
    //    public string cAmountPaid { get; set; }
    //    public string cUserId { get; set; }
    //    public string cUsername { get; set; }
    //    public string cPermitType { get; set; }
    //    public string cIncomeId { get; set; }
    //    public string cIncomeDesc { get; set; }
    //    public string cCancelUserId { get; set; }
    //    public string cCancelled { get; set; }
    //    public string cCancelDate { get; set; }
    //    public string cCancelTime { get; set; }
    //}

    //class BillingDetailsView
    //{
    //    public BillingDetailsView() { }
    //    public string StudentNumber { get; set; }
    //    public string StudentFullName { get; set; }
    //    public string ProgramSection { get; set; }
    //    public string CurrentAccountAmount { get; set; }//amount of current account
    //    public string BackAccountAmount { get; set; }//past account's balance
    //    public string PaidAmount { get; set; }//total payments on current account
    //    public string DiscountAmount { get; set; }//discount on current account
    //    public string TotalBalance { get; set; }//current account + back account amount - payments - discount
    //    public string AmountDue { get; set; }//current account's amount due
    //    public string RecentPaymentDate { get; set; }//current account's recent payment
    //    public string PaymentStatus { get; set; }//fully paid/active/inactive
    //}

    //class SchoolsFees
    //{
    //    public SchoolsFees() { }
    //    public string SchoolYearId { get; set; }
    //    public string FeeCode { get; set; }
    //    public string FeeDescription { get; set; }
    //    public string ProgramId { get; set; }
    //    public string Amount { get; set; }
    //    public int FeeCatCode { get; set; }
    //}

    //class InquiryWindow
    //{
    //    public InquiryWindow() { }
    //    public string inqInqNumber { get; set; }
    //    public string inqFullName { get; set; }
    //    public string inqBirthDate { get; set; }
    //}

    //class RegView
    //{
    //    public RegView() { }
    //    public string RegistrationId { get; set; }
    //    public string Number { get; set; }
    //    public string Name { get; set; }
    //    public string TableOrigin { get; set; }
    //    public string SchoolYearId { get; set; }
    //    public string SchoolYearDesc { get; set; }
    //}



    //class EmployeesView
    //{
    //    public EmployeesView() { }
    //    public string EmployeeNumber { get; set; }
    //    public string FullName { get; set; }
    //    public string Address { get; set; }
    //    public string EmployeeDepartmentId { get; set; }
    //    public string EmployeeDepartmentName { get; set; }
    //    public string Gender { get; set; }
    //    public string BirthDate { get; set; }
    //    public int Age { get; set; }
    //    public string BirthPlace { get; set; }
    //    public string Religion { get; set; }
    //    public string LandlineNumber { get; set; }
    //    public string MobileNumber { get; set; }
    //    public string EmailAddress { get; set; }
    //    public string EmploymentStatus { get; set; }
    //    public string EmploymentStartDate { get; set; }
    //    public string EmploymentEndDate { get; set; }
    //    public string IsActive { get; set; }
    //    public string ImagePath { get; set; }
    //}

    //class ScheduleView
    //{
    //    public ScheduleView() { }
    //    public int ScheduleId { get; set; }
    //    public string ScheduledTime { get; set; }
    //    public string DailyTeacherName { get; set; }
    //    public string DailyRoomDescription { get; set; }
    //    public string Recurrence { get; set; }
    //    public int SlotsPrepared { get; set; }
    //    public string SchoolYear { get; set; }
    //    public string SectionName { get; set; }
    //    public string SubjectCode { get; set; }
    //    public string SubjectName { get; set; }
    //    public decimal Units { get; set; }
    //}

    //class ScheduleDayView
    //{
    //    public ScheduleDayView() { }
    //    public int ScheduleDayId { get; set; }
    //    public int ScheduleId { get; set; }
    //    public string DayOfWeek { get; set; }
    //    public string StartTime { get; set; }
    //    public string EndTime { get; set; }
    //    public string RoomDescription { get; set; }
    //    public string TeacherName { get; set; }
    //}

    //class RoomsViews
    //{
    //    public RoomsViews() { }
    //    public string RoomNumber { get; set; }
    //    public string RoomDescription { get; set; }
    //    public string BuildingDescription { get; set; }
    //    public int Capacity { get; set; }
    //    public string Aircon { get; set; }
    //}

    //class WeeklySchedule
    //{
    //    public WeeklySchedule() { }
    //    public int GridRowFromBeginningTime { get; set; }
    //    public int GridRowSpanFromNumberOfHours { get; set; }
    //    public int GridColumnFromDayOfWeek { get; set; }
    //    //
    //    public string DayOfWeek { get; set; }
    //    public string SubjectName { get; set; }
    //    public string ScheduleTime { get; set; }
    //    public string RoomDescription { get; set; }
    //    public string SectionName { get; set; }
    //    public string TeacherName { get; set; }
    //}

    //class SubjectsView
    //{
    //    public SubjectsView() { }
    //    public string SubjectCode { get; set; }
    //    public string SubjectName { get; set; }
    //    public string SubjectDescription { get; set; }
    //    public string Prerequisite { get; set; }
    //    public string CurriculumName { get; set; }
    //    public string Units { get; set; }
    //    public string ProgramName { get; set; }
    //    public string SubjectUnder { get; set; }
    //    public string HasMain { get; set; }
    //    public decimal Percentage { get; set; }
    //}

    //class LedgerTransactionVew
    //{
    //    public LedgerTransactionVew() { }
    //    public string PaymentId { get; set; }
    //    public string Debit { get; set; }
    //    public string Credit { get; set; }
    //    public string TotalBalance { get; set; }
    //    public string CashierName { get; set; }
    //    public string TransactionDate { get; set; }
    //}

    //class PromissoryView
    //{
    //    public PromissoryView() { }
    //    public string StudentNumber { get; set; }
    //    public string FullName { get; set; }
    //    public int SequenceNumber { get; set; }
    //    public DateTime DueDate { get; set; }
    //    public string UserName { get; set; }
    //    public string EmployeeName { get; set; }
    //    public string SystemDate { get; set; }
    //    public string SystemTime { get; set; }
    //    public string Amount { get; set; }
    //    public string Status { get; set; }
    //    public string IsPaid { get; set; }
    //}

    //class RegisteredStudent
    //{
    //    public RegisteredStudent() { }
    //    public string regStudentNumber { get; set; }
    //    public string regFullName { get; set; }
    //}

    //class StudentCardView
    //{
    //    public StudentCardView() { }
    //    public string StudentNumber { get; set; }
    //    public string FullName { get; set; }
    //    public string YearLevel { get; set; }
    //    public string TaggingStatus { get; set; }
    //    public string ImagePath { get; set; }
    //}

    //class EmployeeCardView
    //{
    //    public EmployeeCardView() { }
    //    public string EmployeeNumber { get; set; }
    //    public string FullName { get; set; }
    //    public string Department { get; set; }
    //    public string TaggingStatus { get; set; }
    //    public string ImagePath { get; set; }
    //}

    //class TeachersView
    //{
    //    public TeachersView() { }
    //    public string SubjectCode { get; set; }
    //    public string SubjectName { get; set; }
    //    public string Time { get; set; }
    //    public string Room { get; set; }
    //    public int ProgId { get; set; }
    //    public int Sectid { get; set; }
    //    public string ProgramSection { get; set; }
    //}

    //class StudentGradeView
    //{
    //    public StudentGradeView() { }
    //    public string StudentNumber { get; set; }
    //    public string LastName { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public decimal Grade { get; set; }
    //}

    //for grade import and export
    //[DelimitedRecord(",")]
    //public class RawGrade
    //{
    //    public string Col1 { get; set; }
    //    public string Col2 { get; set; }
    //    public string Col3 { get; set; }
    //    public string Col4 { get; set; }
    //    public string Col5 { get; set; }
    //}

    //[DelimitedRecord(",")]
    //public class GradeFormat
    //{
    //    public string StudentNumber { get; set; }
    //    public string LastName { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public decimal Grade { get; set; }
    //}

    //class TeacherGradeView
    //{
    //    public TeacherGradeView() { }
    //    public string StudentNumber { get; set; }
    //    public string FullName { get; set; }
    //    public string FirstGrading { get; set; }
    //    public string SecondGrading { get; set; }
    //    public string ThirdGrading { get; set; }
    //    public string FourthGrading { get; set; }
    //    public string Average { get; set; }
    //}

    //class OutreachView
    //{
    //    public OutreachView() { }
    //    public string oSchoolYearId { get; set; }
    //    public int oCTSchoolsPub { get; set; }
    //    public int oCTSchoolsPrv { get; set; }
    //    public int oAudiencePub { get; set; }
    //    public int oAudiencePrv { get; set; }
    //    public int oInquiriesPub { get; set; }
    //    public int oInquiriesPrv { get; set; }
    //    public int oEnrolledPub { get; set; }
    //    public int oEnrolledPrv { get; set; }
    //}

    //class TeacherGradeWindow
    //{
    //    public TeacherGradeWindow() { }
    //    public string StudentNumber { get; set; }
    //    public string FullName { get; set; }
    //    public string Grade { get; set; }
    //}

    //class GradeEntry
    //{
    //    public GradeEntry() { }
    //    public string geStudentNumber { get; set; }
    //    public string geStudentName { get; set; }
    //    public string geSubjectCode { get; set; }
    //    public string geGradingPeriod { get; set; }
    //    public string geSchoolYearId { get; set; }
    //    public decimal geStudentGrade { get; set; }
    //}

    //class GradesSubmitted
    //{
    //    public GradesSubmitted() { }
    //    public string SchoolYear { get; set; }
    //    public string SubjectCode { get; set; }
    //    public string SubjectDesc { get; set; }
    //    public string GradingPeriod { get; set; }
    //    public int ProgId { get; set; }
    //    public int SectId { get; set; }
    //    public string ProgramSection { get; set; }
    //}

    //class StudentGradesSubmitted
    //{
    //    public StudentGradesSubmitted() { }
    //    public string StudentNo { get; set; }
    //    public string StudentName { get; set; }
    //    public string Grade { get; set; }
    //}

    //class DiscountsView
    //{
    //    public DiscountsView() { }
    //    public int DiscountId { get; set; }
    //    public string DiscountDesc { get; set; }
    //    public decimal Percentage { get; set; }
    //    public string Remarks { get; set; }
    //    public string FeeCat { get; set; }
    //}

    //class ETView
    //{
    //    public ETView() { }
    //    public string SchoolYeadID { get; set; }
    //    public string SchoolYear { get; set; }
    //    public int TargetOld { get; set; }
    //    public int TargetNew { get; set; }
    //    public int ActualOld { get; set; }
    //    public int ActualNew { get; set; }
    //    public int TargetTotal { get; set; }
    //    public int ActualTotal { get; set; }
    //    public string AccomOld { get; set; }
    //    public string AccomNew { get; set; }
    //    public string AccomTotal { get; set; }
    //}
    //class Students
    //{
    //    public Students() { }
    //    public string StudNo { get; set; }
    //    public string FullName { get; set; }
    //    public string ProgramSection { get; set; }
    //    public int SectId { get; set; }
    //    public int ProgId { get; set; }
    //}

    //class GradeRecordView
    //{
    //    public GradeRecordView() { }
    //    public string SubjCode { get; set; }
    //    public string SubjName { get; set; }
    //    public decimal FirstGrade { get; set; }
    //    public decimal SecondGrade { get; set; }
    //    public decimal ThirdGrade { get; set; }
    //    public decimal FourthGrade { get; set; }
    //    public decimal Average { get; set; }
    //}
}
