using AutoMapper;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudgetCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        private IDashboardRepository _dashboardRepository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            try
            {
                var model = Mapper.Map<DashboardDto>(_dashboardRepository.GetDashboardData());
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"Index() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }
    }
}