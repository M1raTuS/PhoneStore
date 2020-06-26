using PhoneStore.Domain.Abstract;
using System.Web.Mvc;

namespace PhoneStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IPhoneRepository repository;
        public AdminController(IPhoneRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Phones);
        }
    }
}