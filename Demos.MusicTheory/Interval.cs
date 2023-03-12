using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Mappers;

namespace Demos.MusicTheory;

public partial class Interval
{
    private IntervalInternal _intervalInternal;

    internal IntervalInternal IntervalInternal => _intervalInternal;
    
    public Interval(int baseLength, IntervalQuality intervalQuality)
    {
        _intervalInternal = new IntervalInternal(baseLength, IntervalMapper.Map(intervalQuality));
    }

    #region Interval properties
    
    /// <summary>
    /// Third, fourth, fifth etc.
    /// </summary>
    public int DiatonicDegree => _intervalInternal.DiatonicScaleDegree;

    /// <summary>
    /// Perfect, major, minor etc.
    /// </summary>
    public IntervalQuality Quality => IntervalMapper.Map(_intervalInternal.QualityInternal);
    
    public int SemitoneCount => _intervalInternal.SemitoneCount;

    public override string ToString() => _intervalInternal.ToString();

    #endregion

    #region Interval services

    public static IEnumerable<Interval> GetIntervalsByChromaticDistance(int chromaticDistance) =>
        MusicTheoryService.Instance.GetIntervalsBySemitoneDistance(chromaticDistance);



    #endregion
}

public partial class Interval
{
    public static Interval PerfectUnison => new(1, IntervalQuality.Perfect);
    public static Interval AugmentedUnison => new(1, IntervalQuality.Augmented);
    public static Interval DiminishedSecond => new Interval(2, IntervalQuality.Diminished);
    public static Interval MinorSecond => new(2, IntervalQuality.Minor);
    public static Interval MajorSecond => new(2, IntervalQuality.Major);
    public static Interval DiminishedThird => new Interval(3, IntervalQuality.Diminished);
    public static Interval MinorThird => new(3, IntervalQuality.Minor);
    public static Interval MajorThird => new(3, IntervalQuality.Major);
    public static Interval PerfectFourth => new(4, IntervalQuality.Perfect);
    public static Interval PerfectFifth => new(5, IntervalQuality.Perfect);
    public static Interval DiminishedSixth => new Interval(6, IntervalQuality.Diminished);
    public static Interval MinorSixth => new(6, IntervalQuality.Minor);
    public static Interval MajorSixth => new(6, IntervalQuality.Major);
    public static Interval DiminishedSeventh => new Interval(7, IntervalQuality.Diminished);
    public static Interval MinorSeventh => new(7, IntervalQuality.Minor);
    public static Interval MajorSeventh => new(7, IntervalQuality.Major);
    public static Interval PerfectOctave => new(8, IntervalQuality.Perfect);
}