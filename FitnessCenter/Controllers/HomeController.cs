using FitnessCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static FitnessCenter.BL.Instructors;

namespace FitnessCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FitnessCenterContext db;

        private static IEnumerable<Instructor> tempInstructors;
        private static IEnumerable<SubscriberVM> tempSubscribers;
        private static IEnumerable<GroupVM> tempGroups;
        private static IEnumerable<GroupCash> tempSelectGr;

        public HomeController(ILogger<HomeController> logger, FitnessCenterContext context)
        {
            db = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SelectDate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SelectDate(SelectGr date)
        {
            var x = BL.Groups.GetCash(date, db);
            tempSelectGr = x;
            return RedirectToAction("SelectGr");
        }

        public IActionResult SelectGr()
        {
            return View(tempSelectGr);
        }

        public IActionResult SelectGroupMode()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchGroup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchGroup(GroupSearchKey item)
        {
            var x = BL.Groups.GetGroup(db, item);
            tempGroups = x;
            return RedirectToAction("Group");
        }

        public IActionResult Group()
        {
            return View(tempGroups);
        }

        [HttpGet]
        public IActionResult InstructorSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InstructorSearch(SearchItem item)
        {
            var x = GetInstructors(item.Prop, item.Value, db);
            tempInstructors = x;
            return RedirectToAction("Instructor");
        }

        [HttpGet]
        public IActionResult SubscriberSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubscriberSearch(SubscriberSearchKey searchkey)
        {
            var x = BL.Subscribers.GetSubscribers(searchkey, db);
            tempSubscribers = x;
            return RedirectToAction("Subscriber");
        }

        public IActionResult Subscriber()
        {
            return View(tempSubscribers);
        }

        public IActionResult Instructor()
        {
            return View(tempInstructors);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
