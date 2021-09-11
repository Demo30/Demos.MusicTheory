namespace Demos.MusicTheory.Commons
{
    public interface IContentEqual<T>
    {
        bool IsEqualByContent(T comparedObject);
    }
}
