﻿using BankSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace MainApi.Models
{
    public class ReceiveTransactionModel
    {
        [MaxLength(ModelConstants.MoneyTransfer.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string SenderName { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string RecipientName { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string SenderAccountUniqueId { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.SwiftCodeMaxLength)]
        public string DestinationBankSwiftCode { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.NameMaxLength)]
        public string DestinationBankName { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.CountryMaxLength)]
        public string DestinationBankCountry { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string DestinationBankAccountUniqueId { get; set; }

        [Required]
        public string ReferenceNumber { get; set; }
    }
}
