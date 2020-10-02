using System;

namespace Realtor.Core.SharedKernel
{
    public abstract class AuditableEntity : AuditableEntity<Guid>, IAuditableEntity
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
    }
}
