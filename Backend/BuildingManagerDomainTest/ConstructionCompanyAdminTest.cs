using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using System.Diagnostics.CodeAnalysis;
using BuildingManagerDomain.Enums;

namespace BuildingManagerDomainTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConstructionCompanyAdminTest
    {
        [TestMethod]
        public void ConstructionCompanyAdminIdTest()
        {
            Guid constructionCompanyAdminId = Guid.NewGuid();
            ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin { Id = constructionCompanyAdminId };
            Assert.AreEqual(constructionCompanyAdminId, constructionCompanyAdmin.Id);
        }

        [TestMethod]
        public void ConstructionCompanyAdminNameTest()
        {
            string constructionCompanyAdminName = "John";
            ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin { Name = constructionCompanyAdminName };
            Assert.AreEqual(constructionCompanyAdminName, constructionCompanyAdmin.Name);
        }
    }
}