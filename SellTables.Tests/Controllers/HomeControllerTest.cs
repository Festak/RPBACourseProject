using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SellTables.Interfaces;
using System.Collections.Generic;
using SellTables.Controllers;
using SellTables.Models;
using SellTables.Services;
using SellTables.ViewModels;
using System.Web.Mvc;

namespace SellTables.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestGetUsersMethod()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(a => a.GetAllUsers()).Returns(new List<ApplicationUser>());
            HomeController controller = new HomeController(null, mock.Object, null);

            var result = controller.GetUsers();
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetCreativesMethod()
        {
            var mock = new Mock<ICreativeService>();
            mock.Setup(a => a.GetAllCreatives()).Returns(new List<CreativeViewModel>());
            HomeController controller = new HomeController(mock.Object, null, null);

            var result = controller.GetCreatives();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetCreativesRangeMethod()
        {
            var mock = new Mock<ICreativeService>();
            mock.Setup(a => a.GetCreativesRange(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<CreativeViewModel>());

            HomeController controller = new HomeController(mock.Object, null, null);

            var result = controller.GetCreativesRange(0, 0, 0);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetTagsMethod()
        {
            var mock = new Mock<ITagService>();
            mock.Setup(a => a.GetAllModelTags()).Returns(new List<TagViewModel>());
            HomeController controller = new HomeController(null, null, mock.Object);

            var result = controller.GetTags();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetPopularMethod()
        {
            var mock = new Mock<ICreativeService>();
            mock.Setup(a => a.GetPopularCreatives()).Returns(new List<CreativeViewModel>());
            HomeController controller = new HomeController(mock.Object, null, null);

            var result = controller.GetPopular();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetLastEditedMethod()
        {
            var mock = new Mock<ICreativeService>();
            mock.Setup(a => a.GetLastEditedCreatives()).Returns(new List<CreativeViewModel>());
            HomeController controller = new HomeController(mock.Object, null, null);

            var result = controller.GetLastEdited();

            Assert.IsNotNull(result);
        }
    }
}