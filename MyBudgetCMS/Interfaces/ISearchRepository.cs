using MyBudgetCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetCMS.Interfaces
{
    public interface ISearchRepository
    {
        List<Search> Search(string searchStr);
    }
}
