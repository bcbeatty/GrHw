using System.Collections.Generic;
using GrHw.Server.Domain;

namespace GrHw.Server.Business
{
    public interface IPersonFactory
    {
        Person PostString(string inputLine);
        List<Person> GetList();
    }
}