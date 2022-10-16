using Sample.WebApiRestful.Data.Abstract;
using Sample.WebApiRestful.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.WebApiRestful.Service
{
    public class CategoryService : ICategoryService
    {
        IRepository<Categories> _categoryRepository;

        public CategoryService(IRepository<Categories> categoryRepository)
        {
            _categoryRepository = categoryRepository;
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

        public string GetCategoryNameById(int id)
        {
            return "Candy";
        }

        //Task.FromResult
        //Task.Factory.StartNew
    }
}
