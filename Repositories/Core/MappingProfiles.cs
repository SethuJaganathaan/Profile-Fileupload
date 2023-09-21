using AutoMapper;
using Repositories.DTO.Information;
using Repositories.DTO.Users;
using Repositories.Entities;

namespace Repositories.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.Id, dest => dest.Ignore());

            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.Id, dest => dest.Ignore());

            CreateMap<Information, InformationCreateDTO>().ReverseMap();

            CreateMap<InformationUpdateDTO, Information>()
                .ForMember(dest => dest.InfoFile, opt => opt.Ignore());
        }
    }
}