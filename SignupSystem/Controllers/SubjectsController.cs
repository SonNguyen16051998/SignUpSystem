using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
