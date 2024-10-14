using Microsoft.EntityFrameworkCore;
using Todos.Common.Enums;
using Todos.EF.Context;
using Todos.EF.Entities;
using Todos.Models;
using Todos.Models.Get;

namespace Todos.Repositories;

public interface ITodosRepository
{
    Task<List<Note>> ListNotes(string userId, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, NoteType? noteType);
    Task<Note?> GetNote(string userId, long id);
    Task<long> AddNote(string userId, Note note);
    Task EditNote(string userId, Note note);
    Task DeleteNote(string userId, int noteId);
}

public class TodosRepository : ITodosRepository
{
    private readonly TodosDbContext _context;
    
    public TodosRepository(TodosDbContext context)
    {
        _context = context;
    }

    public async Task<List<Note>> ListNotes(string userId, DateTimeOffset? dateFrom, DateTimeOffset? dateTo,
        NoteType? noteType)
    {
        var query = _context.Set<NoteEntity>()
            .Where(x => x.UserId == userId);

        if (dateFrom != null) query = query.Where(x => x.CreatedOn >= dateFrom);

        if (dateTo != null) query = query.Where(x => x.CreatedOn < dateTo.Value.AddDays(1));

        if (noteType != null) query = query.Where(x => x.NoteType.Id == noteType);

        return await query
            .Select(x => new Note
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                ModifiedOn = x.ModifiedOn,
                NoteType = new NoteTypeModel
                {
                    Id = x.NoteType.Id,
                    Name = x.NoteType.Name
                },
                NoteStatus = new NoteStatusModel
                {
                    Id = x.NoteStatus.Id,
                    Name = x.NoteStatus.Name
                },
                UserId = x.UserId,
                Items = x.Items
                    .Select(i => new Item
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsCompleted = i.IsCompleted,
                        NoteId = i.NoteId
                    })
                    .ToList()
            })
            .ToListAsync();
    }

    public Task<Note?> GetNote(string userId, long id)
    {
        return _context.Set<NoteEntity>()
            .Where(x => x.UserId == userId && x.Id == id)
            .Select(x => new Note
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                ModifiedOn = x.ModifiedOn,
                NoteType = new NoteTypeModel
                {
                    Id = x.NoteType.Id,
                    Name = x.NoteType.Name
                },
                NoteStatus = new NoteStatusModel
                {
                    Id = x.NoteStatus.Id,
                    Name = x.NoteStatus.Name
                },
                UserId = x.UserId,
                Items = x.Items
                    .Select(i => new Item
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsCompleted = i.IsCompleted,
                        NoteId = i.NoteId
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<long> AddNote(string userId, Note note)
    {
        var entity = await _context.Set<NoteEntity>()
            .AddAsync(new NoteEntity
            {
                Title = note.Title,
                Description = note.Description,
                CreatedOn = DateTimeOffset.Now,
                ModifiedOn = null,
                NoteTypeId = note.NoteType.Id,
                NoteStatusId = note.NoteStatus.Id,
                UserId = userId,
                Items = note.Items
                    .Select(item => new ItemEntity
                    {
                        Description = item.Description,
                        IsCompleted = item.IsCompleted
                    })
                    .ToList()
            });

        await _context.SaveChangesAsync();

        return entity.Entity.Id;
    }
    
    public async Task EditNote(string userId, Note note)
    {
        var noteEntity = await _context.Set<NoteEntity>()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == note.Id);

        if (noteEntity is null) throw new NotSupportedException();

        await _context.Set<ItemEntity>()
            .Where(x => x.NoteId == note.Id)
            .ExecuteDeleteAsync();

        noteEntity.Title = note.Title;
        noteEntity.Description = note.Description;
        noteEntity.NoteTypeId = note.NoteType.Id;
        noteEntity.NoteStatusId = note.NoteStatus.Id;
        noteEntity.Items = note.Items
            .Select(item => new ItemEntity
            {
                Description = item.Description,
                IsCompleted = item.IsCompleted
            })
            .ToList();
        noteEntity.ModifiedOn = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteNote(string userId, int noteId)
    {
        var noteEntity = await _context.Set<NoteEntity>()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == noteId);

        if (noteEntity is null) throw new NotSupportedException();

        await _context.Set<NoteEntity>()
            .Where(x => x.Id == noteId)
            .ExecuteDeleteAsync();

        await _context.SaveChangesAsync();
    }
}