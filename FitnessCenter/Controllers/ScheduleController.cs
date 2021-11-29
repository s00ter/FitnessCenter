using FitnessCenter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FitnessCenter.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly FitnessCenterContext db;

        public ScheduleController(FitnessCenterContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var item = db.Schedules.First(x => x.Id == id);

            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Schedule item)
        {
            if (ModelState.IsValid && CheckSchedule(item))
            {
                try
                {
                    db.Schedules.Update(item);
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
        public IActionResult Index(string search)
        {
            if (search == null)
            {
                return View(db.Schedules.ToList());
            }
            var list = db.Schedules.ToList()
                .Where(x => x.DayOfWeek.ToLower().Contains(search.ToLower()));
            return View(list);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Schedules.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Schedule item)
        {
            if (ModelState.IsValid && CheckSchedule(item))
            {
                try
                {
                    db.Schedules.Add(item);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            return RedirectToAction("Index", "Schedule");
        }

        public bool CheckSchedule(Schedule item)
        {
            if (db.Groups.Any(x => x.Id == item.GroupId)
                && db.Groups.Any(x => x.Id == item.GroupId))
            {
                return true;
            }
            return false;
        }
    }
}
