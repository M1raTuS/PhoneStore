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

        [TestMethod]
        public void Can_Edit_Phone()
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
            Phone phone1 = controller.Edit(1).ViewData.Model as Phone;
            Phone phone2 = controller.Edit(2).ViewData.Model as Phone;
            Phone phone3 = controller.Edit(3).ViewData.Model as Phone;

            //Assert
            Assert.AreEqual(1, phone1.PhoneId);
            Assert.AreEqual(2, phone2.PhoneId);
            Assert.AreEqual(3, phone3.PhoneId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Phone()
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
            Phone phone1 = controller.Edit(6).ViewData.Model as Phone;

            //Assert
        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            AdminController controller = new AdminController(mock.Object);
            Phone phone = new Phone { Name = "Test" };

            //Act
            ActionResult result = controller.Edit(phone);

            //Assert
            mock.Verify(m => m.SavePhone(phone));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            AdminController controller = new AdminController(mock.Object);
            Phone phone = new Phone { Name = "Test" };
            controller.ModelState.AddModelError("error","error");

            //Act
            ActionResult result = controller.Edit(phone);

            //Assert
            mock.Verify(m => m.SavePhone(It.IsAny<Phone>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Can_Delete_Valid_Phones()
        {
            //Arrange
            Phone phone = new Phone { PhoneId = 2, Name = "Phone2" };
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
            controller.Delete(phone.PhoneId);

            //Assert
            mock.Verify(m => m.DeletePhone(phone.PhoneId));
        }
    }
}
