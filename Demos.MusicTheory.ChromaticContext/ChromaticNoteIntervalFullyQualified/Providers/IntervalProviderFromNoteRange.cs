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

    public IntervalInternal GetInterval(NoteRangeInternal rangeInternal)
    {
        var rangeDiatonicScaleDegree = GetDiatonicDegreeSpan(rangeInternal);
        
        // TODO: wouldn't it be easier to try to calculate these directly?
        var interval = _indexSpanProvider
            .GetIntervals(rangeInternal.ChromaticIndexSpan)
            .Intervals
            .SingleOrDefault(i => i.DiatonicScaleDegree == rangeDiatonicScaleDegree);

        if (interval is null)
        {
            throw new ApplicationException($"No suitable interval found for note range: {rangeInternal.NoteInternalStart}-{rangeInternal.NoteInternalEnd}.");
        }

        return interval;
    }

    private static int GetDiatonicDegreeSpan(NoteRangeInternal rangeInternal)
    {
        var startNote = rangeInternal.NoteInternalStart;
        var endNote = rangeInternal.NoteInternalEnd;
        
        var startNoteDiatonicDegree = (int) startNote.QualityInternal;
        var endNoteDiatonicDegree = (int) endNote.QualityInternal;

        var shouldSwitch =
            startNote > endNote ||
            (startNote == endNote && startNote.Order > endNote.Order) ||
            (startNote == endNote && startNote.Order == endNote.Order && startNoteDiatonicDegree > endNoteDiatonicDegree);

        if (shouldSwitch)
        {
            var normalizedRange = new NoteRangeInternal(rangeInternal.NoteInternalEnd, rangeInternal.NoteInternalStart);
            return GetDiatonicDegreeSpan(normalizedRange);
        }
        
        var subOctaves = rangeInternal.ChromaticIndexSpan / ChromaticContextConstants.ChromaticStepsFullOctave;
        var subOctavesDiatonicDegrees = subOctaves * ChromaticContextConstants.DiatonicStepsInOctave;
        
        int baseDiatonicDifference;
        if (rangeInternal.NoteInternalStart <= rangeInternal.NoteInternalEnd && startNoteDiatonicDegree <= endNoteDiatonicDegree)
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