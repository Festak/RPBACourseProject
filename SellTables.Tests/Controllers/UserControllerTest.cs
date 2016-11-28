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
            var mock = new Mock<ICreativeService>();
            mock.Setup(a => a.GetCreativesByUser("admin@admin.com")).Returns(new List<CreativeViewModel>());
            UserController controller = new UserController(mock.Object, null, null);

            var result = controller.GetCreativesByUser("admin@admin.com");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserAvatarUriTest()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(a => a.GetUserAvatarUri(It.IsAny<string>()))
            .Returns((string s) => s.ToLower()); ;
            UserController controller = new UserController(null, mock.Object, null);

            var result = controller.GetUserAvatarUri("123");
            
            Assert.IsNotNull(result);
        }



    }
}
