﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterBacalar_v3.Models
{
    public class SystemInfo
    {
        public static int UserId { get; set; }
        public static string UserNameInSystem { get; set; }
        public static string NameTestInSystem { get; set; }
        public static string SubjectName { get; set; }
        public static int NumberQuest { get; set; }
        public static int totalScore = 0;
        public static int points = 0;

        public SystemInfo()
        {
            UserId = 0;
            NumberQuest = 1;
        }
    }
}