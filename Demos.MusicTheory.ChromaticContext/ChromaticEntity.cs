using Demos.MusicTheory.Abstractions.Commons;

namespace Demos.MusicTheory.ChromaticContext;

public class ChromaticEntity : IContentEqual<ChromaticEntity>, IChromaticEntity
{
    public int ChromaticContextIndex { get; }

    public ChromaticEntity(int chromaticContextIndex)
    {
        ChromaticContextIndex = chromaticContextIndex;
    }

    public virtual bool IsEqualByContent(ChromaticEntity comparedObject)
    {
        return ChromaticContextIndex.Equals(comparedObject?.ChromaticContextIndex);
    }
}