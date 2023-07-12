using MainApi.Services.Models.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApi.Services.Bank
{
    public interface IBanksService
    {
        Task<T> GetBankAsync<T>(string bankName, string swiftCode, string bankCountry)
           where T : BankBaseServiceModel;

        Task<IEnumerable<T>> GetAllBanksSupportingPaymentsAsync<T>()
            where T : BankBaseServiceModel;

        Task<T> GetBankByIdAsync<T>(string id)
            where T : BankBaseServiceModel;

        Task<T> GetBankByBankIdentificationCardNumbersAsync<T>(string identificationCardNumbers)
            where T : BankBaseServiceModel;
    }
}
