namespace Demos.MusicTheory.Services;

internal class ServicesProvider
{
    internal readonly Dictionary<Type, Func<object>> Services = new();
}