using AutoMapper;
using BankSystem.Common;
using BankSystem.Common.AutoMapping.Interfaces;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.Models.BankMoneyTransfer
{
    public class MoneyTransferCreateServiceModel : MoneyTransferBaseServiceModel, IHaveCustomMapping
    {
        [MaxLength(ModelConstants.MoneyTransfer.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string SenderName { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string RecipientName { get; set; }

        [Required]
        public DateTime MadeOn { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string Source { get; set; }

        [Required]
        [MaxLength(ModelConstants.Account.UniqueIdMaxLength)]
        public string? DestinationAccountUniqueId { get; set; }

        [Required]
        public string ReferenceNumber { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<MoneyTransferCreateServiceModel, MoneyTransfer>()
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.DestinationAccountUniqueId));
        }
    }
}
