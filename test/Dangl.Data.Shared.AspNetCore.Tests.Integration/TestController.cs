using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dangl.Data.Shared.AspNetCore.Tests.Integration
{
    public class TestController : Controller
    {
        [HttpPost("RequiredFormFile")]
        public IActionResult RequiredFormFile([Required]IFormFile formFile)
        {
            return Ok();
        }

        [HttpPost("MultipleRequiredFormFile")]
        public IActionResult MultipleRequiredFormFile([Required]IFormFile formFile, [Required]IFormFile otherFormFile)
        {
            return Ok();
        }

        [HttpPost("FormFile")]
        public IActionResult FormFile(IFormFile formFile)
        {
            return Ok();
        }

        [HttpPost("Model")]
        public IActionResult Model([FromBody]ModelWithoutRequirement model)
        {
            return Ok();
        }

        [HttpPost("ModelWithBiggerThanZeroAttribute")]
        public IActionResult ModelWithBiggerThanZeroAttribute([FromBody]ModelWithRequirement model)
        {
            return Ok();
        }

        [HttpGet("JsonData")]
        public IActionResult JsonData()
        {
            return Ok(new
            {
                Value = "Some Data"
            });
        }
    }
}
