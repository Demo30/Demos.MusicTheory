namespace Demos.MusicTheory.ChromaticContext
{
    public class ChromaticNoteFullyQualifiedRange
    {
        public ChromaticNoteFullyQualified ChromaticNoteStart { get; }
        public ChromaticNoteFullyQualified ChromaticNoteEnd { get; }

        public ChromaticNoteFullyQualifiedRange(ChromaticNoteFullyQualified start, ChromaticNoteFullyQualified end)
        {
            ChromaticNoteStart = start;
            ChromaticNoteEnd = end;
        }
    }
}
