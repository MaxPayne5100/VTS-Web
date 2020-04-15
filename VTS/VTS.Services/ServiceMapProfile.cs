using AutoMapper;

namespace VTS.Services
{
    /// <summary>
    /// Class for mapping DTOs with Entities.
    /// </summary>
    public class ServiceMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceMapProfile"/> class.
        /// </summary>
        public ServiceMapProfile()
        {
            // DTO -> Entities
            CreateMap<Core.DTO.User, DAL.Entities.User>();
            CreateMap<Core.DTO.UserVacationInfo, DAL.Entities.UserVacationInfo>();

            // Entities -> DTO
            CreateMap<DAL.Entities.User, Core.DTO.User>();
            CreateMap<DAL.Entities.UserVacationInfo, Core.DTO.UserVacationInfo>();
        }
    }
}