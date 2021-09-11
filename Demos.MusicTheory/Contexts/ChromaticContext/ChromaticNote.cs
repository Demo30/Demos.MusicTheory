using Demos.MusicTheory.Abstractions.ChromaticContext;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticNote : IChromaticNote
    {
        public int ChromaticContextIndex { get; }

        public ChromaticNote(int chromaticContextIndex)
        {
            ChromaticContextIndex = chromaticContextIndex;
        }
    }
}
