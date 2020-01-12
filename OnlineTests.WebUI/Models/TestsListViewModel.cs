using OnlineTests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class TestsListViewModel
    {
        public IEnumerable<Test> Tests { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public int? CurrentLevel { get; set; }
    }
}