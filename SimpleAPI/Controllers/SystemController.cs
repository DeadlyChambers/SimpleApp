using Microsoft.AspNetCore.Mvc;
using SimpleAPI.DataAccess.Configuration;
using System;
using System.Threading.Tasks;

namespace SimpleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        [HttpGet]
        public SystemObj Get()
        {
            var assembly = this.GetType().Assembly;
            return new SystemObj
            {
                Project = assembly.GetName().Name,
                Date = DateTime.Now.ToString("G"),
                Version = StartupDb.AppVersion
            };
        }
    }

    public class SystemObj
    {
        public string Project { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
    }
}
