using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesterBacalar_v3.Models;
using System.IO;

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
                var table = from Tests in Tes.Tests
                            select new
                            {
                                NameTest = Tests.test_name
                            };
                var name = table.ToList();

                for (int i = 0; i < name.Count; i++)
                {
                    if (named == name[i].NameTest)
                    {
                        return RedirectToAction("Error");
                    }
                }

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

        public ViewResult Error()
        {
            return View();
        }


        [HttpPost]
        public ActionResult TesterAdmin(string named, string selectSubj)
        {
            if (String.IsNullOrEmpty(named) == false)
            {
                var table = from Tests in Tes.Tests
                            select new
                            {
                                NameTest = Tests.test_name
                            };
                var name = table.ToList();

                for (int i = 0; i < name.Count; i++)
                {
                    if(named == name[i].NameTest)
                    {
                        return RedirectToAction("Error");
                    } 
                }
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
            SystemInfo.NumberQuest = 1;
            ViewBag.NameTest = SystemInfo.NameTestInSystem;
            ViewBag.sub = SystemInfo.SubjectName;
            ViewBag.NumberQuest = SystemInfo.NumberQuest;
            return View();
        }

        [HttpPost]
        public ActionResult AddTest(string action, AddTestModel adt, string quest, 
            string Answ_1, string Answ_2, string Answ_3, string Answ_4, string Answ_5,
            int score1, int score2, int score3, int score4, int score5)
        {
            if (action == "Добавити")
            {
                if (String.IsNullOrEmpty(quest) == true)
                {
                    ViewBag.NameTest = SystemInfo.NameTestInSystem;
                    ViewBag.sub = SystemInfo.SubjectName;
                    ViewBag.NumberQuest = SystemInfo.NumberQuest;
                    @ViewBag.Error = "Введіть запитання!";
                }
                else
                {
                    if (String.IsNullOrEmpty(Answ_1) == true && String.IsNullOrEmpty(Answ_2) == true)
                    {
                        ViewBag.NameTest = SystemInfo.NameTestInSystem;
                        ViewBag.sub = SystemInfo.SubjectName;
                        ViewBag.NumberQuest = SystemInfo.NumberQuest;
                        @ViewBag.Error = "Введіть хоча б дві відповіді!";
                    }
                    else
                    {

                        if (adt.Answ1Check == false &&
                    adt.Answ2Check == false &&
                    adt.Answ3Check == false &&
                    adt.Answ4Check == false &&
                    adt.Answ5Check == false)
                        {
                            ViewBag.NameTest = SystemInfo.NameTestInSystem;
                            ViewBag.sub = SystemInfo.SubjectName;
                            ViewBag.NumberQuest = SystemInfo.NumberQuest;
                            ViewBag.ErrorCheck = "Виберіть хоча б одну правильну відповідь!";
                        }
                        else
                        {

                            if(SystemInfo.NumberQuest == 1)
                            {
                                Tests newT = new Tests
                                {
                                    test_name = SystemInfo.NameTestInSystem,
                                    subject_name = SystemInfo.SubjectName
                                };
                                Tes.Tests.Add(newT);
                                Tes.SaveChanges();
                            }

                            int lenght = 2;

                            if(String.IsNullOrEmpty(Answ_3) == false)
                            {
                                lenght++;
                                if(String.IsNullOrEmpty(Answ_4) == false)
                                {
                                    lenght++;
                                    if (String.IsNullOrEmpty(Answ_5) == false)
                                    {
                                        lenght++;
                                    }
                                }
                            }

                            string[] anser = new string[lenght];
                            bool[] ifcorect = new bool[lenght];
                            int[] score = new int[lenght];

                            anser[0] = Answ_1;
                            ifcorect[0] = adt.Answ1Check;
                            score[0] = score1;
                            anser[1] = Answ_2;
                            ifcorect[1] = adt.Answ2Check;
                            score[1] = score2;

                            if (lenght >= 3)
                            {
                                anser[2] = Answ_3;
                                ifcorect[2] = adt.Answ3Check;
                                score[2] = score3;
                                if (lenght >= 4)
                                {
                                    anser[3] = Answ_4;
                                    ifcorect[3] = adt.Answ4Check;
                                    score[3] = score4;
                                    if (lenght == 5)
                                    {
                                        anser[4] = Answ_5;
                                        ifcorect[4] = adt.Answ5Check;
                                        score[4] = score5;
                                    }
                                }
                            }

                            int cir = 0;
                            int type = 1;
                            for(int i = 0; i < lenght; i++)
                            {
                                if(ifcorect[i] == true)
                                {
                                    cir++;
                                }
                                if(cir >= 2)
                                {
                                    type = 2;
                                }
                            }

                            int idTest = Tes.Tests.Where(c => c.test_name == SystemInfo.NameTestInSystem).
                                Select(c => c.test_id).FirstOrDefault();

                            Questions newQues = new Questions
                            {
                                test_id = idTest,
                                question_text = quest,
                                type = type
                            };

                            Tes.Questions.Add(newQues);
                            Tes.SaveChanges();

                            for (int i = 0; i < lenght; i++)
                            {
                                if(ifcorect[i] == false)
                                {
                                    score[i] = 0;
                                }
                            }

                            var table = from Questions in Tes.Questions
                                        select new
                                        {
                                            idQest = Questions.question_id
                                        };
                            var idmas = table.ToList();
                            int idQest = 0;
                            for (int i = 0; i < idmas.Count; i++)
                            {
                                if(idmas[i].idQest > idQest)
                                {
                                    idQest = idmas[i].idQest;
                                }
                            }

                            for(int i = 0; i < lenght; i++)
                            {
                                Answers newAns = new Answers
                                {
                                    question_id = idQest,
                                    answer_text = anser[i],
                                    is_correct = ifcorect[i],
                                    answer_score = score[i]
                                };
                                Tes.Answers.Add(newAns);
                                Tes.SaveChanges();
                            }

                            SystemInfo.NumberQuest++;
                            ViewBag.NameTest = SystemInfo.NameTestInSystem;
                            ViewBag.sub = SystemInfo.SubjectName;
                            ViewBag.NumberQuest = SystemInfo.NumberQuest;                         
                        }
                    }
                }
            }else if(action == "Закінчити створення")
            {
                SystemInfo.NumberQuest = 1;
                SystemInfo.NameTestInSystem = null;
                SystemInfo.SubjectName = null;
                return RedirectToAction("TestMenu");
            }
 
            return View();
        }
    }
}