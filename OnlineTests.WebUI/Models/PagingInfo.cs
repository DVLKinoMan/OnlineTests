using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTests.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (ItemsPerPage!=0) ? (TotalItems / ItemsPerPage + ((TotalItems % ItemsPerPage) > 0 ? 1 : 0)) : 0; }
        }
    }
}