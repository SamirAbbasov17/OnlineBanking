using MainApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApi.Services
{
    public abstract class BaseService
    {
        protected readonly MainApiDbContext Context;

        protected BaseService(MainApiDbContext context)
            => this.Context = context;
    }
}
