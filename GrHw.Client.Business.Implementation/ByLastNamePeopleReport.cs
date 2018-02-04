using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrHw.Client.Domain;

namespace GrHw.Client.Business.Implementation
{
    public class ByLastNamePeopleReport : IPeopleReport
    {         
        private readonly ILineProcessor<Person> _lineProcessor;

        public ByLastNamePeopleReport(ILineProcessor<Person> lineProcessor)
        {
            _lineProcessor = lineProcessor;
        }
        public string GetReport(IList<Person> people, char delimeter)
        {
            var output = new StringBuilder();
            var list = people.OrderByDescending(s => s.LastName);
            foreach (var person in list)
            {
                output.AppendLine(_lineProcessor.ToLine(person, delimeter));
            }
            return output.ToString();
        }
    }
}