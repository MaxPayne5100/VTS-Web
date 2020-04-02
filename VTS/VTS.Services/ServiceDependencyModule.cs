using Autofac;
using VTS.Services.AuthenticationService;

namespace VTS.Services
{
    public class ServiceDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationService.AuthenticationService>()
                .As<IAuthenticationService>();
        }
    }
}