using Autofac;

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
                .As<AuthenticationService.IAuthenticationService>();
            builder.RegisterType<UserService.UserService>()
                .As<UserService.IUserService>();
            builder.RegisterType<UserVacationInfoService.UserVacationInfoService>()
                .As<UserVacationInfoService.IUserVacationInfoService>();
            builder.RegisterType<EmployeeService.EmployeeService>()
                .As<EmployeeService.IEmployeeService>();
            builder.RegisterType<HeadService.HeadService>()
                .As<HeadService.IHeadService>();
            builder.RegisterType<ManagerService.ManagerService>()
                .As<ManagerService.IManagerService>();
            builder.RegisterType<BookingService.BookingService>()
                .As<BookingService.IBookingService>();
            builder.RegisterType<BookingService.AbstractHandler>()
                .As<BookingService.IHandler>();
        }
    }
}