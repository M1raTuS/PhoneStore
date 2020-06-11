using PhoneStore.Domain.Abstract;
using PhoneStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace PhoneStore.WebUI.Controllers
{
    public class PhoneController : Controller
    {
        private IPhoneRepository repository;
        public int pageSize = 4;
        public PhoneController(IPhoneRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            PhonesListViewModel model = new PhonesListViewModel
            {
                Phones = repository.Phones
                .Where(p => category == null || p.Category == category)
                .OrderBy(phone => phone.PhoneId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Phones.Count() :
                    repository.Phones.Where(phone => phone.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}