using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission13.Models;

namespace Mission13.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private BowlingDbContext repo { get; set; }

        public TeamsViewComponent (BowlingDbContext temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["teamName"];

            var teams = repo.Teams
                //.Include(x => x.TeamID)
                //.Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x);
                
            return View(teams);
        }
    }
}
