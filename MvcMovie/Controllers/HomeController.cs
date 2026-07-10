using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using System.Diagnostics;

using MvcMovie.Data;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MvcMovieContext _context;

    public HomeController(ILogger<HomeController> logger, MvcMovieContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(string id)
    {
        if (_context.Movie == null)
        {
            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        }

        var movies = from m in _context.Movie
                     select m;

        if (!String.IsNullOrEmpty(id))
        {
            movies = movies.Where(s => s.Title!.ToUpper().Contains(id.ToUpper()));
        }

        return View(await movies.ToListAsync());
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
