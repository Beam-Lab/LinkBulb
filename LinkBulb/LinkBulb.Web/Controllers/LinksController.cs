using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkBulb.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkBulb.Web.Controllers
{
    public class LinksController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public LinksController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("/l/{username}")]
        public async  Task<IActionResult> Index(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var links = await _context.Links.Where(l => l.UserId == Guid.Parse(user.Id)).ToListAsync();

            links = links.Where(l => l.PublishDate < DateTime.Now).ToList();

            return View(links);
        }
    }
}