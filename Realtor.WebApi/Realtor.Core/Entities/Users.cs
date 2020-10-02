using Realtor.Core.SharedKernel;

namespace Realtor.Core.Entities
{
    public class Users : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
