using System.Collections.Generic;
using GrHw.Client.Business;
using GrHw.Client.Business.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GrHw.Person.Tests.Business
{
    [TestClass]
    public class ByGenderPeopleReportTests
    {
        private Mock<ILineProcessor<Client.Domain.Person>> _lineProcessor;


        [TestInitialize]
        public void TestInitialize()
        {
            _lineProcessor = new Mock<ILineProcessor<Client.Domain.Person>>(MockBehavior.Strict);
        }

        private void VerifyAll()
        {
            _lineProcessor.VerifyAll();
        }

        private ByGenderPeopleReport GetByGenderPeopleReport()
        {
            return new ByGenderPeopleReport(_lineProcessor.Object);
        }

        [TestMethod]
        public void ByDateOfBirthPeopleReport_GetReport_Success()
        {
            var people = new List<Client.Domain.Person>
            {
                new Client.Domain.Person
                {
                    LastName = "Beatty",
                    FirstName = "Brian",
                    Gender = "M",
                    DateOfBirth = "3/14/70",
                    FavoriteColor = "Green"
                },
                new Client.Domain.Person
                {
                    LastName = "Smith",
                    FirstName = "Sally",
                    Gender = "F",
                    DateOfBirth = "3/14/2010",
                    FavoriteColor = "Purple"
                }
            };
            var expected = "Smith,Sally,F,Purple,3/14/2010\n\r\nBeatty,Brian,M,Green,3/13/1970\n\r\n";
            _lineProcessor.Setup(m => m.ToLine(It.Is<Client.Domain.Person>(s => s.LastName == people[1].LastName), It.IsAny<char>())).Returns("Smith,Sally,F,Purple,3/14/2010\n");
            _lineProcessor.Setup(m => m.ToLine(It.Is<Client.Domain.Person>(s => s.LastName == people[0].LastName), It.IsAny<char>())).Returns("Beatty,Brian,M,Green,3/13/1970\n");
             var report = GetByGenderPeopleReport();
            var actual = report.GetReport(people, ',');
            Assert.AreEqual(expected, actual);
            VerifyAll();
        }


    }
}