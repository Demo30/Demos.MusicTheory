namespace Demos.MusicTheory.Services;

internal static class ServicesManager
{
    internal static readonly ServicesProvider ServicesProvider = new();
    
    public static T GetService<T>() => (T)ServicesProvider.Services[typeof(T)]();
    
    public static object GetService(Type serviceType) => ServicesProvider.Services[serviceType]();

}