using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realtor.Core.SharedKernel;

namespace Realtor.Data.Extensions
{
    public static class EntityTypeConfigurationExtensions
    {
        public static void EnsureKey<TEntity>(this EntityTypeBuilder<TEntity> builder, bool autoGenerateKey = false)
            where TEntity : class, IEntity
        {
            if (!autoGenerateKey)
            {
                builder.Property(entity => entity.Id).ValueGeneratedNever();
            }

            builder.HasKey(entity => entity.Id);
        }
    }
}
