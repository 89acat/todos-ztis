using System.ComponentModel.DataAnnotations;
using Todos.Models.Get;

namespace Todos.Models;

public class Note
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [DataType(DataType.Date)] public DateTimeOffset CreatedOn { get; set; }
    [DataType(DataType.Date)] public DateTimeOffset? ModifiedOn { get; set; }

    public NoteTypeModel NoteType { get; set; } = new();
    public NoteStatusModel NoteStatus { get; set; } = new();

    public string UserId { get; set; }

    public List<Item> Items { get; set; } = new();
    public int FinishedItems => Items.Count(x => x.IsCompleted);
    public int AllItems => Items.Count;
    public int UnfinishedItems => AllItems - FinishedItems;

    // Colors of Note container
    public string TextColor => NoteType?.Id switch
    {
        Common.Enums.NoteType.Work => "text-blue-500",
        Common.Enums.NoteType.School => "text-amber-500",
        Common.Enums.NoteType.Home => "text-emerald-500",
        Common.Enums.NoteType.Personal => "text-violet-500",
        Common.Enums.NoteType.Other => "text-pink-500",
        _ => ""
    };

    public string BackgroundColor => NoteType?.Id switch
    {
        Common.Enums.NoteType.Work => "bg-blue-200",
        Common.Enums.NoteType.School => "bg-amber-100",
        Common.Enums.NoteType.Home => "bg-emerald-200",
        Common.Enums.NoteType.Personal => "bg-violet-200",
        Common.Enums.NoteType.Other => "bg-pink-200",
        _ => ""
    };

    // Styles
    public string IsHidden => ModifiedOn is null ? "hidden" : "";

    public string CheckedNew => NoteStatus.Id == Common.Enums.NoteStatus.New ? "checked" : "";
    public string CheckedActive => NoteStatus.Id == Common.Enums.NoteStatus.Active ? "checked" : "";
    public string CheckedCompleted => NoteStatus.Id == Common.Enums.NoteStatus.Completed ? "checked" : "";
}