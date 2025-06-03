using System;

using Domain.Commons;

namespace Domain.Entities
{
    public class Vendor: AuditEntityBase, IDataKeyFilter
    {
        public int Id { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? VendorCompanyName { get; set; }
        public string? HeadOfficeAddress { get; set; }
        public string? HeadOfficePhone { get; set; }
        public string? HeadOfficeFax { get; set; }
        public string? BillingAddress { get; set; }
        public string? BillingContactName { get; set; }
        public string? BillingPhone { get; set; }
        public string? BillingFax { get; set; }
        public string? BillingMail { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? BankAccountType { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankAccountHolder { get; set; }
        public string? Remarks { get; set; }
        public double? JamCharge { get; set; }
        public int? BillingDate { get; set; }
        public string? DataKey { get; set; }
        public EnumStatus Status { get; set; }
    }
}