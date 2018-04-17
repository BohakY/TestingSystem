using System;
using System.Collections.Generic;
using System.Linq;
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
            @ViewBag.Who = "Тут мають бути користувачі";
            return View();
        }

    }
}