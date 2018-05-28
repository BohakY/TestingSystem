using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesterBacalar_v3.Models;

namespace TesterBacalar_v3.Controllers
{
    public class TesterController : Controller
    {

        TesterBacalarWorkBDEntities db = new TesterBacalarWorkBDEntities();
        // GET: Tester
        public ViewResult Tester()
        {
            var table = db.Tests.ToList();
            @ViewBag.Tests = table;

            return View();
        }

        public ViewResult testerView()
        {
            @ViewBag.Who = "testerV 222";
            return View();
        }

        public ViewResult bbb()
        {
            @ViewBag.Who = "bbb";
            return View();
        }

        public ViewResult Question()
        {
            @ViewBag.Who = "tsdfgghfgjghjhjk";
            return View();
        }
    }
}