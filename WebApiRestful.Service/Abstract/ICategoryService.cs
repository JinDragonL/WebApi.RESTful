using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Service.Abstract
{
    public interface ICategoryService
    {
        Task<List<Categories>> GetCategoryAll();
        string GetCategoryNameById(int id);
        Task<bool> UpdateStatus(int id);
    }
}