using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        public AuditableEntitySaveChangesInterceptor(
            IDateTime dateTime,
            ICurrentUserService currentUserService)
        {
            this._dateTime = dateTime;
            this._currentUserService = currentUserService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        public void UpdateEntities(DbContext? context)
        {

            if (context == null) return;

            var entities = context.ChangeTracker.Entries<BaseAuditableEntity>();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.Created = _dateTime.Now;
                    entity.Entity.CreatedBy = _currentUserService.UserId;
                }

                // Eklem içinde, bir işlem yapacaksam if olarak belirtirdim :)
                if (
                    entity.State == EntityState.Modified ||
                    entity.State == EntityState.Added)
                {
                    entity.Entity.LastModified = _dateTime.Now;
                    entity.Entity.LastModifiedBy = _currentUserService.UserId;
                }
            }
        }
    }
}
