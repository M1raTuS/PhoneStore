using PhoneStore.Domain.Abstract;
using PhoneStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ViewResult List(int page = 1)
        {
            PhonesListViewModel model = new PhonesListViewModel
            {
                Phones = repository.Phones
                .OrderBy(phone => phone.PhoneId)
                .Skip((page - 1)*pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Phones.Count()
                }
            };
            return View(model);
        }
    }
}