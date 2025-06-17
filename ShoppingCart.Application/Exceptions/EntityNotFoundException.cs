namespace ShoppingCart.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public string EntityName { get; }
    public object Key { get; }

    public EntityNotFoundException(string entityName, object key)
        : base($"{entityName} con identificador '{key}' no fue encontrado")
    {
        EntityName = entityName;
        Key = key;
    }

    public EntityNotFoundException(string message) : base(message)
    {
        EntityName = string.Empty;
        Key = string.Empty;
    }
}