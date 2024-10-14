namespace Todos.EF.Extensions;

public static class EfExtensions
{
    private const string Entity = "Entity";

    public static string TableName(this string entityName)
    {
        if (entityName.EndsWith(Entity)) return entityName[..^Entity.Length];

        return entityName;
    }
}