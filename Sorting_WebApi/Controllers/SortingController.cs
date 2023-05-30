using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using Sort_Library;
using Microsoft.AspNetCore.Components.Forms;

namespace Sorting_WebApi.Controllers
{
    [Route("sorting")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        //Web-Api сервис
        [HttpPost, Route("post/wordcount")]
        public ActionResult<Dictionary<string, int>> GetWordCountsParallel([FromBody]string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return BadRequest();

            var response = Library.GetWordCountsParallel(filePath);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }
    }
}
