using System;
using System.Collections.Generic;
using System.Linq;
using GrHw.Client.Business;
using GrHw.Client.Business.Implementation;
using GrHw.Client.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GrHw.Person.Tests.Business
{
    [TestClass]
    public class PersonBatchProcessorTests
    {
        Mock<IFileIORepository> _fileIORepository;
        Mock<IPersonRepository> _personRepository;
        private Mock<ILineProcessor<Client.Domain.Person>> _personLineProcessor;
        private Mock<IPeopleReport>[] _reportsMock;
        [TestInitialize]
        public void TestInitialize()
        {
            _fileIORepository = new Mock<IFileIORepository>(MockBehavior.Strict);
            _personRepository = new Mock<IPersonRepository>(MockBehavior.Strict);
            _personLineProcessor = new Mock<ILineProcessor<Client.Domain.Person>>(MockBehavior.Strict);
            _reportsMock = new[]
            {
                new Mock<IPeopleReport>(MockBehavior.Strict),
                new Mock<IPeopleReport>(MockBehavior.Strict)
            };
        }

        private void VerifyAll()
        {
            _fileIORepository.VerifyAll();
            _personRepository.VerifyAll();
            _personLineProcessor.VerifyAll();
            foreach (var mock in _reportsMock)
            {
                mock.VerifyAll();
            }
        }

        private PersonBatchProcessor GetPersonBatchProcessor()
        {
            return new PersonBatchProcessor(_fileIORepository.Object, _personRepository.Object, _personLineProcessor.Object, _reportsMock.Select(o => o.Object).ToArray());
        }

        [TestMethod]
        public void BatchProcessor_Loads_Filename_Success()
        {
            var fileName = Guid.NewGuid().ToString("n");
            var expected = new[] { "a,b,c", "d,e,f" };
            _fileIORepository.Setup(m => m.ReadFile(It.Is<string>(s => s == fileName))).Returns(expected);

            var processor = GetPersonBatchProcessor();
            var actual = processor.Load(fileName);
            CollectionAssert.AreEqual(expected, actual);
            VerifyAll();
        }


        [TestMethod]
        public void BatchProcessor_Parse_Success()
        {
            var contents = new[] { "Beatty, Brian, M,Green, 3/14/70", "Schmore,Jolean,F,Purple,6/10/95" };
            var expected = new List<Client.Domain.Person>
            {
                new Client.Domain.Person
                {
                    LastName = "Beatty",
                    FirstName = "Brian",
                    DateOfBirth = " 3/14/70",
                    FavoriteColor = "Green",
                    Gender = "M"
                },
                new Client.Domain.Person
                {
                    LastName = "Schmore",
                    FirstName = "Jolean",
                    DateOfBirth = "6/10/95",
                    FavoriteColor = "Purple",
                    Gender = "F"
                }
            };

            _personLineProcessor.Setup(m => m.ParseLine(It.Is<string[]>(s => s[0] == "Beatty"))).Returns(expected[0]);
            _personLineProcessor.Setup(m => m.ParseLine(It.Is<string[]>(s => s[1] == "Jolean"))).Returns(expected[1]);
            var processor = GetPersonBatchProcessor();
            var actual = processor.Parse(contents, ',');
            CollectionAssert.AreEquivalent(expected, actual, "Array");
            VerifyAll();
        }

        [TestMethod]
        public void BatchProcessor_Save_Success()
        {
            var people = new List<Client.Domain.Person>
            {
                new Client.Domain.Person
                {
                    LastName = "Beatty",
                    FirstName = "Brian",
                    DateOfBirth = " 3/14/70",
                    FavoriteColor = "Green",
                    Gender = "M"
                },
                new Client.Domain.Person
                {
                    LastName = "Schmore",
                    FirstName = "Jolean",
                    DateOfBirth = "6/10/95",
                    FavoriteColor = "Purple",
                    Gender = "F"
                }
            };

            _personRepository.Setup(m => m.Insert(It.Is<Client.Domain.Person>(s => s.FirstName == "Brian"))).Returns(people[0]);
            _personRepository.Setup(m => m.Insert(It.Is<Client.Domain.Person>(s => s.Gender == "F"))).Returns(people[0]);

            var processor = GetPersonBatchProcessor();
            processor.Save(people);

            VerifyAll();
        }

        [TestMethod]
        public void BatchProcessor_GetReports_Success()
        {
            var people = new List<Client.Domain.Person>
            {
                new Client.Domain.Person
                {
                    LastName = "Beatty",
                    FirstName = "Brian",
                    DateOfBirth = " 3/14/70",
                    FavoriteColor = "Green",
                    Gender = "M"
                },
                new Client.Domain.Person
                {
                    LastName = "Schmore",
                    FirstName = "Jolean",
                    DateOfBirth = "6/10/95",
                    FavoriteColor = "Purple",
                    Gender = "F"
                }
            };
            var report1 = "Beatty, Brian, M,Green, 3/14/70\nSchmore,Jolean,F,Purple,6/10/95\n";
            var report2 = "Schmore,Jolean,F,Purple,6/10/95\nBeatty, Brian, M,Green, 3/14/70\n";
            _reportsMock[0].Setup(m => m.GetReport(It.IsAny<IList<Client.Domain.Person>>(), It.Is<char>(s => s == ','))).Returns(report1);
            _reportsMock[1].Setup(m => m.GetReport(It.IsAny<IList<Client.Domain.Person>>(), It.Is<char>(s => s == ','))).Returns(report2);
            var expected = new List<string>
            {
                report1,
                report2
            };
            var processor = GetPersonBatchProcessor();
            var actual = processor.GetReports(people, ',');
            CollectionAssert.AreEqual(expected, actual);
            VerifyAll();
        }
    }
}
