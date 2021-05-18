using System.Net;
using System.Threading.Tasks;
using Manageme.Extensions;
using Manageme.Forms;
using Manageme.Services;
using Manageme.Services.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Manageme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [Authorize]
        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "User Not Found", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.Conflict, "Category already defined", typeof(ErrorResult))]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryForm form)
        {
            return this.FromServiceResult(
                await _categoryService.AddCategoryAsync(this.GetRequestUserId(), form)
            );
        }

        [Authorize]
        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "User Not Found", typeof(ErrorResult))]
        public async Task<IActionResult> GetCategories()
        {
            return this.FromServiceResult(
                await _categoryService.GetCategoriesAsync(this.GetRequestUserId())
            );
        }

        [Authorize]
        [HttpDelete("{categoryId}")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "Category Not Found", typeof(ErrorResult))]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            return this.FromServiceResult(
                await _categoryService.DeleteCategoryAsync(this.GetRequestUserId(), categoryId)
            );
        }
    }
}
