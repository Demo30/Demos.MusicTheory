using System;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

internal class NoteRange
{
    public Note NoteStart { get; }
    public Note NoteEnd { get; }

    public int ChromaticIndexSpan =>
        Math.Abs(NoteStart.ChromaticContextIndex - NoteEnd.ChromaticContextIndex);

    public NoteRange(Note start, Note end)
    {
        NoteStart = start;
        NoteEnd = end;
    }
}