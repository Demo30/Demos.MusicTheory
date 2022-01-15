using System;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

public class ChromaticNoteEnharmonicCluster : ChromaticEntity
{
    public ChromaticNoteFullyQualified[] Cluster { get; }

    public ChromaticNoteEnharmonicCluster(ChromaticNoteFullyQualified[] enharmonicNotes) : base(GetChromaticContextIndex(enharmonicNotes))
    {
        if ((enharmonicNotes?.Length ?? 0) == 0)
        {
            throw new ArgumentNullException(nameof(enharmonicNotes));   
        }

        CheckEnharmonicNoteCompatibility(enharmonicNotes);
        Cluster = enharmonicNotes;
    }

    private static int GetChromaticContextIndex(ChromaticNoteFullyQualified[] enharmonicNotes) =>
        enharmonicNotes?.Length > 0 ? enharmonicNotes.First().ChromaticContextIndex : -1;

    private void CheckEnharmonicNoteCompatibility(ChromaticNoteFullyQualified[] enharmonicNotes)
    {
        var valid = enharmonicNotes
            .Select(en => en.ChromaticContextIndex)
            .Distinct()
            .Count() == 1;

        if (!valid)
        {
            throw new ArgumentException("Supplied collection of chromatic notes cannot form an enharmonic cluster due to mismatching chromatic context index.");
        }
    }
}