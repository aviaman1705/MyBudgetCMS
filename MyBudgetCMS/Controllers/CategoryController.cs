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
    public class CategoryController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ICategoryRepository _categoryRepository;
        private ITypeOfPaymentRepository _typeOfPaymentRepository;

        public CategoryController(
            ICategoryRepository categoryRepository,
            IPaymentPerMonthRepository paymentPerMonthRepository,
            ITypeOfPaymentRepository typeOfPaymentRepository)
        {
            _categoryRepository = categoryRepository;
            _typeOfPaymentRepository = typeOfPaymentRepository;
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        //GET:Admin/MonthlyBudget/AddMonthlyBudget
        [HttpGet]
        public ActionResult AddCategory()
        {
            try
            {
                ViewBag.Parents = LoadParents();
                ViewBag.TypesOfPayments = LoadTypesOfPayments();
                return View();
            }
            catch (Exception ex)
            {
                logger.Error($"AddCategory() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //POST:Admin/Category/AddCategory
        [HttpPost]
        public ActionResult AddCategory([Bind(Exclude = "Id")] AddCategoryDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Parents = LoadParents();
                    ViewBag.TypesOfPayments = LoadTypesOfPayments();
                    return View(model);
                }

                if (_categoryRepository.GetAll().Any(x => x.Title == model.Title))
                {
                    ViewBag.Parents = LoadParents();
                    ViewBag.TypesOfPayments = LoadTypesOfPayments();
                    ModelState.AddModelError("CustomError", "קטגוריה כבר קיימת");
                    return View(model);
                }

                Category dto = Mapper.Map<Category>(model);
                _categoryRepository.Add(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"AddCategory() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //GET:Admin/Category/EditCategory
        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "ID")]
        public ActionResult EditCategory(int id)
        {
            try
            {
                EditCategoryDto model;
                Category dto = _categoryRepository.Get(id);

                if (dto == null)
                {
                    return Content("הקטגוריה לא קיימת");
                }

                model = Mapper.Map<EditCategoryDto>(dto);

                ViewBag.Parents = LoadParents();
                ViewBag.TypesOfPayments = LoadTypesOfPayments();

                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error($"EditCategory() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //POST:Admin/Category/EditCategory
        [HttpPost]
        public ActionResult EditCategory(EditCategoryDto model)
        {
            try
            {
                //Check the model state
                if (!ModelState.IsValid)
                {
                    ViewBag.Parents = LoadParents();
                    ViewBag.TypesOfPayments = LoadTypesOfPayments();
                    return View(model);
                }

                //Make sure title and slug are unique
                if (_categoryRepository.GetAll().Where(x => x.Id != model.Id).Any(x => x.Title == model.Title))
                {
                    ViewBag.Parents = LoadParents();
                    ModelState.AddModelError("CustomError", "קטגוריה זו כבר קיימת.");
                    ViewBag.TypesOfPayments = LoadTypesOfPayments();
                    return View(model);
                }

                //Get the Sentence
                Category dto = _categoryRepository.Get(model.Id);
                if (dto == null)
                {
                    return Content("הקטגוריה לא קיימת");
                }
                else
                {
                    dto = Mapper.Map<Category>(model);

                    //Update dto object
                    _categoryRepository.Update(dto);
                    ViewBag.TypesOfPayments = LoadTypesOfPayments();
                    ViewBag.Parents = LoadParents();

                    //  Get the url for the action method:  
                    var url = Url.Action("EditCategory", "Category", new { Id = model.Id });

                    //  Remove the item from cache  
                    Response.RemoveOutputCacheItem(url);

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"EditSentence() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        //GET:Admin/Category/DeleteCategory
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error($"DeleteCategory() {DateTime.Now}");
                logger.Error(ex.Message);
                logger.Error("==============================");
                return null;
            }
        }

        private List<SelectListItem> LoadTypesOfPayments()
        {
            var typesOfPayments = _typeOfPaymentRepository.GetAll()
                .Select(x => new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }).OrderBy(xx => xx.Text).ToList();

            return typesOfPayments;
        }

        private List<SelectListItem> LoadParents()
        {
            var parents = _categoryRepository.GetAll()
                .Where(xx => xx.ParentID == null)
                .Select(x => new SelectListItem()
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }).OrderBy(xx => xx.Text).ToList();

            return parents;
        }
    }
}