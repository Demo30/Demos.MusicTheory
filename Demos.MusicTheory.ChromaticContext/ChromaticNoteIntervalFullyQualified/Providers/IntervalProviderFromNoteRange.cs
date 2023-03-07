using System;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

internal class IntervalProviderFromNoteRange
{
    private readonly IntervalProviderFromIndexSpan _indexSpanProvider;

    public IntervalProviderFromNoteRange() : this(ServicesManager
        .GetService<IntervalProviderFromIndexSpan>())
    {
    }

    internal IntervalProviderFromNoteRange(
        IntervalProviderFromIndexSpan indexSpanProvider)
    {
        _indexSpanProvider = indexSpanProvider;
    }

    public IntervalInternal GetIntervals(NoteRangeInternal rangeInternal)
    {
        return _indexSpanProvider
            .GetIntervals(rangeInternal.ChromaticIndexSpan)
            .Intervals
            .Single(i => i.DiatonicScaleDegree == GetDiatonicDegreeSpan(rangeInternal));
    }

    private static int GetDiatonicDegreeSpan(NoteRangeInternal rangeInternal)
    {
        var subOctaves = rangeInternal.ChromaticIndexSpan / ChromaticContextConstants.ChromaticStepsFullOctave;
        var subOctavesDiatonicDegrees = subOctaves * ChromaticContextConstants.DiatonicStepsInOctave;
        var baseDiatonicDifference =
            Math.Abs((int) rangeInternal.NoteInternalStart.QualityInternal - (int) rangeInternal.NoteInternalEnd.QualityInternal) + 1;
        return subOctavesDiatonicDegrees + baseDiatonicDifference;
    }
}