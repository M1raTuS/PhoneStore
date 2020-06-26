using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneStore.Domain.Abstract;
using PhoneStore.Domain.Entities;
using PhoneStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PhoneStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Phones()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone>
            {
                new Phone { PhoneId = 1, Name = "Phone1"},
                new Phone { PhoneId = 2, Name = "Phone2"},
                new Phone { PhoneId = 3, Name = "Phone3"},
                new Phone { PhoneId = 4, Name = "Phone4"},
                new Phone { PhoneId = 5, Name = "Phone5"}
            });

            AdminController controller = new AdminController(mock.Object);

            //Act
            List<Phone> result = ((IEnumerable<Phone>)controller.Index().ViewData.Model).ToList();

            //Assert
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Phone1", result[0].Name);
            Assert.AreEqual("Phone2", result[1].Name);
            Assert.AreEqual("Phone3", result[2].Name);
        }
    }
}
