using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace FCGagarin.PL.WebUI.Modules
{
    public class PresenterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("FCGagarin.BLL.Presenters"))
                .Where(t => t.Name.EndsWith("Presenter"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}