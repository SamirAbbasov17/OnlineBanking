using BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers.Models;

namespace BankSystem.Web.Infrastructure.Helpers.GlobalTransferHelpers
{
    public interface IGlobalTransferHelper
    {
        Task<GlobalTransferResult> TransferMoneyAsync(GlobalTransferDto model);
    }
}
