using BankSystem.Services.Models.BankCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.BankCard
{
    public interface ICardService
    {
        Task<bool> CreateAsync(CardCreateServiceModel model);

        Task<IEnumerable<T>> GetCardsAsync<T>(string userId, int pageIndex = 1, int count = int.MaxValue)
            where T : CardBaseServiceModel;

        Task<bool> DeleteAsync(string id);

        Task<T> GetAsync<T>(
            string cardNumber,
            string cardExpiryDate,
            string cardSecurityCode,
            string cardOwner)
            where T : CardBaseServiceModel;

        Task<T> GetAsync<T>(string id)
            where T : CardBaseServiceModel;

        Task<int> GetCountOfAllCardsOwnedByUserAsync(string userId);
    }
}
