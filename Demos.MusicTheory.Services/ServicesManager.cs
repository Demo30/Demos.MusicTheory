namespace Demos.MusicTheory.Services;

internal static class ServicesManager
{
    private static ServicesProvider? _servicesProvider;

    internal static ServicesProvider ServicesProvider
    {
        get
        {
            _servicesProvider ??= new ServicesProvider();
            return _servicesProvider;
        }
    }

    public static void ResetServiceProvider()
    {
        _servicesProvider = null;
    }

    public static T GetService<T>() => (T)ServicesProvider.Services[typeof(T)]();
    
    public static object GetService(Type serviceType) => ServicesProvider.Services[serviceType]();
    
}