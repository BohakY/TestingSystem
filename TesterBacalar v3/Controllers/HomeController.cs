using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using TesterBacalar_v3.Models;

namespace TesterBacalar_v3.Controllers
{
    public class HomeController : Controller
    {
        TesterBacalarWorkBDEntities Tes = new TesterBacalarWorkBDEntities();

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            var table = from Users in Tes.Users
                        select new
                        {
                            UserId = Users.user_id_,
                            UserName = Users.login
                        };
            var temp = table.ToList();

            for (int i = 0; i < temp.Count; i++)
            {
                if (user.NameUser == temp[i].UserName)
                {
                    SystemInfo.UserId = temp[i].UserId;
                }
            }

            ObjectResult<int?> check = Tes.Check_User(user.NameUser, user.PassworsUser);
            int res = check.FirstOrDefault().GetValueOrDefault();

            if (res == 2)
            {
                SystemInfo.UserNameInSystem = user.NameUser;
                return RedirectToAction("Tester", "Tester");
            }
            else if (res == 3)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            else if (res == 1)
            {
                ViewBag.Error = "Логін або пароль введені неправильно!";
            }

            return View();
        }
    }
}