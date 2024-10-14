using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todos.Common.Enums;
using Todos.Models;
using Todos.Repositories;

namespace Todos.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITodosRepository _repository;

    public HomeController(ITodosRepository repository,
        ILogger<HomeController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IActionResult> Index(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, NoteType? noteType)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var notes = await _repository.ListNotes(userId, dateFrom, dateTo, noteType);
        var model = new NoteList
        {
            Days = notes.GroupBy(x => x.CreatedOn.Date)
                .OrderByDescending(x => x.Key)
                .Select(x => new NoteDay
                {
                    Date = x.Key,
                    Month = (Month)x.Key.Month,
                    Notes = x.ToList()
                })
                .ToList()
        };

        if (model.Days.All(x => x.Date != DateTimeOffset.Now.Date))
            model.Days.Insert(0, new NoteDay
            {
                Date = DateTimeOffset.Now,
                Month = (Month)DateTimeOffset.Now.Month
            });

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}