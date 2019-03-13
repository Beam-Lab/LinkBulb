using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkBulb.Models;
using LinkBulb.Web.Data;
using LinkBulb.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wangkanai.Detection;

namespace LinkBulb.Web.Controllers
{
    //TODO - Change TimeZone - http://prideparrot.com/blog/archive/2011/9/how_to_display_dates_and_times_in_clients_timezone

    public class LinksController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        private readonly IDetection _detection;

        public LinksController(UserManager<IdentityUser> userManager, ApplicationDbContext context, IDetection detection)
        {
            _userManager = userManager;
            _context = context;

            _detection = detection;
        }

        [Route("/l/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var linksPageViewModel = new LinksPageViewModel();
            linksPageViewModel.Links = new List<Link>();

            var user = await _userManager.FindByNameAsync(username);

            linksPageViewModel.UserName = user.UserName;

            var links = await _context.Links.Where(l => l.UserId == Guid.Parse(user.Id)).ToListAsync();

            linksPageViewModel.Links = links.Where(l => l.PublishDate < DateTime.Now).ToList();

            return View(linksPageViewModel);
        }

        [Route("/l/goto/{linkid}")]
        public async Task<IActionResult> GoTo(string linkid)
        {
            var link = _context.Links.FirstOrDefault( l => l.ID == Guid.Parse(linkid));

            var browser = _detection.Browser;
            var deviceType = _detection.Device.Type.ToString();
            var os = _detection.Platform == null ? string.Empty : _detection.Platform.Type.ToString();

            var linkStatistic = new LinkStatistic() { LinkID = link.ID, ClickDate = DateTime.Now, Browser = browser.Type.ToString(), BrowserVersion = browser.Version.ToString() , DeviceType = deviceType , OS = os };

            _context.LinkStatistics.Add(linkStatistic);
            await _context.SaveChangesAsync();

            return Redirect(link.Url);
        }
    }
}