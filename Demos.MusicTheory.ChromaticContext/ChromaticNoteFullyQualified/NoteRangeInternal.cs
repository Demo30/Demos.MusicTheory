using System;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

internal class NoteRangeInternal
{
    public NoteInternal NoteInternalStart { get; }
    public NoteInternal NoteInternalEnd { get; }

    public int ChromaticIndexSpan =>
        Math.Abs(NoteInternalStart.ChromaticContextIndex - NoteInternalEnd.ChromaticContextIndex);

    public NoteRangeInternal(NoteInternal internalStart, NoteInternal internalEnd)
    {
        NoteInternalStart = internalStart;
        NoteInternalEnd = internalEnd;
    }

    public override string ToString() => $"{NoteInternalStart}-{NoteInternalEnd}";
}