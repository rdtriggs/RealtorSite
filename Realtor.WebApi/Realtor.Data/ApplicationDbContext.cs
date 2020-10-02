using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Realtor.Core.Entities;
using Realtor.Core.SharedKernel;
using Realtor.Core.Extensions;
using Z.EntityFramework.Plus;

namespace Realtor.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        private readonly IPrincipal _user;
        private bool _auditEnabled = true;
        private IDbContextTransaction _currentTransaction;

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext(DbContextOptions options, IPrincipal user) : base(options)
        {
            _user = user;
        }

        public DbSet<Locations> Location { get; set; }
        public DbSet<Users> Users { get; set; }

        // Audit tables
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }

        public bool SuppressThrowingInvalidOperationExceptionOnAuditDisable { get; set; }

        /// <summary>
        ///     In order to disable auditing, you must explicitly set
        ///     <see cref="SuppressThrowingInvalidOperationExceptionOnAuditDisable" /> to true, otherwise an
        ///     InvalidOperationException will be thrown.
        ///     NOTE: This is a sensitive operation and must be used sparingly and with justification.
        /// </summary>
        public void DisableAudit()
        {
            if (!SuppressThrowingInvalidOperationExceptionOnAuditDisable)
            {
                throw new InvalidOperationException(
                    "Cannot disable auditing without SuppressThrowingInvalidOperationExceptionOnAuditDisable being true.");
            }

            _auditEnabled = false;
        }

        public void EnableAudit()
        {
            _auditEnabled = true;
            SuppressThrowingInvalidOperationExceptionOnAuditDisable = false;
        }

        public async Task BeginTransaction(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken)
                .AnyContext();
        }

        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync(cancellationToken).AnyContext();

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();

                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Audit configuration
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) => context.AddRange(audit.Entries);
            AuditManager.DefaultConfiguration.Exclude(entity => true);
            AuditManager.DefaultConfiguration.Include<IChangeTrackable>();
            AuditManager.DefaultConfiguration.ExcludeProperty<IAuditableEntity>(entity => new
            {
                entity.CreatedById,
                entity.CreatedDate,
                entity.UpdatedById,
                entity.UpdatedDate
            });
            AuditManager.DefaultConfiguration.IgnorePropertyUnchanged = true;
        }

        private void ProcessAuditableEntries()
        {
            const string defaultUser = "System";
            IIdentity identity = _user?.Identity;
            string userId = identity != null ? ((ClaimsPrincipal)_user).GetUserId() : null;
            string userName = identity?.Name ?? (string.IsNullOrEmpty(userId) ? defaultUser : null);
            IEnumerable<EntityEntry<AuditableEntity>> entries = ChangeTracker.Entries<AuditableEntity>();

            foreach (EntityEntry<AuditableEntity> entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.CreatedById = userId;
                        entry.Entity.UpdatedById = userId;
                        break;
                    case EntityState.Modified:
                        entry.Property(entity => entity.CreatedById).IsModified = false;
                        entry.Property(entity => entity.CreatedDate).IsModified = false;
                        entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.UpdatedById = userId;
                        break;
                }
            }
        }

        public override int SaveChanges()
        {
            if (!_auditEnabled)
            {
                return base.SaveChanges();
            }

            Audit audit = new Audit();
            audit.PreSaveChanges(this);
            ProcessAuditableEntries();
            int rowsAffected = base.SaveChanges();
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction == null)
            {
                return rowsAffected;
            }

            audit.Configuration.AutoSavePreAction(this, audit);
            base.SaveChanges();

            return rowsAffected;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!_auditEnabled)
            {
                return await base.SaveChangesAsync(cancellationToken).AnyContext();
            }

            Audit audit = new Audit();
            audit.PreSaveChanges(this);
            ProcessAuditableEntries();
            int rowsAffected = await base.SaveChangesAsync(cancellationToken).AnyContext();
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction == null)
            {
                return rowsAffected;
            }

            audit.Configuration.AutoSavePreAction(this, audit);
            await base.SaveChangesAsync(cancellationToken).AnyContext();

            return rowsAffected;
        }
    }
}

