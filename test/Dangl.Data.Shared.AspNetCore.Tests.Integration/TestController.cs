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

        [HttpPost("RequiredFormFileInModel")]
        public IActionResult RequiredFormFileInModel([Required]FormFileUploadModel model)
        {
            if (model?.formFile == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        public class FormFileUploadModel
        {
            [Required]
            public IFormFile formFile { get; set; }
        }

        [HttpPost("FormFile")]
        public IActionResult FormFile(IFormFile formFile)
        {
            return Ok();
        }

        [HttpPost("Model")]
        public IActionResult Model([FromBody]ModelWithoutRequirement model)
        {
            return Ok(model);
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

        [HttpGet("NoCacheNoTransform")]
        [CdnNoCache]
        public IActionResult NoCacheNoTransform()
        {
            return Ok(new
            {
                Value = "Some Data"
            });
        }
    }
}
