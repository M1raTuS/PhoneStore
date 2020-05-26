using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneStore.Domain.Abstract;
using PhoneStore.Domain.Entities;
using PhoneStore.WebUI.Controllers;

namespace PhoneStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
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
            PhoneController controller = new PhoneController(mock.Object);
            controller.pageSize = 3;

            //Act
            IEnumerable<Phone> result = (IEnumerable<Phone>)controller.List(2).Model;

            //Assert
            List<Phone> phones = result.ToList();
            Assert.IsTrue(phones.Count == 2);
            Assert.AreEqual(phones[0].Name, "Phone4");
            Assert.AreEqual(phones[1].Name, "Phone5");
        }
    }
}





