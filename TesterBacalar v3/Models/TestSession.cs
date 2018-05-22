using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesterBacalar_v3;

namespace TesterBacalar_v3.Models
{
    public class TestSession
    {
        public Guid Id { get; set; }

        public int CurrentQuestion { get; set; }

        public int Result { get; set; }

        public Tests CurrentTest { get; set; }
    }
}