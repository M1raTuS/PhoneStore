using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhoneStore.Domain.Abstract;
using PhoneStore.Domain.Entities;
using PhoneStore.WebUI.Controllers;
using PhoneStore.WebUI.HtmlHelpers;
using PhoneStore.WebUI.Models;

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
            PhonesListViewModel result = (PhonesListViewModel)controller.List(2).Model;

            //Assert
            List<Phone> phones = result.Phones.ToList();
            Assert.IsTrue(phones.Count == 2);
            Assert.AreEqual(phones[0].Name, "Phone4");
            Assert.AreEqual(phones[1].Name, "Phone5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10,
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone>
            {
                new Phone {PhoneId = 1, Name = "Phone1"},
                new Phone {PhoneId = 2, Name = "Phone2"},
                new Phone {PhoneId = 3, Name = "Phone3"},
                new Phone {PhoneId = 4, Name = "Phone4"},
                new Phone {PhoneId = 5, Name = "Phone5"}
            });
            PhoneController controller = new PhoneController(mock.Object);
            controller.pageSize = 3;

            //Act
            PhonesListViewModel result = (PhonesListViewModel)controller.List(2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}





