using GrHw.Server.Business.Impementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrHw.Person.Tests.Server.Business
{
    [TestClass]
    public class PersonLineProcessorTests
    {
        [TestMethod]
        public void PersonLineProcessor_ParseLine_Success()
        {
            var parser = new PersonLineProcessor(',');
            var str = "Beatty,Brian,M,Green,03/14/1970";
            var actual = parser.ParseLine(str);
          
            Assert.AreEqual("Beatty",actual.LastName,"LastName");
        }

        [TestMethod]
        public void PersonLineProcessor_Delimiter_SetInConstructor_Success()
        {
            var parser = new PersonLineProcessor('$');
            Assert.AreEqual('$', parser.Delimiter);
        }


    }
}