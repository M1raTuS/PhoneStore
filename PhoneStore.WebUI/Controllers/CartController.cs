using System.Linq;
using System.Web.Mvc;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Abstract;
using PhoneStore.WebUI.Models;

namespace PhoneStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        private IPhoneRepository repository;
        public CartController(IPhoneRepository repo)
        {
            repository = repo;
        }

        public RedirectToRouteResult AddToCart(int phoneId, string returnUrl)
        {
            Phone phone = repository.Phones
                .FirstOrDefault(g => g.PhoneId == phoneId);

            if (phone != null)
            {
                GetCart().AddItem(phone, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int phoneId, string returnUrl)
        {
            Phone phone = repository.Phones
                .FirstOrDefault(g => g.PhoneId == phoneId);

            if (phone != null)
            {
                GetCart().RemoveLine(phone);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}