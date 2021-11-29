using FitnessCenter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FitnessCenter.Controllers
{
    public class SubscribtionController : Controller
    {
        private readonly FitnessCenterContext db;

        public SubscribtionController(FitnessCenterContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var org = db.Subscribtions.First(x => x.Id == id);

            return View(org);
        }

        [HttpPost]
        public IActionResult Update(Subscribtion subsc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Subscribtions.Update(subsc);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(Subscribtion subsc)
        {
            if (ModelState.IsValid)
            {
                var subs = db.Subscribers.Where(x => x.SubscribtionId == subsc.Id).ToList();
                subs.ForEach(x =>
                {
                    var groups = db.Groups.Where(gr => gr.Id == x.GroupId).ToList();
                    groups.ForEach(gr =>
                    {
                        gr.SubscribersCount--;
                        db.Groups.Update(gr);
                    });
                    db.Subscribers.Remove(x);
                });
                db.Subscribtions.Remove(subsc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var subsc = db.Subscribtions.First(org => org.Id == id);
            return View(subsc);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if ((search != null) && decimal.TryParse(search, out decimal value))
            {
                var list = db.Subscribtions.Where(x => x.Price <= value);
                return View(list);
            }
            return View(db.Subscribtions.ToList());
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Subscribtions.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Subscribtion subsc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Subscribtions.Add(subsc);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            return RedirectToAction("Index", "Subscribtion");
        }
    }
}
