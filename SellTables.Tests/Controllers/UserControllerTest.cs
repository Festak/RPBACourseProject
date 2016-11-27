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
    public class UserControllerTest
    {
        [TestMethod]
        public void GetCreativesByUserTest()
        {
            // Arrange
            var mock = new Mock<ICreativeService>();
            mock.Setup(a => a.GetCreativesByUser("admin@admin.com")).Returns(new List<CreativeViewModel>());
            UserController controller = new UserController(mock.Object, null, null);

            // Act
            var result = controller.GetCreativesByUser("admin@admin.com");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserAvatarUriTest()
        {
            // Arrange
            var mock = new Mock<IUserService>();
            mock.Setup(a => a.GetUserAvatarUri(It.IsAny<string>()))
        .Returns((string s) => s.ToLower()); ;
            UserController controller = new UserController(null, mock.Object, null);

            // Act
            var result = controller.GetUserAvatarUri("123");

            // Assert
            Assert.IsNotNull(result);
        }



    }
}
