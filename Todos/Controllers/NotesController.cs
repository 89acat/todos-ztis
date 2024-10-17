using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todos.Models;
using Todos.Models.Get;
using Todos.Repositories;

namespace Todos.Controllers;

[Authorize]
public class NotesController : Controller
{
    private readonly ITodosRepository _repository;

    public NotesController(ITodosRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Show(long id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var note = await _repository.GetNote(userId, id);
        return View(note);
    }

    [HttpPost]
    public ActionResult RenderItem(Item item)
    {
        return PartialView("_ListItem", item);
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View(new Note());
    }

    [HttpPost]
    public async Task<IActionResult> Add(Note note)
    {
        if (string.IsNullOrEmpty(note.Title))
        {
            ModelState.AddModelError("Title", "Title is required.");
            return View(note);
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var noteId = await _repository.AddNote(userId, note);
        return Redirect($"/notes/show?id={noteId}");
    }


    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var note = await _repository.GetNote(userId, id);
        return View(note);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Note note)
    {
        if (string.IsNullOrEmpty(note.Title))
        {
            ModelState.AddModelError("Title", "Title is required.");
            return View(note);
        }
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _repository.EditNote(userId, note);
        return Redirect($"/notes/show?id={note.Id}");
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _repository.DeleteNote(userId, id);
        return Redirect("/");
    }
}