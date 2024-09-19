namespace DriveSafe.Presentation;

public class UserNotException : Exception
{
    public UserNotException() { }
    
    public UserNotException(string message) : base(message) { }

    public UserNotException(string message, Exception inner) : base(message, inner) { }
}