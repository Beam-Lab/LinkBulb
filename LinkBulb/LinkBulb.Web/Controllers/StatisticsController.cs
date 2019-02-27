using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LinkBulb.Web.Data;
using LinkBulb.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkBulb.Web.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public StatisticsController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new List<StatisticsViewModel>();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var links = await _context.Links.Where(l => l.UserId == Guid.Parse(userId)).ToListAsync();

            foreach (var link in links)
            {
                viewModel.Add(new StatisticsViewModel() { Link = link, Clicks = _context.LinkStatistics.Count(l => l.LinkID == link.ID) });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = new List<StatisticsViewModel>();



            return View(viewModel);
        }

    }
}