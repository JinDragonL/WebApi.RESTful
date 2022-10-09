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
            return await _categoryRepository.GetAll();
        }

    }
}
