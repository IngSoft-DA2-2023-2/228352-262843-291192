using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class ImporterTest
    {
        [TestMethod]
        public void CreateImporterWithName_SetsNameProperty()
        {
            var expectedName = "TestImporter";

            var importer = new Importer { Name = expectedName };

            Assert.AreEqual(expectedName, importer.Name);
        }

    }
}
