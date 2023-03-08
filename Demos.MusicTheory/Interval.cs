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


    #endregion

    #region Interval services
    
    //public static Interval GetIntervalFromChromaticDistance(int chromaticDistance) => MusicTheoryService.Instance.



    #endregion
}

public partial class Interval
{
    public static Interval PerfectUnison => new(1, IntervalQuality.Perfect);
    public static Interval MinorSecond => new(2, IntervalQuality.Minor);
    public static Interval MajorSecond => new(2, IntervalQuality.Major);
    public static Interval MinorThird => new(3, IntervalQuality.Minor);
    public static Interval MajorThird => new(3, IntervalQuality.Major);
    public static Interval PerfectFourth => new(4, IntervalQuality.Perfect);
    public static Interval PerfectFifth => new(5, IntervalQuality.Perfect);
    public static Interval MinorSixth => new(6, IntervalQuality.Minor);
    public static Interval MajorSixth => new(6, IntervalQuality.Major);
    public static Interval MinorSeventh => new(7, IntervalQuality.Minor);
    public static Interval MajorSeventh => new(7, IntervalQuality.Major);
    public static Interval PerfectOctave => new(8, IntervalQuality.Perfect);
}