using Sample.WebApiRestful.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.WebApiRestful.Service
{
    public interface ICategoryService
    {
        Task<List<Categories>> GetCategoryAll();
        string GetCategoryNameById(int id);
        Task<bool> UpdateStatus(int id);
    }
}