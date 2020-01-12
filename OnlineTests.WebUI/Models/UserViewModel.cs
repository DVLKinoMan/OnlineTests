using OnlineTests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserResult> UserResults { get; set; }
        public int Score { get { return UserResults.Sum(us => us.PointsEarned * us.Test.Level); } }
    }
}