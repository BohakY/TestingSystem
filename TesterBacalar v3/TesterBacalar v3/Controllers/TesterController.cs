using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesterBacalar_v3.Controllers
{
    public class TesterController : Controller
    {
        // GET: Tester
        public ViewResult Tester()
        {
            @ViewBag.Who = "Тестер панель";
            return View();
        }

        public ViewResult testerView()
        {
            @ViewBag.Who = "testerV";
            return View();
        }
    }
}