// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Domain.Commons;

namespace Domain.Entities
{
    public class CompanyTenant : IDeleteAudit, IDataKeyFilter
    {
        public CompanyTenant(int authPTenantId, string fullName, string dataKey)
        {
            if (authPTenantId == 0) throw new ArgumentNullException(nameof(authPTenantId));

            AuthPTenantId = authPTenantId;
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            DataKey = dataKey ?? throw new ArgumentNullException(nameof(dataKey));
            var listName = fullName.Split('|');
            ShortName = listName[listName.Length - 1];
            IsDeleted = false;
        }
        public Guid CompanyTenantId { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        /// <summary>
        /// This contains the datakey from the AuthP's Tenant
        /// </summary>
        public string DataKey { get; set; }

        /// <summary>
        /// This contains the Primary key from the AuthP's Tenant
        /// </summary>
        public int AuthPTenantId { get; set; }
        [NotMapped]
        public string InventoryAlertEmail { get; set; }
        public bool? IsDeleted { get; set; }
        public string? ShopName { get; set; }
        public string? ContactEmailAddress { get; set; }
        public string? DeliveryNoteMessage { get; set; }
        public string? DeliveryNoteMessageHeader  { get; set; }
        public string? SiteAddress { get; set; }
        public string? SiteAddressName { get; set; }
        public string? CompanyName { get; set; }
        public string? LogoImage { get; set; }
        public int? ShipmentCreateInterval { get; set; }

        public void UpdateDataKey(string newDataKey)
        {
            DataKey = newDataKey;
        }
        public void UpdateNames(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentException("The FullName cannot be null or empty");

            FullName = fullName;
            var listName = fullName.Split('|');
            ShortName = listName[listName.Length -1];
        }
    }
}