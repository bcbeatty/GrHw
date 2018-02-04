using System.Collections.Generic;
using GrHw.Client.Business.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GrHw.Person.Tests.Business
{
    [TestClass]
    public class PersonLineProcessorTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        private void VerifyAll()
        {
        }

        private PersonLineProcessor GetPersonLineProcessor()
        {
            return new PersonLineProcessor();
        }

        [TestMethod]
        public void PersonLineProcessor_ParseLine_Success()
        {
            var contents = new[] { "Beatty", "Brian", "M", "Green", "3/14/70" };
            var expected = new Client.Domain.Person
            {
                LastName = "Beatty",
                FirstName = "Brian",
                DateOfBirth = "3/14/1970",
                FavoriteColor = "Green",
                Gender = "M"
            };
            var processor = GetPersonLineProcessor();
            var actual = processor.ParseLine(contents);
            Assert.AreEqual(expected.LastName, actual.LastName, "LastName");
            Assert.AreEqual(expected.FirstName, actual.FirstName, "FirstName");
            Assert.AreEqual(expected.DateOfBirth, actual.DateOfBirth, "DateOfBirth");
            Assert.AreEqual(expected.FavoriteColor, actual.FavoriteColor, "FavoriteColor");
            Assert.AreEqual(expected.Gender, actual.Gender, "Gender");
            VerifyAll();
        }

        [TestMethod]
        public void ByLastNamePeopleReport_GetReport_Success()
        {
            var person =
                new Client.Domain.Person
                {
                    LastName = "Beatty",
                    FirstName = "Brian",
                    Gender = "M",
                    DateOfBirth = "3/14/90",
                    FavoriteColor = "Green"

                };
            var expected = "Beatty,Brian,M,Green,3/14/1990";
             var report = GetPersonLineProcessor();
            var actual = report.ToLine(person, ',');
            Assert.AreEqual(expected, actual);
            VerifyAll();
        }


    }
}