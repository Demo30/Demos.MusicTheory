using System;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified
{
    public class ChromaticNoteFullyQualifiedRange
    {
        public ChromaticNoteFullyQualified ChromaticNoteStart { get; }
        public ChromaticNoteFullyQualified ChromaticNoteEnd { get; }
        public int ChromaticIndexSpan => 
            Math.Abs(ChromaticNoteStart.ChromaticContextIndex - ChromaticNoteEnd.ChromaticContextIndex);

        public ChromaticNoteFullyQualifiedRange(ChromaticNoteFullyQualified start, ChromaticNoteFullyQualified end)
        {
            ChromaticNoteStart = start;
            ChromaticNoteEnd = end;
        }
    }
}
