using Autofac;
using VTS.Repos.UnitOfWork;

namespace VTS.Repos
{
    public class ReposDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>();
        }
    }
}