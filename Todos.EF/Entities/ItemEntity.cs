namespace Todos.EF.Entities;

public class ItemEntity
{
    public long Id { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public long NoteId { get; set; }

    public NoteEntity Note { get; set; }
}