using AutoMapper;
using MyBudgetCMS.Infrastructure;
using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Dto;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyBudgetCMS.Controllers.Api
{
    //[Authorize(Roles = "Admin")]
    public class CategoryController : ApiController
    {
        private ICategoryRepository _categoryRepository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [CacheFilter(TimeDuration = 100)]
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

                var Categories = new List<CategoryGridItemDto>();

                //Search query when sSearch is not empty
                if (sSearch != "" && sSearch != null) //If there is search query
                {
                    if (MemoryCacher.GetValue(Constant.CategoryList) != null)
                    {
                        //Get the list from cache
                        Categories = (List<CategoryGridItemDto>)MemoryCacher.GetValue(Constant.CategoryList);
                    }
                    else
                    {
                        Categories = Mapper.Map<List<CategoryGridItemDto>>(_categoryRepository.GetAll().Where(a => a.Title.ToLower().Contains(sSearch.ToLower())
                                          || a.Parent.Title.ToLower().Contains(sSearch.ToLower())
                                          || a.TypeOfPayment.Title.ToLower().Contains(sSearch.ToLower())
                                          )
                                          .ToList());

                        MemoryCacher.Add(Constant.CategoryList, Categories, DateTimeOffset.Now.AddMinutes(30));
                    }

                    Count = Categories.Count();
                    // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                    Categories = SortFunction(iSortCol, sortOrder, Categories).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }
                else
                {
                    if (MemoryCacher.GetValue(Constant.CategoryList) != null)
                    {
                        //Get the list from cache
                        Categories = (List<CategoryGridItemDto>)MemoryCacher.GetValue(Constant.CategoryList);
                    }
                    else
                    {
                        //get data from database
                        Categories = Mapper.Map<List<CategoryGridItemDto>>(_categoryRepository.GetAll().ToList());
                        MemoryCacher.Add(Constant.CategoryList, Categories, DateTimeOffset.Now.AddMinutes(30));
                    }

                    Count = Categories.Count();
                    // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                    Categories = SortFunction(iSortCol, sortOrder, Categories).Skip(iDisplayStart).Take(iDisplayLength).ToList();
                }

                var CategoriesPaged = new SysDataTablePager<CategoryGridItemDto>(Categories, Count, Count, sEcho);

                return Ok(CategoriesPaged);
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
        private List<CategoryGridItemDto> SortFunction(int iSortCol, string sortOrder, List<CategoryGridItemDto> list)
        {
            if (iSortCol == 2 || iSortCol == 3 || iSortCol == 4 || iSortCol == 5)
            {
                switch (iSortCol)
                {
                    case 2:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.TypeOfPaymentName).ToList();
                        else
                            list = list.OrderBy(c => c.TypeOfPaymentName).ToList();
                        break;
                    case 3:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.ParentName).ToList();
                        else
                            list = list.OrderBy(c => c.ParentName).ToList();
                        break;
                    case 4:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Title).ToList();
                        else
                            list = list.OrderBy(c => c.Title).ToList();
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
