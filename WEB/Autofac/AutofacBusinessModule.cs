using Autofac;
using AutoMapper;
using DataAccess.Services.Concrete;
using DataAccess.Services.Interface;
using WEB.Automapper;

namespace WEB.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            _ = builder.RegisterAssemblyTypes(typeof(BaseRepository<>).Assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).InstancePerLifetimeScope();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CategoryMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            _ = builder.RegisterInstance(mapper);
        }
    }
}
