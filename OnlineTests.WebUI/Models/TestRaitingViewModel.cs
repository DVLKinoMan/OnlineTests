using OnlineTests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class TestRaitingViewModel
    {
        public Test Test { get; set; }
        public int TestTakenNumber { get { return UserResutls.Count(); } }
        public IEnumerable<UserResult> UserResutls { get; set; }
        public double AveragePoint { get { return UserResutls.Count()!=0 ? UserResutls.Average(u => u.PointsEarned):0; } }
    }
}