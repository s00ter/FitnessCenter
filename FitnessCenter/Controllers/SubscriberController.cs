using FitnessCenter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using static FitnessCenter.BL.Subscribers;

namespace FitnessCenter.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly FitnessCenterContext db;
        private static int curSubGroup;
        private static Subscriber curSub;

        public SubscriberController(FitnessCenterContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult Delete(Subscriber item)
        {
            item = curSub;
            if (ModelState.IsValid && CheckSubscriber(item, db))
            {
                var prevGroups = db.Groups.Where(x => x.Id == curSubGroup).ToList();
                prevGroups.ForEach(x =>
                {
                    x.SubscribersCount--;
                    db.Groups.Update(x);
                });
                db.Subscribers.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var mark = db.Subscribers.First(org => org.Id == id);
            curSubGroup = mark.GroupId;
            curSub = mark;
            return View(mark);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if (search == null)
            {
                return View(SelectSubscriber(db.Subscribers.ToList(), db));
            }
            var list = SelectSubscriber(db.Subscribers.ToList(), db)
                .Where(x => x.Name.ToLower().Contains(search.ToLower()));
            return View(list);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(SelectSubscriber(db.Subscribers.ToList(), db));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Subscriber item)
        {
            if (ModelState.IsValid && CheckSubscriber(item, db))
            {
                try
                {
                    var groups = db.Groups.Where(x => x.Id == item.GroupId).ToList();
                    groups.ForEach(x =>
                    {
                        x.SubscribersCount++;
                        db.Groups.Update(x);
                    });

                    db.Subscribers.Add(item);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            return RedirectToAction("Index", "Subscriber");
        }


    }
}
