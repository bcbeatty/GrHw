using Autofac;
using GrHw.Client.Business.Implementation;
using GrHw.Client.Repository;
using GwHw.Client.Repository.Implementation;

namespace GrHw.Client.Module
{
    public class ClientAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonRepository>().As<IPersonRepository>().SingleInstance();
            builder.RegisterType<PersonBatchProcessor>().AsImplementedInterfaces().SingleInstance();  
            builder.RegisterType<FileIORepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ByDateOfBirthPeopleReport>().AsImplementedInterfaces().Named<string>("ByDate").SingleInstance();
            builder.RegisterType<ByGenderPeopleReport>().AsImplementedInterfaces().Named<string>("ByGender").SingleInstance();
            builder.RegisterType<ByLastNamePeopleReport>().AsImplementedInterfaces().Named<string>("ByLastName").SingleInstance();         
        }
      
    }
}
