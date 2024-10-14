namespace Todos.Common.Extensions;

public static class EnumExtensions
{
    public static List<T> GetValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }
}