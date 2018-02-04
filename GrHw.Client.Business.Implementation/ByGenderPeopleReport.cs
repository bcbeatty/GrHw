using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrHw.Client.Domain;
using GrHw.Framework;

namespace GrHw.Client.Business.Implementation
{
    public class ByGenderPeopleReport : IPeopleReport
    {
        

        private readonly ILineProcessor<Person> _lineProcessor;

        public ByGenderPeopleReport(ILineProcessor<Person> lineProcessor)
        {
            _lineProcessor = lineProcessor;
        }
        public string GetReport(IList<Person> people, char delimeter)
        {
            var output = new StringBuilder();
            var list = people.OrderBy(s => s.Gender).ThenBy(s => s.LastName);
            foreach (var person in list)
            {
                output.AppendLine(_lineProcessor.ToLine(person, delimeter));
            }
            return output.ToString();
        }

    }
}