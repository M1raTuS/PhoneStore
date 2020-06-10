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
            PhonesListViewModel result = (PhonesListViewModel)controller.List(null, 2).Model;

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
            PhonesListViewModel result = (PhonesListViewModel)controller.List(null, 2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Phones()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone>
            {
                new Phone{PhoneId = 1, Name = "Phone1", Category = "Cat1"},
                new Phone{PhoneId = 2, Name = "Phone2", Category = "Cat1"},
                new Phone{PhoneId = 3, Name = "Phone3", Category = "Cat2"},
                new Phone{PhoneId = 4, Name = "Phone4", Category = "Cat1"},
                new Phone{PhoneId = 5, Name = "Phone5", Category = "Cat3"},
                new Phone{PhoneId = 6, Name = "Phone6", Category = "Cat2"}
            });
            PhoneController controller = new PhoneController(mock.Object);
            controller.pageSize = 3;

            //Act
            List<Phone> result = ((PhonesListViewModel)controller.List("Cat1", 1).Model)
                .Phones.ToList();

            //Assert
            Assert.AreEqual(result.Count(), 3);
            Assert.IsTrue(result[1].Name == "Phone2" && result[0].Category == "Cat1");
            Assert.IsTrue(result[2].Name == "Phone4" && result[1].Category == "Cat1");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone>
            {
                new Phone { PhoneId = 1, Name = "Phone1", Category = "Samsung"},
                new Phone { PhoneId = 2, Name = "Phone2", Category = "Samsung"},
                new Phone { PhoneId = 3, Name = "Phone3", Category = "Apple"},
                new Phone { PhoneId = 4, Name = "Phone4", Category = "Meizu"},
                new Phone { PhoneId = 5, Name = "Phone5", Category = "Xiaomi"},
            });
            NavController target = new NavController(mock.Object);

            //Act
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            //Assert
            Assert.AreEqual(results.Count(), 4);
            Assert.AreEqual(results[0], "Apple");
            Assert.AreEqual(results[1], "Meizu");
            Assert.AreEqual(results[2], "Samsung");
            Assert.AreEqual(results[3], "Xiaomi");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new Phone[]
            {
                new Phone { PhoneId = 1, Name = "Phone1", Category = "Apple"},
                new Phone { PhoneId = 2, Name = "Phone2", Category = "Asus"}
            });
            NavController target = new NavController(mock.Object);
            string categoryToSelect = "Asus";

            //Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Phone_Count()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone>
            {
            new Phone { PhoneId = 1, Name = "Phone1", Category="Cat1"},
            new Phone { PhoneId = 2, Name = "Phone2", Category="Cat2"},
            new Phone { PhoneId = 3, Name = "Phone3", Category="Cat1"},
            new Phone { PhoneId = 4, Name = "Phone4", Category="Cat2"},
            new Phone { PhoneId = 5, Name = "Phone5", Category="Cat3"}
            });
            PhoneController controller = new PhoneController(mock.Object);
            controller.pageSize = 3;

            //Act
            int res1 = ((PhonesListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((PhonesListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((PhonesListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((PhonesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //Arrange
            Phone phone1 = new Phone { PhoneId = 1, Name = "Phone1" };
            Phone phone2 = new Phone { PhoneId = 2, Name = "Phone2" };
            Cart cart = new Cart();

            //Act
            cart.AddItem(phone1, 1);
            cart.AddItem(phone2, 1);
            List<CartLine> results = cart.Lines.ToList();

            //Assert
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Phone, phone1);
            Assert.AreEqual(results[1].Phone, phone2);
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arrange
            Phone phone1 = new Phone { PhoneId = 1, Name = "Phone1" };
            Phone phone2 = new Phone { PhoneId = 2, Name = "Phone2" };
            Cart cart = new Cart();

            //Act
            cart.AddItem(phone1, 1);
            cart.AddItem(phone2, 1);
            cart.AddItem(phone1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Phone.PhoneId).ToList();

            //Assert
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            //Arrange
            Phone phone1 = new Phone { PhoneId = 1, Name = "Phone1" };
            Phone phone2 = new Phone { PhoneId = 2, Name = "Phone2" };
            Phone phone3 = new Phone { PhoneId = 3, Name = "Phone3" };

            Cart cart = new Cart();
            cart.AddItem(phone1, 1);
            cart.AddItem(phone2, 4);
            cart.AddItem(phone3, 2);
            cart.AddItem(phone2, 1);

            //Act
            cart.RemoveLine(phone2);

            //Assert
            Assert.AreEqual(cart.Lines.Where(c => c.Phone == phone2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //Arrange
            Phone phone1 = new Phone { PhoneId = 1, Name = "Phone1", Price = 100 };
            Phone phone2 = new Phone { PhoneId = 2, Name = "Phone2", Price = 55 };
            Cart cart = new Cart();

            //Act
            cart.AddItem(phone1, 1);
            cart.AddItem(phone2, 1);
            cart.AddItem(phone1, 5);
            decimal result = cart.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result, 655);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Arrange
            Phone phone1 = new Phone { PhoneId = 1, Name = "Phone1", Price = 100 };
            Phone phone2 = new Phone { PhoneId = 2, Name = "Phone2", Price = 55 };
            Cart cart = new Cart();

            //Act
            cart.AddItem(phone1, 1);
            cart.AddItem(phone2, 1);
            cart.AddItem(phone1, 5);
            cart.Clear();

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone> {
            new Phone { PhoneId = 1, Name = "Phone1", Category="Cat1"},
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object);

            //Act
            controller.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Phone.PhoneId, 1);
     
        }
        [TestMethod]
        public void Adding_Phone_To_Cart_Goes_To_Cart_Screen()
        {
            //Arrange
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone> {
            new Phone { PhoneId = 1, Name = "Phone1", Category="Cat1"},
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object);

            //Act
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            //Arrange
            Cart cart = new Cart();

            CartController target = new CartController(null);

            //Act
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            //Assert
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}





