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

        const int ANSWERCOEF1 = 1;
        const int ANSWERCOEF2 = 2;
        const int ANSWERCOEF3 = 3;

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

            Session[testSessionId.ToString()] = testSession;

            return RedirectToAction("GetQuestion", new { sessionId = testSessionId });
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
            TestSession session = Session[sessionId.ToString()] as TestSession;
            List<Questions> questions = session.CurrentTest.Questions.ToList();

            int currentTest = session.CurrentTest.test_id;
            int questionsCount = questions.Count();
            Questions currentQuestionObj = db.Questions.FirstOrDefault(q => q.test_id == currentTest);
            int currentQuestion = currentQuestionObj.question_id + session.CurrentQuestion;

            if (action == "Пропустити")
            {
                session.CurrentQuestion += 1;
                Session[sessionId.ToString()] = session;
                return RedirectToAction("GetQuestion");
            }
            else if (action == "Відповісти")
            {
                bool isWrite = true;

                try
                {
                    if (arr.Length == 0)
                    {
                        isWrite = false;
                    }
                }
                catch (Exception ex)
                {
                    isWrite = false;
                }
                if (isWrite)
                {
                    List<string> checkData = arr.ToList();

                    List<Answers> storageData = db.Answers.Where(a => a.question_id == currentQuestion).ToList();
                    List<Answers> trueData = new List<Answers>();

                    foreach (Answers elem in storageData)
                    {
                        if (elem.answer_score == ANSWERCOEF1 || elem.answer_score == ANSWERCOEF2 || elem.answer_score == ANSWERCOEF3)
                        {
                            trueData.Add(elem);
                        }
                    }

                    List<string> trueDataString = new List<string>();

                    foreach (Answers element in trueData)
                    {
                        string currentElement = element.answer_text;
                        trueDataString.Add(currentElement);
                    }

                    bool isEqual = trueDataString.SequenceEqual(checkData);
                    if (isEqual)
                    {
                        SystemInfo.points++;
                        foreach (Answers element in trueData)
                        {
                            int answerScore = (int)element.answer_score;
                            SystemInfo.totalScore += answerScore;
                        }
                    }

                    if (session.CurrentQuestion >= questionsCount - 1)
                    {
                        Rezult currentRezult = new Rezult
                        {
                            user_id_ = SystemInfo.UserId,
                            test_id = currentTest,
                            points = SystemInfo.points,
                            total_score = SystemInfo.totalScore,
                            data_time = DateTime.Now
                        };

                        db.Rezult.Add(currentRezult);
                        db.SaveChanges();

                        ViewBag.CurrentTest = currentTest;
                        ViewBag.Score = SystemInfo.points;
                        return RedirectToAction("ViewResult", currentRezult);
                    }

                    session.CurrentQuestion += 1;
                    Session[sessionId.ToString()] = session;

                    return RedirectToAction("GetQuestion", new { sessionId });
                }
                else
                {
                    return RedirectToAction("GetQuestion", new { sessionId });
                }
            }
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

        public ViewResult ViewResult(Rezult currentResult)
        {

            string currentUserFirstName = db.Users.Where(u => u.user_id_ == SystemInfo.UserId).FirstOrDefault().first_name;
            string currentUserSurname = db.Users.Where(u => u.user_id_ == SystemInfo.UserId).FirstOrDefault().last_name;

            ViewBag.currentUserSurname = currentUserSurname;
            ViewBag.currentUserFirstName = currentUserFirstName;


            string currentTest = db.Tests.Where(t => t.test_id == currentResult.test_id).FirstOrDefault().test_name;
            ViewBag.currentTest = currentTest;
<<<<<<< HEAD

            //string testName = 
            //ViewBag.testName = testName;
=======
>>>>>>> 8a2bcced945f8eb5a4d90072dd96939338e1234a


            ViewBag.Points = SystemInfo.points;
            ViewBag.TotalScore = SystemInfo.totalScore;

            //if (action == "Переглянути статистику")
            //{
            //    return RedirectToAction("Satistics") as ViewResult;
            //}

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