using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public RedirectToRouteResult RunTest(int testId, int userId)
        {
            Guid testSessionId = Guid.NewGuid();

            Tests currentTest = db.Tests.FirstOrDefault(t => t.test_id == testId);
            Users currentUser = db.Users.FirstOrDefault(u => u.user_id_ == userId);

            TestSession testSession = new TestSession
            {
                Id = testSessionId,
                CurrentQuestion = 0,
                CurrentUser = currentUser,
                Result = 0,
                CurrentTest = currentTest
            };

            //string testName = currentTest.test_name.ToString();
            

            Session[testSessionId.ToString()] = testSession;

            return RedirectToAction("GetQuestion", new { sessionId = testSessionId } );
        }

        public ViewResult GetQuestion(Guid sessionId)
        {
            TestSession session = Session[sessionId.ToString()] as TestSession;
            List<Questions> questions = session.CurrentTest.Questions.ToList();
            
            Questions question = questions[session.CurrentQuestion];

            ViewBag.Session = session;

          

            return View(question);
        }

        public RedirectToRouteResult CheckResult(Guid sessionId, string[] arr)
        {
            TestSession session = Session[sessionId.ToString()] as TestSession;
            List<Questions> questions = session.CurrentTest.Questions.ToList();

            int currentTest = session.CurrentTest.test_id;

            int questionsCount = questions.Count();
            int currentQuestion = session.CurrentQuestion;

            if (currentQuestion >= questionsCount - 1)
            {
                //Rezult currentResult { user_id}
                //db.Rezult.Add

                //var result = db.Set<Rezult>();
                //result.Add(new Rezult { user_id_ = SystemInfo.UserId , test_id = currentTest, points = 5, total_score = 10, data_time = DateTime.Now};
                //db.SaveChanges();



                //Rezult currentResult = new Rezult { user_id_ = SystemInfo.UserId, test_id = currentTest, points = 5, total_score = 10, data_time = DateTime.Now };
                //db.Rezult.Add(currentResult);
                //db.SaveChanges();

                return RedirectToAction("Tester");
            }
            session.CurrentQuestion += 1;
            Session[sessionId.ToString()] = session;

            return RedirectToAction("GetQuestion", new { sessionId });

        }

        public ViewResult Tester()
        {
            
            ViewBag.Nameuser = SystemInfo.UserNameInSystem;
            ViewBag.Id = SystemInfo.UserId;
            var table = db.Tests.ToList();
            @ViewBag.Tests = table;
      
            string firstName = db.Users.Find(SystemInfo.UserId).first_name.ToString();
            ViewBag.FirstName = firstName;

            string lastName = db.Users.Find(SystemInfo.UserId).last_name.ToString();
            ViewBag.LastName = lastName;


           

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



            return View();
        }
    }
}