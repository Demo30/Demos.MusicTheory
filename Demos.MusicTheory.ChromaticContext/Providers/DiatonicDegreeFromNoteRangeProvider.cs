using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.Constants;

namespace Demos.MusicTheory.ChromaticContext.Providers;

internal class DiatonicDegreeFromNoteRangeProvider : IDiatonicDegreeFromNoteRangeProvider
{
    public int GetDiatonicDegreeSpan(NoteRangeInternal rangeInternal)
    {
        var startNote = rangeInternal.NoteInternalStart;
        var endNote = rangeInternal.NoteInternalEnd;
        
        var startNoteDiatonicDegree = (int) startNote.QualityInternal;
        var endNoteDiatonicDegree = (int) endNote.QualityInternal;

        var subOctaves = rangeInternal.ChromaticIndexSpan / ChromaticContextConstants.ChromaticStepsFullOctave;
        var subOctavesDiatonicDegrees = subOctaves * ChromaticContextConstants.DiatonicStepsInOctave;
        
        int baseDiatonicDifference;
        if (startNote <= endNote && startNoteDiatonicDegree <= endNoteDiatonicDegree)
        {
            baseDiatonicDifference = endNoteDiatonicDegree - startNoteDiatonicDegree;
        }
        else
        {
            baseDiatonicDifference = (endNoteDiatonicDegree + 7) - startNoteDiatonicDegree;
        }

        return subOctavesDiatonicDegrees + (baseDiatonicDifference + 1);
    }
}