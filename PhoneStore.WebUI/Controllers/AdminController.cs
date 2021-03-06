﻿using PhoneStore.Domain.Abstract;
using PhoneStore.Domain.Entities;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneStore.WebUI.Controllers
{
    [Authorize]
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

        public ViewResult Edit(int phoneId)
        {
            Phone phone = repository.Phones.FirstOrDefault(p => p.PhoneId == phoneId);
            return View(phone);
        }
        [HttpPost]
        public ActionResult Edit(Phone phone, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    phone.ImageMimeType = image.ContentType;
                    phone.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(phone.ImageData, 0, image.ContentLength);
                }
                repository.SavePhone(phone);
                TempData["message"] = string.Format("Изменения в телефоне \"{0}\" были сохранены", phone.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(phone);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Phone());
        }
        [HttpPost]
        public ActionResult Delete(int phoneId)
        {
            Phone deletedPhone = repository.DeletePhone(phoneId);
            if (deletedPhone != null)
            {
                TempData["message"] = string.Format("Телефон \"{0}\" был удален",
                    deletedPhone.Name);
            }
            return RedirectToAction("Index");
        }
    }
}