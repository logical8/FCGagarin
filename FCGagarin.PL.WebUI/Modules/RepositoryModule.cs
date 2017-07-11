using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace FCGagarin.PL.WebUI.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("FCGagarin.DAL.Repositories"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }
    }
}