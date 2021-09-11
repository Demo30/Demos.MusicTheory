using System;
namespace Demos.MusicTheory
{
    public class ChromaticFullyQualifiedRange
    {
        public ChromaticNote ChromaticNoteStart { get; }
        public ChromaticNote ChromaticNoteEnd { get; }

        public ChromaticFullyQualifiedRange(ChromaticNote start, ChromaticNote end)
        {
            ChromaticNoteStart = start;
            ChromaticNoteEnd = end;
        }
    }
}
