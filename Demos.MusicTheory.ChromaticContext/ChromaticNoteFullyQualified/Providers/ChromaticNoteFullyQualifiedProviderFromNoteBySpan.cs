using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Commons;
using System.Collections.Generic;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
internal class ChromaticNoteFullyQualifiedProviderFromNoteBySpan
{
    internal ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, int chromaticIndexSpan, OneDimensionDirection direction)
    {
        List<ChromaticNoteFullyQualified> enharmonicNotes = new();

        var noteIndex = note.ChromaticContextIndex;
        var noteSimpleIndex = noteIndex % ChromaticContextConstants.ChromaticStepsFullOctave;

        enharmonicNotes.Add(new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 1, NotationSymbols.None));
        

        return new ChromaticNoteEnharmonicCluster(enharmonicNotes.ToArray());
    }
}