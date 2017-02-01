using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SellTables.Repositories;
using System.Collections.Generic;
using SellTables.Models;
using SellTables.Interfaces;
using SellTables.Services;
using SellTables.ViewModels;

namespace SellTables.Tests
{
    [TestClass]
    public class TagServiceTest
    {
        [TestMethod]
        public void GetAllTagsTest()
        {
            var mock = new Mock<IRepository<Tag>>();
            mock.Setup(a => a.GetAll()).Returns(new List<Tag>() as ICollection<Tag>);
            TagService service = new TagService(mock.Object);

            var result = service.GetAllTags();
            Assert.IsInstanceOfType(result, typeof(List<Tag>));
        }

        [TestMethod]
        public void GetAllModelTagsTest()
        {
            var mock = new Mock<IRepository<Tag>>();
            mock.Setup(a => a.GetAll()).Returns(new List<Tag>() as ICollection<Tag>);
            TagService service = new TagService(mock.Object);

            var result = service.GetAllModelTags();
            Assert.IsInstanceOfType(result, typeof(List<TagViewModel>));
        }
    }
}
