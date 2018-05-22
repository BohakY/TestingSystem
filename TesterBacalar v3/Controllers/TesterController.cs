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

        public RedirectToRouteResult RunTest(int testId)
        {
            Guid testSessionId = Guid.NewGuid();

            Tests currentTest = db.Tests.FirstOrDefault(t => t.test_id == testId);

            TestSession testSession = new TestSession
            {
                Id = testSessionId,
                CurrentQuestion = 0,
                Result = 0,
                CurrentTest = currentTest
            };

            Session[testSessionId.ToString()] = testSession;

            return RedirectToAction("GetQuestion", new { sessionId = testSessionId });
        }

        public ViewResult GetQuestion(Guid sessionId)
        {
            TestSession session = Session[sessionId.ToString()] as TestSession;
            List<Questions> questions = session.CurrentTest.Questions.ToList();

            Questions question = questions[session.CurrentQuestion];

            //session.CurrentQuestion += 1;

            //ViewBag.SessionId = sessionId;
            ViewBag.Session = session;

            return View(question);
        }

        public RedirectToRouteResult CheckResult(Guid sessionId)
        {
            TestSession session = Session[sessionId.ToString()] as TestSession;

            session.CurrentQuestion += 1;
            Session[sessionId.ToString()] = session;

            return RedirectToAction("GetQuestion", new { sessionId });
        }

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



            return View();
        }
    }
}