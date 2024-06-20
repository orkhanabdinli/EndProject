namespace EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;

public class UserProfileMediaNotFoundException : Exception
{
    public int StatusCode { get; set; }
    public UserProfileMediaNotFoundException()
    {
    }
    public UserProfileMediaNotFoundException(string message) : base(message)
    {
    }
    public UserProfileMediaNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
