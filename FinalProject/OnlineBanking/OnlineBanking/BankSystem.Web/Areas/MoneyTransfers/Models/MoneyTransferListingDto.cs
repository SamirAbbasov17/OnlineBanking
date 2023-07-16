﻿using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Services.Models.BankMoneyTransfer;

namespace BankSystem.Web.Areas.MoneyTransfers.Models
{
    public class MoneyTransferListingDto : IMapWith<MoneyTransferListingServiceModel>
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public DateTime MadeOn { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string ReferenceNumber { get; set; }
    }
}
