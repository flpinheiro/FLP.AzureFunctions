namespace FLP.Core.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
    public NotFoundException() : this("The requested item was not found.")
    {
    }
}
