using AutoMapper;
using MyBudgetCMS.Infrastructure;
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
    public class MonthlyBudgetController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IMonthlyBudgetRepository _monthlyBudgetRepository;

        public MonthlyBudgetController(IMonthlyBudgetRepository monthlyBudgetRepository)
        {
            _monthlyBudgetRepository = monthlyBudgetRepository;
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
                var Count = 0;

                var MonthlyBudgets = new List<MonthlyBudgetGridItemDto>();

                //Search query when sSearch is not empty
                if (sSearch != "" && sSearch != null) //If there is search query
                {
                    if (MemoryCacher.GetValue(Constant.MonthlyBudgetList) != null)
                    {
                        //Get the list from cache
                        MonthlyBudgets = (List<MonthlyBudgetGridItemDto>)MemoryCacher.GetValue(Constant.MonthlyBudgetList);
                    }
                    else
                    {
                        MonthlyBudgets = Mapper.Map<List<MonthlyBudgetGridItemDto>>(_monthlyBudgetRepository.GetAll()
                            .Where(a => a.Date.ToString().ToLower().Contains(sSearch.ToLower())
                                          || a.Budget.ToString().ToLower().Contains(sSearch.ToLower())
                                          || a.Id.ToString().ToLower().Contains(sSearch.ToLower()))
                                          .ToList());

                        Count = MonthlyBudgets.Count();
                        // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                        MonthlyBudgets = SortFunction(iSortCol, sortOrder, MonthlyBudgets).Skip(iDisplayStart).Take(iDisplayLength).ToList();

                        MemoryCacher.Add(Constant.MonthlyBudgetList, MonthlyBudgets, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = MonthlyBudgets.Count();
                }
                else
                {
                    if (MemoryCacher.GetValue(Constant.MonthlyBudgetList) != null)
                    {
                        //Get the list from cache
                        MonthlyBudgets = (List<MonthlyBudgetGridItemDto>)MemoryCacher.GetValue(Constant.MonthlyBudgetList);
                    }

                    else
                    {
                        //get data from database
                        MonthlyBudgets = Mapper.Map<List<MonthlyBudgetGridItemDto>>(_monthlyBudgetRepository.GetAll().ToList());
                        MemoryCacher.Add(Constant.MonthlyBudgetList, MonthlyBudgets, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                    }

                    Count = MonthlyBudgets.Count();

                    // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                    MonthlyBudgets = SortFunction(iSortCol, sortOrder, MonthlyBudgets).Skip(iDisplayStart).Take(iDisplayLength).ToList();                    
                }

                var MonthlyBudgetsPaged = new SysDataTablePager<MonthlyBudgetGridItemDto>(MonthlyBudgets, Count, Count, sEcho);

                return Ok(MonthlyBudgetsPaged);
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
        private List<MonthlyBudgetGridItemDto> SortFunction(int iSortCol, string sortOrder, List<MonthlyBudgetGridItemDto> list)
        {
            if (iSortCol == 2 || iSortCol == 3 || iSortCol == 4)
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
                            list = list.OrderByDescending(c => c.Budget).ToList();
                        else
                            list = list.OrderBy(c => c.Budget).ToList();
                        break;
                    case 4:
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
