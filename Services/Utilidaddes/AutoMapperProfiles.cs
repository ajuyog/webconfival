using AutoMapper;
using frontend.Models.Graph;

namespace frontend.Services.Utilidaddes
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MeGraphDTO, SettingsGraphDTO>();
        }
    }
}
