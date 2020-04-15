using Autofac;
using VTS.Repos.UnitOfWork;

namespace VTS.Repos
{
    /// <summary>
    /// Class for implementing dependency injection in Repos layer.
    /// </summary>
    public class ReposDependencyModule : Module
    {
        /// <summary>
        /// Method for DI using Autofac.
        /// </summary>
        /// <param name="builder">Autofac builer.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork.UnitOfWork>()
                .As<IUnitOfWork>();
        }
    }
}