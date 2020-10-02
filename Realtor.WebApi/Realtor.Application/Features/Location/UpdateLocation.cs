using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Realtor.Data;
using Realtor.Core.Extensions;
using Realtor.Core.Entities;

namespace Realtor.Application.Features.Location
{
    public class UpdateLocation
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(m => m.MlsNumber).NotEmpty();
                RuleFor(m => m.City).NotEmpty();
                RuleFor(m => m.State).NotEmpty();
                RuleFor(m => m.ZipCode).NotEmpty();
                RuleFor(m => m.Bathrooms).NotEmpty();
                RuleFor(m => m.Bedrooms).NotEmpty();
                RuleFor(m => m.SquareFeet).NotEmpty();
                RuleFor(m => m.IsActive).NotEmpty();
            }
        }

        public class Command : IRequest<Guid>
        {
            public Guid MlsNumber { get; set; }
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string Neighborhood { get; set; }
            public decimal SalesPrice { get; set; }
            public DateTimeOffset DateListed { get; set; }
            public int Bedrooms { get; set; }
            public int Bathrooms { get; set; }
            public int GarageSize { get; set; }
            public int SquareFeet { get; set; }
            public int LotSize { get; set; }
            public string Description { get; set; }
            public string Photos { get; set; }
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

                Locations entity = _mapper.Map<Command, Locations>(request);
                _db.Location.Add(entity);
                await _db.SaveChangesAsync(cancellationToken).AnyContext();

                return entity.Id;
            }
        }
    }
}
