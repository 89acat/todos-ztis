using Todos.Common.Enums;

namespace Todos.EF.Entities;

public class NoteEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }
    public NoteType NoteTypeId { get; set; }
    public NoteStatus NoteStatusId { get; set; }
    public string UserId { get; set; }

    public NoteTypeEntity NoteType { get; set; }
    public NoteStatusEntity NoteStatus { get; set; }
    public List<ItemEntity> Items { get; set; } = new();
}