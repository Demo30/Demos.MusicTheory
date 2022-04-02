using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Commons;
using System.Collections.Generic;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
internal class ChromaticNoteFullyQualifiedProviderFromNoteBySpan
{
    internal ChromaticNoteEnharmonicCluster GetEnharmonicNoteCluster(ChromaticNoteFullyQualified note, int chromaticIndexSpan, OneDimensionDirection direction)
    {
        List<ChromaticNoteFullyQualified> enharmonicNotes = new();

        int noteIndex = note.ChromaticContextIndex;
        int noteSimpleIndex = noteIndex % ChromaticContextConstants.ChromaticStepsFullOctave;



        enharmonicNotes.Add(new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 1, NotationSymbols.None));
        //int firstDiatonicTreshold = (int)ChromaticNoteQuality.E

        return new ChromaticNoteEnharmonicCluster(enharmonicNotes.ToArray());
    }
}