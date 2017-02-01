using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SellTables;
using SellTables.Interfaces;
using System.Collections.Generic;
using SellTables.Controllers;
using SellTables.Models;
using SellTables.Services;

namespace SellTables.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodTest()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(a => a.GetAllUsers()).Returns(new List<ApplicationUser>());
            HomeController controller = new HomeController(null, mock.Object, null);

            // Act
            var result = controller.GetUsers();

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
