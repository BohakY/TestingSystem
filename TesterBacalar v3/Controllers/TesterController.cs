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
           // Questions firstQuestion = db.Questions.First(q => q.test_id == currentTest.test_id);
            //int firstQuestionId = firstQuestion.question_id;

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

        public RedirectToRouteResult CheckResult(Guid sessionId, string[] arr, string action)
        {
            //if(action == "fre")
            //{
            //    return RedirectToAction("GetQuestion");
            //}

            TestSession session = Session[sessionId.ToString()] as TestSession;
            List<Questions> questions = session.CurrentTest.Questions.ToList();

            int currentTest = session.CurrentTest.test_id;

            int questionsCount = questions.Count();
            int currentQuestion = session.CurrentQuestion + 1;

      

            string[] checkData = arr.ToArray();

            List<Answers> trueData = db.Answers.Where(a => a.question_id == currentQuestion && a.answer_score == 1).ToList();

            //string trueData = db.Answers.Where(a => a.question_id == currentQuestion && a.answer_score == 1).FirstOrDefault().answer_text;

            if (currentQuestion >= questionsCount - 1)
            {
                Rezult currentRezult = new Rezult
                {
                    user_id_ = SystemInfo.UserId,
                    test_id = currentTest,
                    points = 7,
                    total_score = 18,
                    data_time = DateTime.Now
                };

                db.Rezult.Add(currentRezult);
                db.SaveChanges();
                
                
                return RedirectToAction("ResultView", currentRezult);
            }

           // int currentPoints, currentScore = 0;

            //int[] ans = db.Answers.Where(a => a.question_id == currentQuestion).FirstOrDefault().answer_score




            session.CurrentQuestion += 1;
            Session[sessionId.ToString()] = session;

            return RedirectToAction("GetQuestion", new { sessionId });

        }

        public ViewResult Tester()
        {

            ViewBag.Id = SystemInfo.UserId;
            var table = db.Tests.ToList();
            @ViewBag.Tests = table;
      
            string firstName = db.Users.Find(SystemInfo.UserId).first_name.ToString();
            ViewBag.FirstName = firstName;

            string lastName = db.Users.Find(SystemInfo.UserId).last_name.ToString();
            ViewBag.LastName = lastName;

            return View();
        }

        public ViewResult ResultView(Rezult currentResult)
        {
            //Tests currentTest = db.Tests.FirstOrDefault(t => t.test_id == testId);
            //Users currentUser = db.Users.FirstOrDefault(u => u.user_id_ == SystemInfo.UserId);

            string currentUserFirstName = db.Users.Where(u => u.user_id_ == SystemInfo.UserId).FirstOrDefault().first_name;
            string currentUserSurname = db.Users.Where(u => u.user_id_ == SystemInfo.UserId).FirstOrDefault().last_name;

            ViewBag.currentUserSurname = currentUserSurname;
            ViewBag.currentUserFirstName = currentUserFirstName;

<<<<<<< HEAD
            string currentTest = db.Tests.Where(t => t.test_id == currentResult.test_id).FirstOrDefault().test_name;
            ViewBag.currentTest = currentTest;
=======
            //string testName = 
            //ViewBag.testName = testName;
>>>>>>> fde36d39b8d7030d0598991a8b71f58a716a71b3

            
            //int currentPoints = 

            return View();
        }


        public ViewResult Statistics()
        {
            ViewBag.Id = SystemInfo.UserId;

            string firstName = db.Users.Find(SystemInfo.UserId).first_name.ToString();
            ViewBag.FirstName = firstName;

            string lastName = db.Users.Find(SystemInfo.UserId).last_name.ToString();
            ViewBag.LastName = lastName;


            var statistics = db.Rezult.Where(s => s.user_id_ == SystemInfo.UserId).ToList();
            var tableStat = statistics;
            @ViewBag.tableStat = tableStat;


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