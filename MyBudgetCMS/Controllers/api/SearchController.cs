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
    public class SearchController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public IHttpActionResult GetData(string sEcho, string sSearch, int iDisplayStart, int iDisplayLength, string iSortCol_0, string sSortDir_0)
        {

            try
            {
                //iSortCol gives your Column numebr of for which sorting is required
                int iSortCol = Convert.ToInt32(iSortCol_0);

                //provides your sort order (asc/desc)
                string sortOrder = sSortDir_0;

                var SearchItems = new List<SearchDto>();

                if(MemoryCacher.GetValue(Constant.SearchList) != null)
                {
                    SearchItems = (List<SearchDto>)MemoryCacher.GetValue(Constant.SearchList);
                }
                else
                {
                    SearchItems = Mapper.Map<List<SearchDto>>(_searchRepository.Search(sSearch));
                    MemoryCacher.Add(Constant.SearchList, SearchItems, DateTimeOffset.Now.AddMinutes(Constant.CacheTime));
                }

                //get total value count
                var Count = SearchItems.Count();

                // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                SearchItems = SortFunction(iSortCol, sortOrder, SearchItems).Skip(iDisplayStart).Take(iDisplayLength).ToList();

                var SearchPaged = new SysDataTablePager<SearchDto>(SearchItems, Count, Count, sEcho);

                return Ok(SearchPaged);
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
        private List<SearchDto> SortFunction(int iSortCol, string sortOrder, List<SearchDto> list)
        {
            if (iSortCol == 0 || iSortCol == 1 || iSortCol == 2 || iSortCol == 3)
            {
                switch (iSortCol)
                {
                    case 0:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Link).ToList();
                        else
                            list = list.OrderBy(c => c.Link).ToList();
                        break;
                    case 1:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Type).ToList();
                        else
                            list = list.OrderBy(c => c.Type).ToList();
                        break;
                    case 2:
                        if (sortOrder == "desc")
                            list = list.OrderByDescending(c => c.Title).ToList();
                        else
                            list = list.OrderBy(c => c.Title).ToList();
                        break;
                    case 3:
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
