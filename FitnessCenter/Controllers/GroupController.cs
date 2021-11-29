using FitnessCenter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using static FitnessCenter.BL.Groups;

namespace FitnessCenter.Controllers
{
    public class GroupController : Controller
    {
        private readonly FitnessCenterContext db;

        public GroupController(FitnessCenterContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var item = db.Groups.First(x => x.Id == id);

            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Group item)
        {
            if (ModelState.IsValid && CheckGroup(item, db))
            {
                try
                {
                    db.Groups.Update(item);
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
        public IActionResult Delete(Group item)
        {
            if (ModelState.IsValid && CheckGroup(item, db))
            {
                var subs = db.Subscribers.Where(x => x.GroupId == item.Id).ToList();
                subs.ForEach(x => db.Subscribers.Remove(x));
                var sch = db.Schedules.Where(x => x.GroupId == item.Id).ToList();
                sch.ForEach(x => db.Schedules.Remove(x));
                db.Groups.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var mark = db.Groups.First(org => org.Id == id);
            return View(mark);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if (search == null)
            {
                return View(SelectGroup(db.Groups.ToList(), db));
            }
            var list = SelectGroup(db.Groups.ToList(), db)
                .Where(x => x.Name.ToLower().Contains(search.ToLower()));
            return View(list);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(SelectGroup(db.Groups.ToList(), db));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group item)
        {
            if (ModelState.IsValid && CheckGroup(item, db))
            {
                try
                {
                    db.Groups.Add(item);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            return RedirectToAction("Index", "Group");
        }
    }
}
