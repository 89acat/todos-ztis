using Todos.Common.Enums;

namespace Todos.EF.Entities;

public class NoteTypeEntity
{
    public NoteType Id { get; set; }
    public string Name { get; set; }
}