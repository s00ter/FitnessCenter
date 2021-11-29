using FitnessCenter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FitnessCenter.Controllers
{
    public class InstructorController : Controller
    {
        private readonly FitnessCenterContext db;

        public InstructorController(FitnessCenterContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var item = db.Instructors.First(x => x.Id == id);

            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Instructor item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Instructors.Update(item);
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
        public IActionResult Delete(Instructor item)
        {
            if (ModelState.IsValid)
            {
                db.Instructors.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var mark = db.Instructors.First(org => org.Id == id);
            return View(mark);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            if (search == null)
            {
                return View(db.Instructors.ToList());
            }
            var list = db.Instructors.ToList()
                .Where(x => x.Specialization.ToLower().Contains(search.ToLower()));
            return View(list);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(db.Instructors.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instructor item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Instructors.Add(item);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }

            return RedirectToAction("Index", "Instructor");
        }
    }
}
