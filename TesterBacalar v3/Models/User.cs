using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesterBacalar_v3.Models
{
    public class User
    {
        [Required(ErrorMessage = "Введіть логін!")]
        public string NameUser { get; set; }

        [Required(ErrorMessage = "Ведіть пароль!")]
        public string PassworsUser { get; set; }
    }
}