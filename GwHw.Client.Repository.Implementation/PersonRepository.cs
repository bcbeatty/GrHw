using GrHw.Client.Domain;
using GrHw.Client.Repository;

namespace GwHw.Client.Repository.Implementation
{
    public class PersonRepository: IPersonRepository
    {
        public Person GetByKey(int key)
        {
            return null;
        }

        public Person Update(Person item)
        {
            return item;
        }

        public Person Insert(Person item)
        {
            return item;
        }

        public void Delete(Person item)
        {
            //throw new System.NotImplementedException();
        }
    }
}