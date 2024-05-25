using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerDomainTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConstructionCompanyTest
    {
        [TestMethod]
        public void ConstructionCompanyIdTest()
        {
            Guid constructionCompanyId = Guid.NewGuid();
            ConstructionCompany constructionCompany = new() { Id = constructionCompanyId };
            Assert.AreEqual(constructionCompanyId, constructionCompany.Id);
        }

        [TestMethod]
        public void ConstructionCompanyNameTest()
        {
            string constructionCompanyName = "test name";
            ConstructionCompany constructionCompany = new() { Name = constructionCompanyName };
            Assert.AreEqual(constructionCompanyName, constructionCompany.Name);
        }
    }
}
