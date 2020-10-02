using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realtor.Core.SharedKernel;
using Realtor.Data.Extensions;

namespace Realtor.Data
{
    public abstract class EntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.EnsureKey();
        }
    }
}
