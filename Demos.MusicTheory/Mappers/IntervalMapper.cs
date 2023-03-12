using System;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

namespace Demos.MusicTheory.Mappers;

internal static class IntervalMapper
{
    public static Interval Map(IntervalInternal intervalInternal) => new Interval(intervalInternal.DiatonicScaleDegree, Map(intervalInternal.QualityInternal));
    
    public static IntervalInternal Map(Interval interval) => new IntervalInternal(interval.DiatonicDegree, Map(interval.Quality));
    public static IntervalQuality Map(IntervalQualityInternal intervalQualityInternal) =>
        intervalQualityInternal switch
        {
            IntervalQualityInternal.Perfect => IntervalQuality.Perfect,
            IntervalQualityInternal.Augmented => IntervalQuality.Augmented,
            IntervalQualityInternal.Diminished => IntervalQuality.Diminished,
            IntervalQualityInternal.Minor => IntervalQuality.Minor,
            IntervalQualityInternal.Major => IntervalQuality.Major,
            _ => throw new InvalidOperationException()
        };
    
    public static IntervalQualityInternal Map(IntervalQuality intervalQuality) =>
        intervalQuality switch
        {
            IntervalQuality.Perfect => IntervalQualityInternal.Perfect,
            IntervalQuality.Augmented => IntervalQualityInternal.Augmented,
            IntervalQuality.Diminished => IntervalQualityInternal.Diminished,
            IntervalQuality.Minor => IntervalQualityInternal.Minor,
            IntervalQuality.Major => IntervalQualityInternal.Major,
            _ => throw new InvalidOperationException()
        };
}