using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

namespace Demos.MusicTheory.ChromaticContext.Providers;

internal interface IDiatonicDegreeFromNoteRangeProvider
{
    public int GetDiatonicDegreeSpan(NoteRangeInternal rangeInternal);
}