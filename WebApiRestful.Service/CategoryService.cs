using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Restful.Core.Cache;
using WebApiRestful.Data.Abstract;
using WebApiRestful.Domain.Entities;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Service
{
    public class CategoryService : ICategoryService
    {
        IRepository<Categories> _categoryRepository;
        IDistributedCacheService _distributedCacheService;

        public CategoryService(IRepository<Categories> categoryRepository, IDistributedCacheService distributedCacheService)
        {
            _categoryRepository = categoryRepository;
            _distributedCacheService = distributedCacheService;
        }

        public async Task<List<Categories>> GetCategoryAll()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<bool> UpdateStatus(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            category.IsActive = false;

            await _categoryRepository.CommitAsync();

            return await Task.FromResult(true);
        }

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            string resultCache = await _distributedCacheService.Get<string>($"cache_category_{id}");

            if (!string.IsNullOrEmpty(resultCache))
            {
                return resultCache;
            }

            string name = "Coca cola";

            await _distributedCacheService.Set($"cache_category_{id}", name);

            return name;
        }

        //Task.FromResult
        //Task.Factory.StartNew
    }
}
