using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Realtor.Data;
using Realtor.Core.Extensions;
using Realtor.Core.Entities;

namespace Realtor.Application.Features.Location
{
    public class DeleteLocation
    {
        //public class Query : IRequest<Guid>
        //{
        //    public string MlsNumber { get; set; }
        //    public string Street1 { get; set; }
        //    public string Street2 { get; set; }
        //    public string City { get; set; }
        //    public string State { get; set; }
        //    public string ZipCode { get; set; }
        //    public string Neighborhood { get; set; }
        //    public string SalesPrice { get; set; }
        //    public DateTimeOffset? DateListed { get; set; }
        //    public int? Bedrooms { get; set; }
        //    public int? Bathrooms { get; set; }
        //    public int? GarageSize { get; set; }
        //    public int? SquareFeet { get; set; }
        //    public int? LotSize { get; set; }
        //    public string Description { get; set; }
        //    public string PhotoUrl { get; set; }
        //    public bool IsActive { get; set; }
        //}

        public class Command : IRequest<Guid>
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

        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly ApplicationDbContext _db;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                Locations entity = await _db.Location.FirstOrDefaultAsync(x => x.MlsNumber == request.MlsNumber, cancellationToken)
                            .AnyContext();
                _db.Location.Remove(entity);
                await _db.SaveChangesAsync(cancellationToken).AnyContext();

                return entity.Id;
            }
        }
    }
}
