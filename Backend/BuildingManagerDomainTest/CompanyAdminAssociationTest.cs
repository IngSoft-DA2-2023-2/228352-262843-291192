using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class CompanyAdminAssociationTest
    {
        [TestMethod]
        public void ConstructionCompanyAdminIdTest()
        {
            Guid constructionCompanyAdminId = Guid.NewGuid();
            CompanyAdminAssociation association = new CompanyAdminAssociation { ConstructionCompanyAdminId = constructionCompanyAdminId };
            Assert.AreEqual(constructionCompanyAdminId, association.ConstructionCompanyAdminId);
        }
    }
}
