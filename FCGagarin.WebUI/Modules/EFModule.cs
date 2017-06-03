using Autofac;
using FCGagarin.DAL.EF;

namespace FCGagarin.PL.WebUI.Modules
{
    public class EFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterType(typeof(FCGagarinContext)).As(typeof(IContext)).InstancePerLifetimeScope();
        }
    }
}