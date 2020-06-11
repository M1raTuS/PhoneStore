using PhoneStore.Domain.Abstract;
using PhoneStore.Domain.Entities;
using PhoneStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace PhoneStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPhoneRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IPhoneRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToRouteResult AddToCart(Cart cart, int phoneId, string returnUrl)
        {
            Phone phone = repository.Phones
                .FirstOrDefault(g => g.PhoneId == phoneId);

            if (phone != null)
            {
                cart.AddItem(phone, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int phoneId, string returnUrl)
        {
            Phone phone = repository.Phones
                .FirstOrDefault(g => g.PhoneId == phoneId);

            if (phone != null)
            {
                cart.RemoveLine(phone);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }


    }
}