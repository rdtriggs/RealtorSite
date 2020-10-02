using AutoMapper;
using Realtor.Application.Features.Location;
using Realtor.Core.Entities;

namespace Realtor.Application.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<CreateLocation.Command, Locations>(MemberList.Source);
            CreateMap<Locations, CreateLocation.Command>();
            CreateMap<Locations, GetLocations.Model>();
            CreateMap<Locations, DeleteLocation.Command>();
        }
    }
}
