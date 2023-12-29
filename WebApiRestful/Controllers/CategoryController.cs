using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Restful.Core.Abstract;
using WebApiRestful.Service.Abstract;

namespace WebApiRestful.Controllers
{
    [Authorize]
    [Route("api/[controller]")]  //api/category
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        IEmailHelper _emailHelper;

        public CategoryController(ICategoryService categoryService, IEmailHelper emailHelper)
        {
            _categoryService = categoryService;
            _emailHelper = emailHelper;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        [HttpGet]
        [Produces("text/json")]
        public async Task<IActionResult> GetAllCategory(CancellationToken cancellationToken)
        {
            //throw new System.ArgumentNullException();
            var result = await _categoryService.GetCategoryAll();

            await _emailHelper.SendEmailAsync(cancellationToken, new Domain.Model.EmailRequest
            {
                To = "************",
                Subject = "This is test for sending mail from NetCore",
                Content = "No Content!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
            });

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
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "OnlyAdmin")]
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
