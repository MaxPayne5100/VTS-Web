using Autofac;
using VTS.Services.AuthenticationService;
using VTS.Services.BookingService;
using VTS.Services.EmployeeService;
using VTS.Services.ManagerService;
using VTS.Services.UserService;
using VTS.Services.UserVacationInfoService;

namespace VTS.Services
{
    /// <summary>
    /// Class for implementing dependency injection in Service layer.
    /// </summary>
    public class ServiceDependencyModule : Module
    {
        /// <summary>
        /// Method for DI using Autofac.
        /// </summary>
        /// <param name="builder">Autofac builer.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationService.AuthenticationService>()
                .As<IAuthenticationService>();
            builder.RegisterType<UserService.UserService>()
                .As<IUserService>();
            builder.RegisterType<UserVacationInfoService.UserVacationInfoService>()
                .As<IUserVacationInfoService>();
            builder.RegisterType<EmployeeService.EmployeeService>()
                .As<IEmployeeService>();
            builder.RegisterType<ManagerService.ManagerService>()
                .As<IManagerService>();
            builder.RegisterType<BookingService.BookingService>()
                .As<IBookingService>();
            builder.RegisterType<AbstractHandler>()
                .As<IHandler>();
        }
    }
}