using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Abstractions.Commons;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticEntity : IContentEqual<ChromaticEntity>, IChromaticEntity
    {
        public int ChromaticContextIndex { get; }

        public ChromaticEntity(int chromaticContextIndex)
        {
            ChromaticContextIndex = chromaticContextIndex;
        }

        public virtual bool IsEqualByContent(ChromaticEntity comparedObject) => 
            ChromaticContextIndex.Equals(comparedObject?.ChromaticContextIndex);
    }
}
