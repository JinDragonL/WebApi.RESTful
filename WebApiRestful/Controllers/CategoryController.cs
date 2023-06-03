using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Controllers
{
    [Authorize]
    [Route("api/[controller]")]  //api/category
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        [HttpGet]
        [Produces("text/json")]
        public async Task<IActionResult> GetAllCategory()
        {
            //throw new System.ArgumentNullException();
            var result = await _categoryService.GetCategoryAll();

            return Ok(result);
        }


        /// <summary>
        /// Update category with Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatusAsync(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

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
}
