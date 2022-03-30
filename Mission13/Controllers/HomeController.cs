using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private BowlingDbContext _repo { get; set; }

        //Constructor
        public HomeController(BowlingDbContext temp)
        {
            _repo = temp;
        }

        public IActionResult Index(int teamid)
        {
            var blah = _repo.Bowlers
                .Where(b => b.TeamID == teamid || teamid == 0)
                .ToList();

            if (teamid == 0)
            {
                ViewBag.Header = "All Teams";
            }
            else
            {
                ViewBag.Header = _repo.Teams.Single(x => x.TeamID == teamid).TeamName;
            }

            return View(blah);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            _repo.Update(bowler);
            _repo.SaveChanges();
            return RedirectToAction("Index", bowler);
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Bowler = _repo.Bowlers.ToList();
            var bowler = _repo.Bowlers.Single(b => b.BowlerID == bowlerid);

            return View("Edit", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {
            _repo.Update(bowler);
            _repo.SaveChanges();

            return RedirectToAction("Index", bowler);
        }

        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var bowler = _repo.Bowlers.Single(b => b.BowlerID == bowlerid);

            return View();
        }

        [HttpPost]
        public IActionResult Delete(Bowler bowler)
        {
            _repo.Bowlers.Where(b => b.BowlerID == bowler.BowlerID);

            _repo.Bowlers.Remove(bowler);
            _repo.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
