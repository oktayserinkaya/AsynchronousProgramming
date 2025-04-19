using Autofac;
using DataAccess.Services.Concrete;
using DataAccess.Services.Interface;

namespace WEB.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseRepository<>).Assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
        }
    }
}
