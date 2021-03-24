using AutoMapper;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBudgetCMS.Controllers
{
    public class TypeOfPaymentController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ITypeOfPaymentRepository _typeOfPaymentRepository;

        public TypeOfPaymentController(ITypeOfPaymentRepository typeOfPaymentRepository)
        {
            _typeOfPaymentRepository = typeOfPaymentRepository;
        }

        // GET: Admin/TypeOfPayment
        public ActionResult Index()
        {
            List<TypeOfPaymentDto> typesOfPayments = Mapper.Map<List<TypeOfPaymentDto>>(_typeOfPaymentRepository.GetAll().ToList());
            return View(typesOfPayments);
        }
    }
}