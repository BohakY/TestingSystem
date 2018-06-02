using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesterBacalar_v3.Models
{
    public class AddTestModel
    {
        public bool Answ1Check { get; set; }
        public bool Answ2Check { get; set; }
        public bool Answ3Check { get; set; }
        public bool Answ4Check { get; set; }
        public bool Answ5Check { get; set; }

        public AddTestModel()
        {
            Answ1Check = false;
            Answ2Check = false;
            Answ3Check = false;
            Answ4Check = false;
            Answ5Check = false;
        }
    }
}