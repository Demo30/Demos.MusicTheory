using System;

namespace Demos.MusicTheory.ChromaticContext
{
    public class ChromaticNoteFullyQualifiedRange
    {
        public ChromaticNoteFullyQualified ChromaticNoteStart { get; }
        public ChromaticNoteFullyQualified ChromaticNoteEnd { get; }
        public int ChromaticIndexLength => 
            Math.Abs(ChromaticNoteStart.ChromaticContextIndex - ChromaticNoteEnd.ChromaticContextIndex);

        public ChromaticNoteFullyQualifiedRange(ChromaticNoteFullyQualified start, ChromaticNoteFullyQualified end)
        {
            ChromaticNoteStart = start;
            ChromaticNoteEnd = end;
        }
    }
}
