using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Thinktecture.AKS.Sample.Controllers
{
    [Route("demo")]
    public class DemoController : Controller
    {

        [HttpPost]
        [Route("files")]
        public async Task<IActionResult> CreateFileAsync()
        {
            var targetFolder = Path.Join("/var", "tt");
            if (!Directory.Exists(targetFolder))
            {
                return StatusCode(500);
            }
            return await Task.Run(() =>
            {
                try
                {
                    System.IO.File.WriteAllText(Path.Combine(targetFolder, Path.GetRandomFileName()), $"Hello, at {DateTime.Now} from {Dns.GetHostName()}.");
                    return StatusCode(200);
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }

            });
        }

        [HttpGet]
        [Route("custom")]
        public async Task<IActionResult> GetCustomFilesAsync()
        {
            var targetFolder = Path.Join("/var", "tt");
            if (!Directory.Exists(targetFolder))
            {
                return StatusCode(500);
            }

            return await Task.Run(() =>
            {
                var files = Directory.GetFiles(targetFolder);
                var response = files
                    .ToList()
                    .Select(file =>
                    {
                        var contents = System.IO.File.ReadAllText(file);
                        return new
                        {
                            File = file,
                            Contents = contents
                        };
                    });
                return Ok(response);
            });
        }
    }
}
