using Todos.Common.Enums;

namespace Todos.Models;

public class NoteList
{
    public List<NoteDay> Days { get; set; } = new();
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public NoteType? NoteType { get; set; }
}

public class NoteDay
{
    public DateTimeOffset Date { get; set; }
    public Month Month { get; set; }
    public List<Note> Notes { get; set; } = new();
}