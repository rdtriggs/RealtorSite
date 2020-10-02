using System;
using Realtor.Core.SharedKernel;

namespace Realtor.Core.Entities
{
    public class Locations : AuditableEntity
    {
        public string MlsNumber { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string SalesPrice { get; set; }
        public DateTimeOffset? DateListed { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? GarageSize { get; set; }
        public int? SquareFeet { get; set; }
        public int? LotSize { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsActive { get; set; }

    }
}
