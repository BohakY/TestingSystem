using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        public ViewResult bbb()
        {
            @ViewBag.Who = "bbb";
            return View();
        }
       
        public ViewResult Question()
        {

            var q = db.Questions.ToList();
            @ViewBag.Questions = q;

            var a = db.Answers.ToList();
            @ViewBag.Answers = a;

            return View();
        }
    }
}