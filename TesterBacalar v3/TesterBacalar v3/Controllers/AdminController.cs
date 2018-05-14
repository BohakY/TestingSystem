using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TesterBacalar_v3.Controllers
{
    public class AdminController : Controller
    {

        TesterBacalarWorkBDEntities Tes = new TesterBacalarWorkBDEntities();

        // GET: Admin
        public ViewResult AdminPanel()
        {
            var table = Tes.GetTableRezult(0).ToList();
            @ViewBag.Rezult = table;
            return View();
        }

        public ViewResult UserAdminstr()
        {
            return View();
        }

        public ViewResult TesterAdmin()
        {
            return View();
        }
    }
}