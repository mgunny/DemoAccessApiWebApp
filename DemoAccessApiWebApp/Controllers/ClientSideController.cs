using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DemoAccessApiWebApp.Controllers
{
    public class ClientSideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}