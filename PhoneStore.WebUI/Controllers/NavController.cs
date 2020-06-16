using PhoneStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PhoneStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IPhoneRepository repository;
        public NavController(IPhoneRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Phones
                .Select(phone => phone.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView("FlexMenu", categories);
        }

    }
}