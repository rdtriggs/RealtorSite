using System;

namespace Realtor.Core.SharedKernel
{
    public interface IAuditableEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        string CreatedById { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        string UpdatedById { get; set; }
        DateTimeOffset UpdatedDate { get; set; }
    }
}
