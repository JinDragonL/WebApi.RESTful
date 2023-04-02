using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiRestful.Domain.Entities;

namespace WebApiRestful.Service.Abstract
{
    public interface ICategoryService
    {
        Task<List<string>> GetCategories(CancellationToken cancellation);
        Task<List<Categories>> GetCategoryAll();
        Task<string> GetCategoryNameByIdAsync(int id);
        Task<bool> UpdateStatus(int id);
    }
}