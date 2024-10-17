using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todos.Common.Enums;
using Todos.Models;
using Todos.Repositories;

namespace Todos.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IUsersRepository _usersRepository;
    private readonly ITodosRepository _todosRepository;
    
    public ProfileController(IUsersRepository usersRepository,
        ITodosRepository todosRepository,
        ILogger<ProfileController> logger)
    {
        _usersRepository = usersRepository;
        _todosRepository = todosRepository;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> Show()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _usersRepository.GetUser(userId);
        var notes = await _todosRepository.ListNotes(userId, null, null, null);

        var model = new User
        {
            Id = user.Id,
            Email = user.Email,
            NumberOfTodos = notes.Count,
            PercentOfCompleted = notes.Count > 0
                ? notes.Count(x => x.NoteStatus.Id == NoteStatus.Completed) / (double)notes.Count * 100
                : 0
        };
        
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _usersRepository.GetUser(userId);
        return View(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            ModelState.AddModelError("Email", "Email is required.");
            return View(user);
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _usersRepository.EditUser(userId, user);
        return Redirect("/profile/show");
    }
}