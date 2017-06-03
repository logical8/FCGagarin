using System;
using System.Reflection;
using Autofac;

namespace FCGagarin.PL.WebUI.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }
    }
}