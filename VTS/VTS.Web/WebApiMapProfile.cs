using AutoMapper;

namespace VTS.Web
{
    /// <summary>
    /// Class for mapping Models with DTOs.
    /// </summary>
    public class WebApiMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiMapProfile"/> class.
        /// </summary>
        public WebApiMapProfile()
        {
            // Model -> DTO
            CreateMap<Models.ProfileModel, Core.DTO.User>();
            CreateMap<Models.ChangeBonusDayOffsModel, Core.DTO.UserVacationInfo>();

            // DTO -> Model
            CreateMap<Core.DTO.User, Models.ProfileModel>();
            CreateMap<Core.DTO.UserVacationInfo, Models.ChangeBonusDayOffsModel>();
        }
    }
}