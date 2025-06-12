using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMartApplication.Services;

namespace OMAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategorieController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CategorieController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet("GetCategorieByCategoriId/{category_id}")]
        public async Task<IActionResult> GetCategorieByCategoriId(int category_id)
        {
           try
            {
               
                var result = await categoriesService.GetCategoryByCategoriId(category_id);

                if (result == null)
                {
                    return NotFound($"No Categories found for user with ID {category_id}.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred while fetching Categories for user {category_id}: {ex.Message}");
            }
        }
    }
}