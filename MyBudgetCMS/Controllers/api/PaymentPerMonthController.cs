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

namespace MyBudgetCMS.Controllers.api
{
    [Authorize(Roles = "Admin")]
    public class PaymentPerMonthController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IPaymentPerMonthRepository _paymentPerMonthRepository;        

        public PaymentPerMonthController(IPaymentPerMonthRepository paymentPerMonthRepository)
        {
            _paymentPerMonthRepository = paymentPerMonthRepository;
        }

        public IHttpActionResult GetData(string sEcho, string sSearch, int iDisplayStart, int iDisplayLength, string iSortCol_0, string sSortDir_0)
        {
            try
            {
                //iSortCol gives your Column numebr of for which sorting is required
                int iSortCol = Convert.ToInt32(iSortCol_0);
                //provides your sort order (asc/desc)
                string sortOrder = sSortDir_0;

                //get total value count
                var Count = _paymentPerMonthRepository.GetAll().Count();

                var Payments = new List<PaymentGridItemDto>();

                //Search query when sSearch is not empty
                if (sSearch != "" && sSearch != null) //If there is search query
                {

                    Payments = Mapper.Map<List<PaymentGridItemDto>>(_paymentPerMonthRepository.GetAll()
                        .Where(a => a.Category.Title.ToString().ToLower().Contains(sSearch.ToLower())
                                      || a.CreatedDate.ToString().ToLower().Contains(sSearch.ToLower())
                                      || a.Sum.ToString().ToLower().Contains(sSearch.ToLower()))                                      
                                      .ToList());

                    Count = Payments.Count();
                    // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                    Payments = SortFunction(iSortCol, sortOrder, Payments).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    //get data from database
                    Payments = Mapper.Map<List<PaymentGridItemDto>>(_paymentPerMonthRepository.GetAll() //speficiy conditions if there is any using .Where(Condition)                             
                                       .ToList());

                    // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                    Payments = SortFunction(iSortCol, sortOrder, Payments).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var PaymentsPaged = new SysDataTablePager<PaymentGridItemDto>(Payments, Count, Count, sEcho);

                return Ok(PaymentsPaged);
            }
            catch (Exception ex)
            {
                logger.Error($"GetData() {DateTime.Now}");
                logger.Error(ex);
                logger.Error("==============================");
                return InternalServerError();
            }
        }

        //Sorting Function
        private List<PaymentGridItemDto> SortFunction(int iSortCol, string sortOrder, List<PaymentGridItemDto> list)
        {
            if (iSortCol == 2 || iSortCol == 3 || iSortCol == 4 || iSortCol == 5)
            {
                switch (iSortCol)
                {
                    case 2:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Date).ToList();
                        else
                            list = list.OrderBy(c => c.Date).ToList();
                        break;
                    case 3:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Sum).ToList();
                        else
                            list = list.OrderBy(c => c.Sum).ToList();
                        break;
                    case 4:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.CategoryName).ToList();
                        else
                            list = list.OrderBy(c => c.CategoryName).ToList();
                        break;
                    case 5:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Id).ToList();
                        else
                            list = list.OrderBy(c => c.Id).ToList();
                        break;
                }
            }

            return list;
        }

    }
}
