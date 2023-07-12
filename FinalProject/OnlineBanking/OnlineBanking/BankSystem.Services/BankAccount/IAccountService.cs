using BankSystem.Services.Models.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.BankAccount
{
    public interface IAccountService
    {
        Task<IEnumerable<T>> GetAllAccountsByUserIdAsync<T>(string userId)
            where T : AccountBaseServiceModel;

        Task<string> CreateAsync(AccountCreateServiceModel model);

        Task<T> GetByUniqueIdAsync<T>(string uniqueId)
            where T : AccountBaseServiceModel;

        Task<T> GetByIdAsync<T>(string id)
            where T : AccountBaseServiceModel;

        Task<bool> ChangeAccountNameAsync(string accountId, string newName);

        Task<IEnumerable<T>> GetAccountsAsync<T>(int pageIndex = 1, int count = int.MaxValue)
            where T : AccountBaseServiceModel;

        Task<int> GetCountOfAccountsAsync();
    }
}
