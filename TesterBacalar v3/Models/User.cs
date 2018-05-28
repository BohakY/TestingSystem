using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesterBacalar_v3.Models
{
    public class User
    {
        TesterBacalarWorkBDEntities Tes = new TesterBacalarWorkBDEntities();

        [Required(ErrorMessage = "Введіть логін!")]
        public string NameUser { get; set; }

        [Required(ErrorMessage = "Ведіть пароль!")]
        public string PassworsUser { get; set; }

        public SelectList DropDownList { get; set; }
        
    }
}