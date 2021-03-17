using DisplayData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace DisplayData.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        private readonly ILogger<HomeController> _logger;
        private List<Employee> EmployeeList { get; set; }

        public HomeController(ILogger<HomeController> logger,IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult DisplayData()
        {
            if (System.IO.File.Exists(@"" + _config.GetValue<string>("OutputfileLocation:Location") + ""))
            {
                using (StreamReader file = System.IO.File.OpenText(@""+ _config.GetValue<string>("OutputfileLocation:Location") + ""))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    EmployeeList = (List<Employee>)serializer.Deserialize(file, typeof(List<Employee>));
                }
            }
            return View(EmployeeList);
        }
            public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
