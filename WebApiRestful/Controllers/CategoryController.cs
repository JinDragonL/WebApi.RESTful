using Alachisoft.NCache.Common.DataStructures.Clustered;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]  //api/category
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
            throw new System.ArgumentNullException();

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
            return Ok(await _categoryService.GetCategoryNameByIdAsync(id));
        }

        [HttpGet("get-categories-with-cancel")]
        public async Task<IActionResult> GetAllCategoryWithCancel(CancellationToken cancellation)
        {
            return Ok(await _categoryService.GetCategories(cancellation));
        }

    }

    //CancellationToken
    //sync = dong bo
    // 1, 2, 3

    //.......................

    //async = bat dong bo
    // 1, 2, 3
}
