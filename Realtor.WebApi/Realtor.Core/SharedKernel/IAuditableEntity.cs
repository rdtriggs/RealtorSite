using System;

namespace Realtor.Core.SharedKernel
{
    public interface IAuditableEntity : IAuditableEntity<Guid>, IEntity { }
}
