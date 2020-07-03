using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneStore.WebUI.Controllers;
using PhoneStore.WebUI.Infrastructure.Abstract;
using PhoneStore.WebUI.Models;
using System.Web.Mvc;

namespace PhoneStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "123")).Returns(true);

            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "123"
            };

            AccountController target = new AccountController(mock.Object);

            //Act
            ActionResult result = target.Login(model, "/MyURL");

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            LoginViewModel model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };

            AccountController target = new AccountController(mock.Object);

            //Act
            ActionResult result = target.Login(model, "/MyURL");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
