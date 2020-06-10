using System.Linq;
using System.Web.Mvc;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Abstract;
using PhoneStore.WebUI.Models;

namespace PhoneStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPhoneRepository repository;
        public CartController(IPhoneRepository repo)
        {
            repository = repo;
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