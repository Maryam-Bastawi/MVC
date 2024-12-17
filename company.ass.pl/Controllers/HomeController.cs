using company.ass.pl.Models;
using company.ass.pl.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace company.ass.pl.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Itransientservices itransient01;
        private readonly Itransientservices itransient02;

        public Iscopedservices Scoped01 { get; }
        public Iscopedservices Scoped02 { get; }
        public Isengletionservices Sengletion01 { get; }
        public Isengletionservices Sengletion02 { get; }

        public HomeController(ILogger<HomeController> logger,
            Iscopedservices scoped01,
            Iscopedservices scoped02, 
            Itransientservices Itransient01,
            Itransientservices Itransient02,
            Isengletionservices sengletion01,
            Isengletionservices sengletion02
            )
        {
            _logger = logger;
            Scoped01 = scoped01;
            Scoped02 = scoped02;
            itransient01 = Itransient01;
            itransient02 = Itransient02;
            Sengletion01 = sengletion01;
            Sengletion02 = sengletion02;
        }
       // url//Home/testlifetime
        public string testlifetime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Scoped01 : : {Scoped01.GetGuid()}\n");
            builder.Append($"Scoped02 : : {Scoped02.GetGuid()}\n\n");
            builder.Append($"itransient01 : : {itransient01.GetGuid()}\n");
            builder.Append($"itransient02 : : {itransient02.GetGuid()}\n\n");
            builder.Append($"Sengletion01 : : {Sengletion01.GetGuid()}\n");
            builder.Append($"Sengletion02 : : {Sengletion02.GetGuid()}\n\n");
            return builder.ToString();

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
