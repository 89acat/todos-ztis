namespace Todos.Models.Get;

public class Item
{
    public long Id { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public long? NoteId { get; set; }

    // Styles for completed tasks
    public string TitleCompleted => IsCompleted ? "line-through text-gray-400" : "";
    public string CheckCompleted => IsCompleted ? "text-emerald-400" : "text-gray-400";
    public string CheckEditCompleted => IsCompleted ? "checked" : "";
}