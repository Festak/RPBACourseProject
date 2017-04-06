using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SellTables;
using SellTables.Interfaces;
using System.Collections.Generic;
using SellTables.Controllers;
using SellTables.Models;
using SellTables.Services;
using System.Web.Mvc;


namespace SellTables.Tests
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            var mock = new Mock<IUserService>();
            AdminController controller = new AdminController(mock.Object, null);

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var mock = new Mock<IUserService>();
            AdminController controller = new AdminController(mock.Object, null);

            controller.DeleteUser("asd");

            mock.Verify(m => m.DeleteUser(It.IsNotNull<string>()));
        }

        [TestMethod]
        public void BanUserTest()
        {
            var mock = new Mock<IUserService>();
            AdminController controller = new AdminController(mock.Object, null);

            controller.BanUser("asd");

            mock.Verify(m => m.BanUser(It.IsNotNull<string>()));
        }

        [TestMethod]
        public void UnbanUserTest()
        {
            var mock = new Mock<IUserService>();
            AdminController controller = new AdminController(mock.Object, null);

            controller.UnbanUser("asd");

            mock.Verify(m => m.UnbanUser(It.IsNotNull<string>()));
        }
    }
}
