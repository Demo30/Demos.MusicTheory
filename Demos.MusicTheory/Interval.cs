using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Mappers;

namespace Demos.MusicTheory;

public class Interval
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