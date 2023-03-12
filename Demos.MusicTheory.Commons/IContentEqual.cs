namespace Demos.MusicTheory.Commons;

public interface IContentEqual<in T>
{
    bool IsEqualByContent(T comparedObject);
}