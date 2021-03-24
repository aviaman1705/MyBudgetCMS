using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudgetCMS.Controllers
{
    public class LayoutController : Controller
    {
        public LayoutController()
        {
   
        }

        public ActionResult Index(string searchStr)
        {
            return View("Index",searchStr);
        }

        //public ActionResult _Header()
        //{

        //    return PartialView("_Header", LoadPages());
        //}

        //public ActionResult _Footer()
        //{            
        //    return PartialView("_Footer",LoadPages());
        //}

        //private List<PageVM> LoadPages()
        //{
        //    var pages = Mapper.Map<List<PageVM>>(_pageRepository.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.Sorting));
        //    return pages;
        //}
    }
}