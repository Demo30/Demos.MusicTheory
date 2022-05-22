using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

public class Interval : ElementaryInterval
{
    public IntervalQuality Quality { get; }
    
    /// <summary>
    /// Subtracts compound octave intervals from the overall base number.
    /// </summary>
    /// <returns></returns>
    public int SimpleBaseNumber => GetSimpleBaseNumber(DiatonicScaleDegree);
    
    /// <summary>
    /// Basic interval number identification such as: second, third, sixth etc.
    /// </summary>
    public int DiatonicScaleDegree
    {
        get => _diatonicScaleDegree;
        private init
        {
            CheckValidDiatonicScaleDegree(value);
            _diatonicScaleDegree = value;
        }
    }

    /// <summary>
    /// Chromatic index span alias property
    /// </summary>
    public int SemitoneCount => ChromaticIndexSpan;

    /// <summary>
    /// Starting from 0. One full octave has one sub-octave.
    /// </summary>
    public int SubOctaves => GetSubOctaves(DiatonicScaleDegree);

    /// <summary>
    /// This distinction probably does not exist in proper music theory, but seems reasonable enough and a handy one too
    /// </summary>
    /// <param name="intervalBaseNumber"></param>
    /// <returns></returns>
    public static bool IsPerfectType(int intervalBaseNumber)
    {
        return new[] {1, 4, 5}.Contains(GetSimpleBaseNumber(intervalBaseNumber));
    }
    
    private readonly int _diatonicScaleDegree;
    
    private static int GetChromaticIndexSpan(int intervalBaseNumber, IntervalQuality quality) => CalculateChromaticIndexSpan(intervalBaseNumber, quality);

    public Interval(int intervalBaseNumber, IntervalQuality quality) : base(GetChromaticIndexSpan(intervalBaseNumber, quality))
    {
        if (!IsNumberQualityCombinationValid(intervalBaseNumber, quality))
            throw new ArgumentException("Invalid combination of interval base number and quality.");

        Quality = quality;
        DiatonicScaleDegree = intervalBaseNumber;
    }
    
    private static int CalculateChromaticIndexSpan(int intervalBaseNumber, IntervalQuality quality)
    {
        var basicSemitoneCountCorrections = GetBasicSemitoneCountCorrections(intervalBaseNumber);
        var simpleBaseNumber = GetSimpleBaseNumber(intervalBaseNumber);
        var diatonicCorrection = simpleBaseNumber / 4;
        var baseSemitoneCount = (simpleBaseNumber - 1) * 2;
        var semitones =
            GetSubOctaves(intervalBaseNumber) * 12 +
            baseSemitoneCount -
            diatonicCorrection +
            basicSemitoneCountCorrections[quality];

        return semitones;
    }

    /// <summary>
    /// Gets interval base number disregarding possible compound interval characteristic
    /// </summary>
    /// <param name="intervalBaseNumber"></param>
    /// <returns></returns>
    private static int GetSimpleBaseNumber(int intervalBaseNumber) => intervalBaseNumber - GetSubOctaves(intervalBaseNumber) * 7;

    private static int GetSubOctaves(int intervalBaseNumber) => (intervalBaseNumber - 1) / 7;
    
    private static Dictionary<IntervalQuality, int> GetBasicSemitoneCountCorrections(int intervalBaseNumber)
    {
        var diffs = new Dictionary<IntervalQuality, int>()
        {
            {IntervalQuality.Perfect, 0},
            {IntervalQuality.Major, 0},
            {IntervalQuality.Minor, -1},
            {IntervalQuality.Augmented, 1}
        };
        
        if (intervalBaseNumber == 1)
            diffs[IntervalQuality.Diminished] = 0;
        else if (IsPerfectType(intervalBaseNumber))
            diffs[IntervalQuality.Diminished] = -1;
        else
            diffs[IntervalQuality.Diminished] = -2;

        return diffs;
    }

    private static void CheckValidDiatonicScaleDegree(int number)
    {
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));
    }

    private bool IsNumberQualityCombinationValid(int diatonicScaleDegree, IntervalQuality quality)
    {
        var primaDeviation = 
            diatonicScaleDegree == 1 &&
            quality == IntervalQuality.Diminished;
        
        return 
            !primaDeviation &&
            GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(diatonicScaleDegree).Contains(quality);
    }

    private IEnumerable<IntervalQuality> GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(int diatonicScaleDegree)
    {
        return IsPerfectType(diatonicScaleDegree) ?
            new[]
            {
                IntervalQuality.Perfect,
                IntervalQuality.Diminished,
                IntervalQuality.Augmented
            } :
            new[]
            {
                IntervalQuality.Major,
                IntervalQuality.Minor,
                IntervalQuality.Diminished,
                IntervalQuality.Augmented
            };
    }
}