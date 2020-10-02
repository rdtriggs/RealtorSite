using System;

namespace Realtor.Core.SharedKernel
{
    public abstract class Entity : Entity<Guid>, IEntity
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
    }
}
