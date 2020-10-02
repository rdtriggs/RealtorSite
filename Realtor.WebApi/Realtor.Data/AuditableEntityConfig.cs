using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realtor.Core;
using Realtor.Core.SharedKernel;
using Realtor.Data.Extensions;

namespace Realtor.Data
{
    public abstract class AuditableEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.EnsureKey();

            // properties
            builder.Property(entity => entity.CreatedById).HasMaxLength(Constants.Entity.Common.CreatedByIdMaxLength);
            builder.Property(entity => entity.UpdatedById).HasMaxLength(Constants.Entity.Common.UpdatedByIdMaxLength);

            // indexes
            builder.HasIndex(entity => entity.CreatedById);
            builder.HasIndex(entity => entity.UpdatedById);
        }
    }
}
