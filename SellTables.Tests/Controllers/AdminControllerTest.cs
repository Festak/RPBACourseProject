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
            // Arrange
            var mock = new Mock<IUserService>();
            mock.Setup(a => a.DeleteUser("User"));
            AdminController controller = new AdminController(mock.Object, null);

            // Atc
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }




    }
}
