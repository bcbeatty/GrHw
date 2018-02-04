using System;
using System.Collections.Generic;
using System.Linq;
using GrHw.Client.Domain;
using GrHw.Client.Repository;
using GrHw.Framework;

namespace GrHw.Client.Business.Implementation
{
    public class PersonBatchProcessor : IBatchProcessor<Person>
    {
        private readonly IFileIORepository _fileIORepository;
        private readonly IPersonRepository _personRepository;
        private readonly ILineProcessor<Person> _personLineProcessor;
        private readonly IPeopleReport[] _reports;
        public PersonBatchProcessor(IFileIORepository fileIORepository, IPersonRepository personRepository, ILineProcessor<Person> personLineProcessor, IPeopleReport[] reports)
        {
            _fileIORepository = fileIORepository;

            _personRepository = personRepository;
            _personLineProcessor = personLineProcessor;
            _reports = reports;
        }


        public string[] Load(string filename)
        {
            return _fileIORepository.ReadFile(filename);
        }

        public List<Person> Parse(string[] contents, char delimeter)
        {
            return contents.Select(line =>
              line.Split(delimeter)).Select(item => _personLineProcessor.ParseLine(item)).ToList();
        }

        

        public void Save(List<Person> people)
        {
            foreach (var person in people)
            {
                _personRepository.Insert(person);
            }

        }

        public List<string> GetReports(List<Person> people, char delimeter)
        {
            return _reports.Select(report => report.GetReport(people, delimeter)).ToList();
        }
    }
}
