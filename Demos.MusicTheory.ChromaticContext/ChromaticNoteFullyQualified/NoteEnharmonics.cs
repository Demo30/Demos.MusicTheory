using System;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

public class NoteEnharmonics : ChromaticEntity
{
    public Note[] Cluster { get; }

    public NoteEnharmonics(Note[] enharmonicNotes) : base(GetChromaticContextIndex(enharmonicNotes))
    {
        if ((enharmonicNotes?.Length ?? 0) == 0)
            throw new ArgumentNullException(nameof(enharmonicNotes));

        CheckEnharmonicNoteCompatibility(enharmonicNotes);
        Cluster = enharmonicNotes;
    }

    private static int GetChromaticContextIndex(Note[] enharmonicNotes)
    {
        return enharmonicNotes?.Length > 0 ? enharmonicNotes.First().ChromaticContextIndex : -1;
    }

    private static void CheckEnharmonicNoteCompatibility(Note[] enharmonicNotes)
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