using System;
using System.Linq;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.Constants;
using Demos.MusicTheory.Services;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;

public class ChromaticIntervalFullyQualifiedProviderFromRange
{
    private readonly ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan _chromaticIndexSpanProvider;

    public ChromaticIntervalFullyQualifiedProviderFromRange() : this(ServicesManager.GetService<ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan>())
    {
    }
    
    internal ChromaticIntervalFullyQualifiedProviderFromRange(ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan chromaticIndexSpanProvider)
    {
        _chromaticIndexSpanProvider = chromaticIndexSpanProvider;
    }

    public ChromaticNoteIntervalFullyQualified GetIntervals(ChromaticNoteFullyQualifiedRange range) => _chromaticIndexSpanProvider
        .GetIntervals(range.ChromaticIndexSpan)
        .Cluster
        .Single(i => i.DiatonicScaleDegree == GetDiatonicDegreeSpan(range));

    private static int GetDiatonicDegreeSpan(ChromaticNoteFullyQualifiedRange range)
    {
        var subOctaves = range.ChromaticIndexSpan / ChromaticContextConstants.ChromaticStepsFullOctave;
        var subOctavesDiatonicDegrees = subOctaves * ChromaticContextConstants.DiatonicStepsInOctave;
        var baseDiatonicDifference = (Math.Abs((int) range.ChromaticNoteStart.Quality - (int) range.ChromaticNoteEnd.Quality)) + 1;
        return subOctavesDiatonicDegrees + baseDiatonicDifference;
    }
        
}