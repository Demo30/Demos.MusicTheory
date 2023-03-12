using System;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

internal class NoteRangeInternal
{
    public NoteInternal NoteInternalStart { get; }
    public NoteInternal NoteInternalEnd { get; }

    public int ChromaticIndexSpan =>
        Math.Abs(NoteInternalStart.ChromaticContextIndex - NoteInternalEnd.ChromaticContextIndex);

    public NoteRangeInternal GetNormalizedNoteRange()
    {
        var startNoteDiatonicDegree = (int) NoteInternalStart.QualityInternal;
        var endNoteDiatonicDegree = (int) NoteInternalEnd.QualityInternal;
        
        var shouldSwitch =
            NoteInternalStart > NoteInternalEnd ||
            (NoteInternalStart == NoteInternalEnd && NoteInternalStart.Order > NoteInternalEnd.Order) ||
            (NoteInternalStart == NoteInternalEnd && NoteInternalStart.Order == NoteInternalEnd.Order && startNoteDiatonicDegree > endNoteDiatonicDegree);

        return shouldSwitch
            ? new NoteRangeInternal(NoteInternalEnd, NoteInternalStart)
            : this;
    }

    public NoteRangeInternal(NoteInternal internalStart, NoteInternal internalEnd)
    {
        NoteInternalStart = internalStart;
        NoteInternalEnd = internalEnd;
    }

    public override string ToString() => $"{NoteInternalStart}-{NoteInternalEnd}";
}