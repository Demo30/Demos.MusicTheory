using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

internal class IntervalInternal : ElementaryInterval
{
    public IntervalQualityInternal QualityInternal { get; }

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

    private static int GetChromaticIndexSpan(int intervalBaseNumber, IntervalQualityInternal qualityInternal) =>
        CalculateChromaticIndexSpan(intervalBaseNumber, qualityInternal);

    public IntervalInternal(int intervalBaseNumber, IntervalQualityInternal qualityInternal) : base(
        GetChromaticIndexSpan(intervalBaseNumber, qualityInternal))
    {
        if (!IsNumberQualityCombinationValid(intervalBaseNumber, qualityInternal))
            throw new ArgumentException("Invalid combination of interval base number and quality.");

        QualityInternal = qualityInternal;
        DiatonicScaleDegree = intervalBaseNumber;
    }

    private static int CalculateChromaticIndexSpan(int intervalBaseNumber, IntervalQualityInternal qualityInternal)
    {
        var basicSemitoneCountCorrections = GetBasicSemitoneCountCorrections(intervalBaseNumber);
        var simpleBaseNumber = GetSimpleBaseNumber(intervalBaseNumber);
        var diatonicCorrection = simpleBaseNumber / 4;
        var baseSemitoneCount = (simpleBaseNumber - 1) * 2;
        var semitones =
            GetSubOctaves(intervalBaseNumber) * 12 +
            baseSemitoneCount -
            diatonicCorrection +
            basicSemitoneCountCorrections[qualityInternal];

        return semitones;
    }

    /// <summary>
    /// Gets interval base number disregarding possible compound interval characteristic
    /// </summary>
    /// <param name="intervalBaseNumber"></param>
    /// <returns></returns>
    private static int GetSimpleBaseNumber(int intervalBaseNumber) =>
        intervalBaseNumber - GetSubOctaves(intervalBaseNumber) * 7;

    private static int GetSubOctaves(int intervalBaseNumber) => (intervalBaseNumber - 1) / 7;

    private static Dictionary<IntervalQualityInternal, int> GetBasicSemitoneCountCorrections(int intervalBaseNumber)
    {
        var diffs = new Dictionary<IntervalQualityInternal, int>
        {
            {IntervalQualityInternal.Perfect, 0},
            {IntervalQualityInternal.Major, 0},
            {IntervalQualityInternal.Minor, -1},
            {IntervalQualityInternal.Augmented, 1},
        };

        if (intervalBaseNumber == 1)
            diffs[IntervalQualityInternal.Diminished] = 0;
        else if (IsPerfectType(intervalBaseNumber))
            diffs[IntervalQualityInternal.Diminished] = -1;
        else
            diffs[IntervalQualityInternal.Diminished] = -2;

        return diffs;
    }

    private static void CheckValidDiatonicScaleDegree(int number)
    {
        if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));
    }

    private bool IsNumberQualityCombinationValid(int diatonicScaleDegree, IntervalQualityInternal qualityInternal)
    {
        var primaDeviation =
            diatonicScaleDegree == 1 &&
            qualityInternal == IntervalQualityInternal.Diminished;

        return
            !primaDeviation &&
            GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(diatonicScaleDegree).Contains(qualityInternal);
    }

    private IEnumerable<IntervalQualityInternal> GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(
        int diatonicScaleDegree)
    {
        return IsPerfectType(diatonicScaleDegree)
            ? new[]
            {
                IntervalQualityInternal.Perfect,
                IntervalQualityInternal.Diminished,
                IntervalQualityInternal.Augmented
            }
            : new[]
            {
                IntervalQualityInternal.Major,
                IntervalQualityInternal.Minor,
                IntervalQualityInternal.Diminished,
                IntervalQualityInternal.Augmented,
            };
    }

    public override string ToString() => $"{GetIntervalQualityNotation(QualityInternal)}{_diatonicScaleDegree}";

    private static string GetIntervalQualityNotation(IntervalQualityInternal qualityInternal)
    {
        return qualityInternal switch
        {
            IntervalQualityInternal.Perfect => "P",
            IntervalQualityInternal.Minor => "m",
            IntervalQualityInternal.Major => "M",
            IntervalQualityInternal.Augmented => "A",
            IntervalQualityInternal.Diminished => "d",
            _ => throw new InvalidOperationException()
        };
    }
}