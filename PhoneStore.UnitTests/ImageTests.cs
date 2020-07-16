using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneStore.Domain.Entities;
using Moq;
using PhoneStore.Domain.Abstract;
using System.Linq;
using PhoneStore.WebUI.Controllers;
using System.Web.Mvc;

namespace PhoneStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            //Arrange
            Phone phone = new Phone
            {
                PhoneId = 2,
                Name = "Phone2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone> {
                new Phone {PhoneId = 1, Name = "Phone1"},
                phone,
                new Phone {PhoneId = 3, Name = "Phone3"}
            }.AsQueryable());

            PhoneController controller = new PhoneController(mock.Object);

            //Act
            ActionResult result = controller.GetImage(2);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(phone.ImageMimeType, ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone> {
                new Phone {PhoneId = 1, Name = "Phone1"},
                new Phone {PhoneId = 3, Name = "Phone3"}
            }.AsQueryable());

            PhoneController controller = new PhoneController(mock.Object);

            //Act
            ActionResult result = controller.GetImage(10);

            //Assert
            Assert.IsNull(result);
        }
    }
}
