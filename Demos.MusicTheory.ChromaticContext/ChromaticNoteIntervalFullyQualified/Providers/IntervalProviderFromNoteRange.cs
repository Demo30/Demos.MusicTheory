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

    public Interval GetIntervals(NoteRange range)
    {
        return _indexSpanProvider
            .GetIntervals(range.ChromaticIndexSpan)
            .Intervals
            .Single(i => i.DiatonicScaleDegree == GetDiatonicDegreeSpan(range));
    }

    private static int GetDiatonicDegreeSpan(NoteRange range)
    {
        var subOctaves = range.ChromaticIndexSpan / ChromaticContextConstants.ChromaticStepsFullOctave;
        var subOctavesDiatonicDegrees = subOctaves * ChromaticContextConstants.DiatonicStepsInOctave;
        var baseDiatonicDifference =
            Math.Abs((int) range.NoteStart.Quality - (int) range.NoteEnd.Quality) + 1;
        return subOctavesDiatonicDegrees + baseDiatonicDifference;
    }
}