using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesterBacalar_v3.Models;

namespace TesterBacalar_v3.Controllers
{
    public class AdminController : Controller
    {

        private TesterBacalarWorkBDEntities Tes = new TesterBacalarWorkBDEntities();

        // GET: Admin
        public ViewResult AdminPanel()
        {
            var table = Tes.GetTableRezult(0).ToList();
            ViewBag.Rezult = table;
            return View();
        }

        public ViewResult UserAdminstr()
        {
            return View();
        }

        public ViewResult TestMenu()
        {
            return View();
        }

        public ViewResult TesterAdmin()
        {
            var table = Tes.GetListSubject(0).ToList();
            User us = new User();
            us.DropDownList = new SelectList(table);
            return View(us);
        }

        public ViewResult TesterAdminInp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TesterAdminInp(string named, string selectSubj)
        {
            if (String.IsNullOrEmpty(named) == false && String.IsNullOrEmpty(selectSubj) == false)
            {
                SystemInfo.SubjectName = selectSubj;
                SystemInfo.NameTestInSystem = named;
                return RedirectToAction("AddTest");
            }
            else
            {
                @ViewBag.Error = "Введіть назву тесту та предмет!";
            }
            return View();
        }


        [HttpPost]
        public ActionResult TesterAdmin(string named, string selectSubj)
        {
            if (String.IsNullOrEmpty(named) == false)
            {
                SystemInfo.SubjectName = selectSubj;
                SystemInfo.NameTestInSystem = named;
                return RedirectToAction("AddTest");
            }
            else
            {
                @ViewBag.Error = "Введіть назву тесту!";
            }


            return View();
        }

        public ViewResult AddTest()
        {
            ViewBag.NameTest = SystemInfo.NameTestInSystem;
            ViewBag.sub = SystemInfo.SubjectName;
            int num = SystemInfo.NumberQuest;
            num++;
            ViewBag.NumberQuest = num;
            return View();
        }
    }
}