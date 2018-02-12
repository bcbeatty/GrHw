using System;
using System.Collections.Generic;
using GrHw.Server.Domain;
using GrHw.Server.Repository;

namespace GrHw.Server.Business.Impementation
{
    public class PersonFactory : IPersonFactory
    {
        private readonly IRepository<Person> _personRepository;
        private readonly Func<char, ILineProcessor<Person>> _lineProcessor;
        public PersonFactory(IRepository<Person> personRepository, Func<char, ILineProcessor<Person>> lineProcessor)
        {
            _personRepository = personRepository;
            _lineProcessor = lineProcessor;
        }

        public Person PostString(string inputLine)
        {
            var delim = ' ';
            if (inputLine.Contains("|"))
            {
                delim = '|';
            }
            else if (inputLine.Contains(","))
            {
                delim = ',';
            }
            return _personRepository.Save(_lineProcessor(delim).ParseLine(inputLine));
        }

        public List<Person> GetList()
        {
            return _personRepository.GetList();
        }
    }
}
