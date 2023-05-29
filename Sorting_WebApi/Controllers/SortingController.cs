using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace Sorting_WebApi.Controllers
{
    [Route("sorting")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        //Web-Api сервис
        [HttpPost, Route("post/wordcount")]
        public async Task<ActionResult<Dictionary<string, int>>> GetWordCountsParallel([FromBody]string filePath)
        {
            try
            {
                Dictionary<string, int> wordCounts = new Dictionary<string, int>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        Parallel.ForEach(Regex.Split(line.ToLower(), @"\W+"), word =>
                        {
                            if (!string.IsNullOrEmpty(word))
                            {
                                if (wordCounts.ContainsKey(word))
                                {
                                    wordCounts[word]++;
                                }
                                else
                                {
                                    wordCounts.Add(word, 1);
                                }
                            }
                        });
                    }
                }
                return wordCounts;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
