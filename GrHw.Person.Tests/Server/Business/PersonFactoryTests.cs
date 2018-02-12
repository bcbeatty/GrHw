using System;
using GrHw.Server.Business;
using GrHw.Server.Business.Impementation;
using GrHw.Server.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GrHw.Person.Tests.Server.Business
{
    [TestClass]
    public class PersonFactoryTests
    {

        private Mock<IRepository<GrHw.Server.Domain.Person>> _personRepository;
        private Mock<Func<char, ILineProcessor<GrHw.Server.Domain.Person>>> _lineProcessorFunc;
        private Mock<ILineProcessor<GrHw.Server.Domain.Person>> _lineProcessor;
        [TestInitialize]
        public void TestInitialize()
        {
            _lineProcessorFunc = new Mock<Func<char, ILineProcessor<GrHw.Server.Domain.Person>>>(MockBehavior.Strict);
            _personRepository = new Mock<IRepository<GrHw.Server.Domain.Person>>(MockBehavior.Strict);
            _lineProcessor = new Mock<ILineProcessor<GrHw.Server.Domain.Person>>(MockBehavior.Strict);
        }


        private void VerifyAll()
        {
            _lineProcessorFunc.VerifyAll();
            _personRepository.VerifyAll();
            _lineProcessor.VerifyAll();
        }

        private IPersonFactory GetPersonFactory()
        {
            return new PersonFactory(_personRepository.Object, _lineProcessorFunc.Object);
        }


        [TestMethod]
        public void PersonFactory_PostString_ParsesPipe_Success()
        {
            var expected = new GrHw.Server.Domain.Person
            {
                LastName = "Beatty",
                FirstName = "Brian",
                Gender = "M",
                FavoriteColor = "Green",
                DateOfBirth = new DateTime(1970, 3, 14)
            };
            var str = "Beatty|Brian|M|Green|03/14/1970";
            _lineProcessor.Setup(m => m.ParseLine(It.Is<string>(s => s == str))).Returns(expected);
            _lineProcessorFunc.Setup(m => m(It.Is<char>(s => s == '|'))).Returns(() => _lineProcessor.Object);
            _personRepository.Setup(m => m.Save(It.Is<GrHw.Server.Domain.Person>(s => s.LastName == expected.LastName))).Returns(expected);
            var factor = GetPersonFactory();
            var actual = factor.PostString(str);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.LastName, actual.LastName, "LastName");
            Assert.AreEqual(expected.FirstName, actual.FirstName, "FirstName");
            Assert.AreEqual(expected.Gender, actual.Gender, "Gender");
            Assert.AreEqual(expected.DateOfBirth, actual.DateOfBirth, "DateOfBirth");
            Assert.AreEqual(expected.FavoriteColor, actual.FavoriteColor, "FavoriteColor");
            VerifyAll();

        }

        [TestMethod]
        public void PersonFactory_PostString_ParsesComma_Success()
        {
            var expected = new GrHw.Server.Domain.Person
            {
                LastName = "Beatty",
                FirstName = "Brian",
                Gender = "M",
                FavoriteColor = "Green",
                DateOfBirth = new DateTime(1970, 3, 14)
            };
            var str = "Beatty,Brian,M,Green,03/14/1970";
            _lineProcessor.Setup(m => m.ParseLine(It.Is<string>(s => s == str))).Returns(expected);
            _lineProcessorFunc.Setup(m => m(It.Is<char>(s => s == ','))).Returns(() => _lineProcessor.Object);
            _personRepository.Setup(m => m.Save(It.Is<GrHw.Server.Domain.Person>(s => s.LastName == expected.LastName))).Returns(expected);
            var factor = GetPersonFactory();
            var actual = factor.PostString(str);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.LastName, actual.LastName, "LastName");
            Assert.AreEqual(expected.FirstName, actual.FirstName, "FirstName");
            Assert.AreEqual(expected.Gender, actual.Gender, "Gender");
            Assert.AreEqual(expected.DateOfBirth, actual.DateOfBirth, "DateOfBirth");
            Assert.AreEqual(expected.FavoriteColor, actual.FavoriteColor, "FavoriteColor");
            VerifyAll();

        }

        [TestMethod]
        public void PersonFactory_PostString_ParsesSpace_Success()
        {
            var expected = new GrHw.Server.Domain.Person
            {
                LastName = "Beatty",
                FirstName = "Brian",
                Gender = "M",
                FavoriteColor = "Green",
                DateOfBirth = new DateTime(1970, 3, 14)
            };
            var str = "Beatty Brian M Green 03/14/1970";
            _lineProcessor.Setup(m => m.ParseLine(It.Is<string>(s => s == str))).Returns(expected);
            _lineProcessorFunc.Setup(m => m(It.Is<char>(s => s == ' '))).Returns(() => _lineProcessor.Object);
            _personRepository.Setup(m => m.Save(It.Is<GrHw.Server.Domain.Person>(s => s.LastName == expected.LastName))).Returns(expected);
            var factor = GetPersonFactory();
            var actual = factor.PostString(str);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.LastName, actual.LastName, "LastName");
            Assert.AreEqual(expected.FirstName, actual.FirstName, "FirstName");
            Assert.AreEqual(expected.Gender, actual.Gender, "Gender");
            Assert.AreEqual(expected.DateOfBirth, actual.DateOfBirth, "DateOfBirth");
            Assert.AreEqual(expected.FavoriteColor, actual.FavoriteColor, "FavoriteColor");
            VerifyAll();

        }
    }
}