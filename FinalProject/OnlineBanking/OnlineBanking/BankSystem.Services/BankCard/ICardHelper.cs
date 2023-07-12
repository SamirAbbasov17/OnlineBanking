using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.BankCard
{
    public interface ICardHelper
    {
        bool CheckLuhn(string creditCardNumber);

        string Generate16DigitNumber();

        string Generate3DigitSecurityCode();
    }
}
