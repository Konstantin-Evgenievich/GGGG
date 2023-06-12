using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Diplom.Controllers
{

    public class HomeController : Controller
    {
        Context db;
        public HomeController(Context context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            string role = User?.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
            //return Content($"ваша роль: {role}");
            return View(db.ApplicationBuh.ToList());
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

