using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Realtor.Data;
using Realtor.Core.Extensions;

namespace Realtor.Application.Features.Location
{
    public class GetLocations
    {
        public class Query : IRequest<IEnumerable<Model>> { }

        public class Model
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

        public class Handler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly ApplicationDbContext _db;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Location.AsNoTracking()
                   .ProjectTo<Model>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken)
                   .AnyContext();
            }
        }
    }
}

