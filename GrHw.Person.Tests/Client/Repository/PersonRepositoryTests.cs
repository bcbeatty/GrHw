using System;
using GwHw.Client.Repository.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrHw.Person.Tests.Client.Repository
{
    [TestClass]
    public class PersonRepositoryTests
    {
        [TestMethod]
        public void GetByKey_ReturnsNull_Success()
        {
            var repo = new PersonRepository();
            Assert.IsNull(repo.GetByKey(DateTime.Now.Second));
        }

        [TestMethod]
        public void Insert_Returns_Item_Success()
        {
            var repo = new PersonRepository();
            var actual = repo.Insert(new GrHw.Client.Domain.Person());
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Update_Returns_Item_Success()
        {
            var repo = new PersonRepository();
            var actual = repo.Update(new GrHw.Client.Domain.Person());
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Delete_Success()
        {
            var repo = new PersonRepository();
            repo.Delete(new GrHw.Client.Domain.Person());
            
        }
    }
}