using System;
using System.Collections.Generic;
using GrHw.Server.Domain;

namespace GrHw.Server.Repository.Implementation
{
    public class PersonRepository: IRepository<Person>
    {
        public Person Save(Person item)
        {
            return item;
        }

        public List<Person> GetList()
        {
           return new List<Person>();
        }
    }
}
