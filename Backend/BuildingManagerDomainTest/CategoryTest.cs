using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void CategoryIdTest()
        {
            Guid categoryId = Guid.NewGuid();
            Category category = new Category { Id = categoryId };
            Assert.AreEqual(categoryId, category.Id);
        }

        [TestMethod]
        public void CategoryNameTest()
        {
            string name = "Category";
            Category category = new Category { Name = name };
            Assert.AreEqual(name, category.Name);
        }

        [TestMethod]
        public void CategoryParentTest()
        {
            Guid parentId = Guid.NewGuid();
            Category category = new Category { ParentId = parentId };
            Assert.AreEqual(parentId, category.ParentId);
        }
    }
}
