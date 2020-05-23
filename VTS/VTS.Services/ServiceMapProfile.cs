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
            CreateMap<Core.DTO.Employee, DAL.Entities.Employee>();
            CreateMap<Core.DTO.Manager, DAL.Entities.Manager>();
            CreateMap<Core.DTO.Head, DAL.Entities.Head>();
            CreateMap<Core.DTO.UserVacationInfo, DAL.Entities.UserVacationInfo>();
            CreateMap<Core.DTO.Holiday, DAL.Entities.Holiday>();

            // Entities -> DTO
            CreateMap<DAL.Entities.User, Core.DTO.User>();
            CreateMap<DAL.Entities.UserVacationInfo, Core.DTO.UserVacationInfo>();
            CreateMap<DAL.Entities.Employee, Core.DTO.Employee>();
            CreateMap<DAL.Entities.Manager, Core.DTO.Manager>();
            CreateMap<DAL.Entities.Head, Core.DTO.Head>();
            CreateMap<DAL.Entities.Holiday, Core.DTO.Holiday>();
        }
    }
}