using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Models;

namespace SAcademy.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
