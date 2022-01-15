namespace Demos.MusicTheory.Abstractions.Commons;

public interface IContentEqual<in T>
{
    bool IsEqualByContent(T comparedObject);
}