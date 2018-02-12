using Autofac;
using GrHw.Server.Business;
using GrHw.Server.Business.Impementation;
using GrHw.Server.Domain;
using GrHw.Server.Repository;
using GrHw.Server.Repository.Implementation;

namespace GrHw.Server.Module
{
    public class ServerAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<PersonRepository>().As<IRepository<Person>>().SingleInstance();
            builder.RegisterType<PersonFactory>().As<IPersonFactory>().SingleInstance();
            builder.RegisterType<PersonLineProcessor>().As<ILineProcessor<Person>>();
           
        }
    }
}