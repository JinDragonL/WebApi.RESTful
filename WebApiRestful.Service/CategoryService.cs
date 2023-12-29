using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Restful.Core.Abstract;
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

            return true;
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

        public async Task<List<string>> GetCategories(CancellationToken cancellation)
        {
            List<string> ls = new();
            try
            {
                var categories = await _categoryRepository.GetAllAsync();

                foreach (var category in categories)
                {
                    if (cancellation.IsCancellationRequested) //CancellationTokenSource
                        cancellation.ThrowIfCancellationRequested();

                    ls.Add(category.Name);

                    Thread.Sleep(3000);
                }
            }
            catch (OperationCanceledException ex)
            {
                //record log
                string log = ex.Message;
                throw;
            }

            return ls;
        }


    }
}
