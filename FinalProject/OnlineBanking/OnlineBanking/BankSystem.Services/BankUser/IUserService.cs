﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services.BankUser
{
    public interface IUserService
    {
        Task<string> GetUserIdByUsernameAsync(string username);
        Task<string> GetAccountOwnerFullnameAsync(string userId);
    }
}
