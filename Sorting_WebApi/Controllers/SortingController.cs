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
        public Dictionary<string, int> GetWordCounts([FromBody] string text)
        {
            Dictionary<string, int> wordCounts = Library.GetWordCountsParallel(text);
            return wordCounts;
            
        }
    }
}
