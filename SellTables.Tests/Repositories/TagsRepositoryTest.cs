using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SellTables;
using SellTables.Interfaces;
using System.Collections.Generic;
using SellTables.Controllers;
using SellTables.Models;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SellTables.Repositories;
using System.Linq;

namespace SellTables.Tests
{
    [TestClass]
    public class TagsRepositoryTest
    {
        [TestMethod]
        public void TagAddRemoveTest()
        {
            var mockSet = new Mock<DbSet<Tag>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Tags).Returns(mockSet.Object);

            IRepository<Tag> tagsRepository = new TagsRepository(mockContext.Object);
            Tag tag = new Tag() { Id = 1 };
            tagsRepository.Add(tag);

            mockSet.Verify(m => m.Add(It.IsAny<Tag>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            tagsRepository.Remove(tag.Id);
            mockSet.Verify(m => m.Remove(It.IsAny<Tag>()), Times.Never);
        }

        [TestMethod]
        public void MethodGetTest()
        {
            var mockSet = new Mock<DbSet<Tag>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Tags).Returns(mockSet.Object);

            IRepository<Tag> tagsRepository = new TagsRepository(mockContext.Object);
      

            tagsRepository.Get(1);
            mockSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once);
          
        }

        [TestMethod]
        public void MethodGetAllTest()
        {
            var mockSet = new Mock<DbSet<Tag>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Tags).Returns(mockSet.Object);
            mockSet.As<IEnumerable<Tag>>().Setup(m => m.GetEnumerator()).Returns((new List<Tag>()).GetEnumerator());

            IRepository<Tag> tagsRepository = new TagsRepository(mockContext.Object);
            var tags = tagsRepository.GetAll();

            Assert.IsNotNull(tags);

        }


    }
}
