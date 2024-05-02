namespace ParseDontValidate;

public class InvalidOfferId: ArgumentException
{
    public InvalidOfferId(string? message, string? paramName) : base(message, paramName)
    {
    }
}
