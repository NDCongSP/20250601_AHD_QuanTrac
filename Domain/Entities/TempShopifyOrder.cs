using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Commons;

namespace Domain.Entities
{
    public class TempShopifyOrder : AuditEntityBase
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string OrderId { get; set; }
        public string OrderName { get; set; }
        public string FinancialStatus { get; set; }
        public string FulfillmentStatus { get; set; }
        public string Gateway { get; set; }
        public string LocationId { get; set; }
        public string Currency { get; set; }
        public string TotalPrice { get; set; }
        public string SubTotalPrice { get; set; }
        public string Tags { get; set; }
        public string TotalDiscounts { get; set; }
        public string TotalTax { get; set; }
        public string Number { get; set; }
        public string OrderNumber { get; set; }
        public string PresentmentCurrency { get; set; }
        public string TotalWeight { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string OrderedCompanyName { get; set; }
        public string CountryCode { get; set; }
        public string ProvinceCode { get; set; }
        public string Shipping { get; set; }
        public string CustomerId { get; set; }
        public string Note { get; set; }
    }
}