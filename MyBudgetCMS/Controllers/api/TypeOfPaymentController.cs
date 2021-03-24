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
    public class TypeOfPaymentController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ITypeOfPaymentRepository _typeOfPaymentRepository;

        public TypeOfPaymentController(ITypeOfPaymentRepository typeOfPaymentRepository)
        {
            _typeOfPaymentRepository = typeOfPaymentRepository;
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
                var Count = _typeOfPaymentRepository.GetAll().Count();

                var typesOfPayments = new List<TypeOfPaymentDto>();

                //Search query when sSearch is not empty
                if (sSearch != "" && sSearch != null) //If there is search query
                {

                    typesOfPayments = Mapper.Map<List<TypeOfPaymentDto>>(_typeOfPaymentRepository.GetAll()
                        .Where(a => a.Code.ToLower().Contains(sSearch.ToLower())
                                      || a.Id.ToString().ToLower().Contains(sSearch.ToLower())
                                      || a.Title.ToString().ToLower().Contains(sSearch.ToLower())).ToList());

                    Count = typesOfPayments.Count();
                    // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                    typesOfPayments = SortFunction(iSortCol, sortOrder, typesOfPayments).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    //get data from database
                    typesOfPayments = Mapper.Map<List<TypeOfPaymentDto>>(_typeOfPaymentRepository.GetAll() //speficiy conditions if there is any using .Where(Condition)                             
                                       .ToList());

                    // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                    typesOfPayments = SortFunction(iSortCol, sortOrder, typesOfPayments).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var TypeOfPaymentsPaged = new SysDataTablePager<TypeOfPaymentDto>(typesOfPayments, Count, Count, sEcho);

                return Ok(TypeOfPaymentsPaged);
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
        private List<TypeOfPaymentDto> SortFunction(int iSortCol, string sortOrder, List<TypeOfPaymentDto> list)
        {
            if (iSortCol == 0 || iSortCol == 1 || iSortCol == 2)
            {
                switch (iSortCol)
                {
                    case 0:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Code).ToList();
                        else
                            list = list.OrderBy(c => c.Code).ToList();
                        break;
                    case 1:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Title).ToList();
                        else
                            list = list.OrderBy(c => c.Title).ToList();
                        break;
                    case 2:
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
