using System.Collections.Generic;
using GrHw.Client.Domain;

namespace GrHw.Client.Business
{
    public interface IBatchProcessor<T> where T : class
    {
        string[] Load(string filename);
        List<T> Parse(string[] filename, char delimeter);
        void Save(List<T> people);
        List<string> GetReports(List<Person> people, char delimeter);
       
    }

     
}
