using System.Data.Entity;
using Autofac;
using FCGagarin.DAL.EF;

namespace FCGagarin.WebUI.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterType(typeof(FCGagarinContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
        }
    }
}