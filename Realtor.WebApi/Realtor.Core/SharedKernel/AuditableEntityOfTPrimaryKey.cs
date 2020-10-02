using System;

namespace Realtor.Core.SharedKernel
{
    public abstract class AuditableEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAuditableEntity<TPrimaryKey>
    {
        public string CreatedById { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UpdatedById { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
