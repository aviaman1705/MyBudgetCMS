using MyBudgetCMS.Interfaces;
using MyBudgetCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Services
{
    public class SearchRepository : ISearchRepository
    {
        private MyBudgetDB context = new MyBudgetDB();

        public List<Search> Search(string searchStr)
        {
            var searchStrParameter = new SqlParameter("@SearchStr", searchStr);
            var result = context.Database.SqlQuery<Search>("Search @SearchStr", searchStrParameter).ToList();
            return result;
        }
    }
}