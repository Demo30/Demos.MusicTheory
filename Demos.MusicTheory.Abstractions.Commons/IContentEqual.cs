namespace Demos.MusicTheory.Abstractions.Commons
{
    public interface IContentEqual<T>
    {
        bool IsEqualByContent(T comparedObject);
    }
}
