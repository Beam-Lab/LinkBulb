using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkBulb.Web.Models;
using LinkBulb.Web.Data;
using System.Security.Claims;

namespace LinkBulb.Web.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _applicationDbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
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
