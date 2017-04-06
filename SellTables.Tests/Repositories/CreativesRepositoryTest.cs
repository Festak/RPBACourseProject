using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SellTables.Models;
using System.Data.Entity;
using Moq;
using SellTables.Interfaces;
using SellTables.Repositories;

namespace SellTables.Tests.Repositories
{
    [TestClass]
    public class CreativesRepositoryTest
    {

        [TestMethod]
        public void CreativeAddRemoveTest()
        {
            var mockSet = new Mock<DbSet<Creative>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Creatives).Returns(mockSet.Object);

            IRepository<Creative> creativeRepository = new CreativesRepository(mockContext.Object);
            Creative creative = new Creative() { Id = 1};
            creativeRepository.Add(creative);

            mockSet.Verify(m => m.Add(It.IsAny<Creative>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            creativeRepository.Remove(creative.Id);
            mockSet.Verify(m => m.Remove(It.IsAny<Creative>()), Times.Never);
        }

    }
}
