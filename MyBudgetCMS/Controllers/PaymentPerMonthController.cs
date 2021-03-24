using AutoMapper;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudgetCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentPerMonthController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IPaymentPerMonthRepository _paymentPerMonthRepository;
        private IMonthlyBudgetRepository _monthlyBudgetRepository;
        private ICategoryRepository _categoryRepository;

        public PaymentPerMonthController(
            IPaymentPerMonthRepository paymentPerMonthRepository,
            IMonthlyBudgetRepository monthlyBudgetRepository,
            ICategoryRepository categoryRepository)
        {
            _paymentPerMonthRepository = paymentPerMonthRepository;
            _monthlyBudgetRepository = monthlyBudgetRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: Admin/PaymentPerMonth
        public ActionResult Index()
        {
            try
            {
                PaymentGridDto model = new PaymentGridDto();
                model.Expenses = Mapper.Map<List<PaymentGridItemDto>>(_paymentPerMonthRepository.GetAll());
                model.TotalSum = model.Expenses.Sum(x => x.Sum);

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

        //GET:Admin/PaymentPerMonth/AddPaymentPerMonth
        [HttpGet]
        public ActionResult AddPaymentPerMonth()
        {
            ViewBag.Budgets = LoadBudgets();
            ViewBag.Categories = LoadCategories();
            return View();
        }

        //POST:Admin/PaymentPerMonth/AddPaymentPerMonth
        [HttpPost]
        public ActionResult AddPaymentPerMonth([Bind(Exclude = "Id")] AddPaymentPerMonthDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Budgets = LoadBudgets();
                    ViewBag.Categories = LoadCategories();
                    return View(model);
                }

                PaymentPerMonth dto = Mapper.Map<PaymentPerMonth>(model);
                _paymentPerMonthRepository.Add(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"AddPaymentPerMonth() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //GET:Admin/PaymentPerMonth/EditPaymentPerMonth
        [HttpGet]
        public ActionResult EditPaymentPerMonth(int id)
        {
            try
            {
                EditPaymentPerMonthDto model;
                PaymentPerMonth dto = _paymentPerMonthRepository.Get(id);

                if (dto == null)
                {
                    return Content("לא קיים תקציב.");
                }

                ViewBag.Budgets = LoadBudgets();
                ViewBag.Categories = LoadCategories();

                model = Mapper.Map<EditPaymentPerMonthDto>(dto);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"EditPaymentPerMonth() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //POST:Admin/PaymentPerMonth/EditPaymentPerMonth
        [HttpPost]
        public ActionResult EditPaymentPerMonth(EditPaymentPerMonthDto model)
        {
            try
            {
                //Check the model state
                if (!ModelState.IsValid)
                {
                    ViewBag.Budgets = LoadBudgets();
                    ViewBag.Categories = LoadCategories();
                    return View(model);
                }

                //Get the entity
                PaymentPerMonth dto = _paymentPerMonthRepository.Get(model.Id);

                if (dto == null)
                {
                    return Content("תקציב לא קיים");
                }
                else
                {
                    dto = Mapper.Map<PaymentPerMonth>(model);

                    //Update dto entity
                    _paymentPerMonthRepository.Update(dto);

                    ViewBag.Budgets = LoadBudgets();
                    ViewBag.Categories = LoadCategories();

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

        //GET:Admin/PaymentPerMonth/DeletePaymentPerMonth/{id}
        [HttpGet]
        public ActionResult DeletePaymentPerMonth(int id)
        {
            try
            {
                _paymentPerMonthRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"DeletePaymentPerMonth() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        private List<SelectListItem> LoadBudgets()
        {
            var budgets = _monthlyBudgetRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.Date.ToString(),
                    Value = x.Id.ToString()
                }).OrderBy(xx => xx.Text).ToList();

            return budgets;
        }

        private List<SelectListItem> LoadCategories()
        {
            var categories = _categoryRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }).OrderBy(xx => xx.Text).ToList();

            return categories;
        }
    }
}