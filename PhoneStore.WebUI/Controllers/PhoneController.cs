using PhoneStore.Domain.Abstract;
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
        public PhoneController(IPhoneRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List()
        {
            return View(repository.Phones);
        }
    }
}