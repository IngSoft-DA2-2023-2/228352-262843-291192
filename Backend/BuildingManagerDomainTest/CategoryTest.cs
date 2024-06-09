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
        public void CategoryParentIdTest()
        {
            Guid parentId = Guid.NewGuid();
            Category category = new Category { ParentId = parentId };
            Assert.AreEqual(parentId, category.ParentId);
        }

        [TestMethod]
        public void CategoryParentTest()
        {
            Guid categoryParent = Guid.NewGuid();
            Category parent = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category",
                ParentId = categoryParent,
                Parent = new Category()
                {
                    Id = categoryParent,
                    Name = "Parent",
                    ParentId = null,
                    Parent = null
                }
            };

            Category category = new Category { Parent = parent };
            Assert.AreEqual(parent, category.Parent);
        }

        [TestMethod]
        public void CategoryChildrenTest()
        {
            Guid categoryId = Guid.NewGuid();
            List<Category> children = [];
            Category category = new Category()
            {
                Name = "Category",
                Children = children,
                Id = categoryId,
                ParentId = null,
                Parent = null
            };
            children.Add(new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category1",
                ParentId = categoryId,
                Parent = category
            });

            Assert.AreEqual(children, category.Children);
        }
    }
}
