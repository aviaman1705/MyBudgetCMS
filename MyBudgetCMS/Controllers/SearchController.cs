using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudgetCMS.Controllers
{
    public class SearchController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SearchController()
        {

        }

        public ActionResult Index(string searchStr)
        {
            ViewBag.SearchStr = searchStr;
            return View("Index");
        }
    }
}