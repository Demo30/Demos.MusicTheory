using System;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

internal class NoteEnharmonicsInternal : ChromaticEntity
{
    public NoteInternal[] Notes { get; }

    public NoteEnharmonicsInternal(NoteInternal[] enharmonicNotes) : base(GetChromaticContextIndex(enharmonicNotes))
    {
        if ((enharmonicNotes?.Length ?? 0) == 0)
            throw new ArgumentNullException(nameof(enharmonicNotes));

        CheckEnharmonicNoteCompatibility(enharmonicNotes);
        Notes = enharmonicNotes;
    }

    private static int GetChromaticContextIndex(NoteInternal[] enharmonicNotes)
    {
        return enharmonicNotes?.Length > 0 ? enharmonicNotes.First().ChromaticContextIndex : -1;
    }

    private static void CheckEnharmonicNoteCompatibility(NoteInternal[] enharmonicNotes)
    {
        var valid = enharmonicNotes
            .Select(en => en.ChromaticContextIndex)
            .Distinct()
            .Count() == 1;

        if (!valid)
            throw new ArgumentException(
                "Supplied collection of chromatic notes cannot form an enharmonic cluster due to mismatching chromatic context index.");
    }
}