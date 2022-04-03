namespace Demos.MusicTheory.Services;

public class ServiceInitializationException : Exception
{
    public ServiceInitializationException() : base("Service initialization internal error.")
    {
        
    }
}