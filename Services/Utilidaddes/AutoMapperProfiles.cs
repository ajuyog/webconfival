using AutoMapper;
using confinancia.Models.Graph;

namespace confinancia.Services.Utilidaddes
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MeGraphDTO, SettingsGraphDTO>();
        }
    }
}
