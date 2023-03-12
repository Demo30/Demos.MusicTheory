using System;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

namespace Demos.MusicTheory.Mappers;

internal static class ChordMapper
{
    public static DiatonicChordQuality Map(ChordQuality chordQuality)
    {
        return chordQuality switch
        {
            ChordQuality.MinorTriad => DiatonicChordQuality.MinorTriad,
            ChordQuality.MajorTriad => DiatonicChordQuality.MajorTriad,
            ChordQuality.AugmentedTriad => DiatonicChordQuality.AugmentedTriad,
            ChordQuality.DiminishedTriad => DiatonicChordQuality.DiminishedTriad,
            _ => throw new InvalidOperationException()
        };
    }
    
    public static ChordQuality Map(DiatonicChordQuality chordQuality)
    {
        return chordQuality switch
        {
            DiatonicChordQuality.MinorTriad => ChordQuality.MinorTriad,
            DiatonicChordQuality.MajorTriad => ChordQuality.MajorTriad,
            DiatonicChordQuality.AugmentedTriad => ChordQuality.AugmentedTriad,
            DiatonicChordQuality.DiminishedTriad => ChordQuality.DiminishedTriad,
            _ => throw new InvalidOperationException()
        };
    }
}