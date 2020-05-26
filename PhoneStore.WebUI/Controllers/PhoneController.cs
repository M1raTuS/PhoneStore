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
        public int pageSize = 4;
        public PhoneController(IPhoneRepository repository)
        {
            this.repository = repository;
        }
        //TODO:Переделать
        public ViewResult List(int page = 1)
        {
            return View(repository.Phones
                .OrderBy(phone => phone.PhoneId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize));
        }
    }
}