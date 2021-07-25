using Actvt.Domain;
using AutoMapper;

namespace Actvt.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity,Activity>();
        }
    }
}