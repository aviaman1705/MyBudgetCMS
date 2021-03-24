using AutoMapper;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyBudgetCMS.Controllers.api
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {
        // GET: Dashboard
        private IDashboardRepository _dashboardRepository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        //public IHttpActionResult GetData()
        //{
        //    try
        //    {
        //        var model = Mapper.Map<DashboardDto>(_dashboardRepository.GetDashboardData());
        //        if (model != null)
        //            return Ok(model);
        //        else
        //            return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error($"Index() {DateTime.Now}");
        //        logger.Error(ex.Message);
        //        logger.Error("==============================");
        //        return InternalServerError();
        //    }
        //}
    }
}
