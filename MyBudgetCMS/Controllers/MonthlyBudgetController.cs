using AutoMapper;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudgetCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MonthlyBudgetController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IMonthlyBudgetRepository _monthlyBudgetRepository;

        public MonthlyBudgetController(IMonthlyBudgetRepository monthlyBudgetRepository)
        {
            _monthlyBudgetRepository = monthlyBudgetRepository;
        }

        //GET:Admin/MonthlyBudget
        public ActionResult Index()
        {
            return View();
        }

        //GET:Admin/MonthlyBudget/AddMonthlyBudget
        [HttpGet]
        public ActionResult AddMonthlyBudget()
        {
            return View();
        }

        //POST:Admin/MonthlyBudget/AddMonthlyBudget 
        [HttpPost]
        public ActionResult AddMonthlyBudget([Bind(Exclude = "Id")] MonthlyBudgetDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                MonthlyBudget dto = Mapper.Map<MonthlyBudget>(model);
                _monthlyBudgetRepository.Add(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"AddMonthlyBudget() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //GET:Admin/MonthlyBudget/EditMonthlyBudget
        [HttpGet]
        public ActionResult EditMonthlyBudget(int id)
        {
            try
            {
                MonthlyBudgetDto model;
                MonthlyBudget dto = _monthlyBudgetRepository.Get(id);

                if (dto == null)
                {
                    return Content("לא קיים תקציב.");
                }

                model = Mapper.Map<MonthlyBudgetDto>(dto);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"EditMonthlyBudget() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //POST:Admin/MonthlyBudget/EditMonthlyBudget    
        [HttpPost]
        public ActionResult EditMonthlyBudget(MonthlyBudgetDto model, HttpPostedFileBase file)
        {
            try
            {
                //Check the model state
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Get the entity
                MonthlyBudget dto = _monthlyBudgetRepository.Get(model.Id);
                if (dto == null)
                {
                    return Content("תקציב לא קיים");
                }
                else
                {
                    dto = Mapper.Map<MonthlyBudget>(model);

                    //Update dto entity
                    _monthlyBudgetRepository.Update(dto);

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"EditMonthlyBudget() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //GET:Admin/MonthlyBudget/DeleteMonthlyBudget
        [HttpGet]
        public ActionResult DeleteMonthlyBudget(int id)
        {
            try
            {
                _monthlyBudgetRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"DeleteMonthlyBudget() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }
    }
}