using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            return Ok(await _categoryService.GetCategoryAll());
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStatusAsync(int id)
        {
            return Ok(await _categoryService.UpdateStatus(id));
        }

        [HttpGet("get-name-category-by-id")]
        public async Task<IActionResult> GetCategoryNameByIdAsync(int id)
        {
            IActionResult actionResult = null;
            await Task.Factory.StartNew(() =>
            {
                actionResult = Ok(_categoryService.GetCategoryNameById(id));
            });

            return actionResult;
        }
    }



}
