namespace Todos.Models;

public class User
{
    public string Id { get; set; }
    public string? Email { get; set; }
    public int NumberOfTodos { get; set; }
    public double PercentOfCompleted { get; set; }
}