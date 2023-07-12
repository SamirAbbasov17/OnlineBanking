using BankSystem.Common.Utils;
using BankSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Services
{
    public abstract class BaseService
    {
        protected readonly BankSystemDbContext Context;

        protected BaseService(BankSystemDbContext context)
            => this.Context = context;

        protected bool IsEntityStateValid(object model)
            => ValidationUtil.IsObjectValid(model);
    }
}
