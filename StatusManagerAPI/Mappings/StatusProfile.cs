using StatusManagerAPI.Models.Dtos;
using StatusManagerAPI.Models;
using AutoMapper;

namespace StatusManagerAPI.Mappings
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>().ReverseMap();
        }
    }
}
