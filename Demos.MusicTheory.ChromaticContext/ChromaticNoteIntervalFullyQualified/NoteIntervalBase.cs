using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

public abstract class NoteIntervalBase : NoteInterval
{
    public IntervalQuality Quality { get; }

    /// <summary>
    /// Basic interval number identification such as: second, third, sixth etc.
    /// </summary>
    public int DiatonicScaleDegree
    {
        get => _diatonicScaleDegree;
        private init
        {
            _validations.CheckValidDiatonicScaleDegree(value);
            _diatonicScaleDegree = value;
        }
    }

    protected static ChromaticIndexSpanCounter _chromaticIndexSpanCounter => new(IsPerfectType);

    private readonly int _diatonicScaleDegree;

    private Validations _validations => new(IsPerfectType);


    protected NoteIntervalBase(int intervalBaseNumber, IntervalQuality quality) : base(
        GetChromaticIndexSpan(intervalBaseNumber, quality))
    {
        if (!_validations.IsNumberQualityCombinationValid(intervalBaseNumber, quality))
            throw new ArgumentException("Invalid combination of interval base number and quality.");

        Quality = quality;
        DiatonicScaleDegree = intervalBaseNumber;
    }

    private static int GetChromaticIndexSpan(int intervalBaseNumber, IntervalQuality quality)
    {
        return _chromaticIndexSpanCounter.GetChromaticIndexSpan(intervalBaseNumber, quality);
    }

    /// <summary>
    /// This distinction probably does not exist in proper music theory, but seems reasonable enough and a handy one too
    /// </summary>
    /// <param name="intervalBaseNumber"></param>
    /// <returns></returns>
    public static bool IsPerfectType(int intervalBaseNumber)
    {
        return new[] {1, 4, 5}.Contains(ChromaticIndexSpanCounter.GetSimpleBaseNumber(intervalBaseNumber));
    }


    // TODO: both of the below classes are weird - get rid of it if you can :)

    protected class ChromaticIndexSpanCounter
    {
        internal delegate bool IsPerfectType(int intervalBaseNumber);

        private readonly IsPerfectType _isPerfectTypeCallback;

        internal ChromaticIndexSpanCounter(IsPerfectType callback)
        {
            _isPerfectTypeCallback = callback;
        }

        internal int GetChromaticIndexSpan(int intervalBaseNumber, IntervalQuality quality)
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
        internal static int GetSimpleBaseNumber(int intervalBaseNumber)
        {
            return intervalBaseNumber - GetSubOctaves(intervalBaseNumber) * 7;
        }

        internal static int GetSubOctaves(int intervalBaseNumber)
        {
            return (intervalBaseNumber - 1) / 7;
        }

        private Dictionary<IntervalQuality, int> GetBasicSemitoneCountCorrections(int intervalBaseNumber)
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
            else if (_isPerfectTypeCallback(intervalBaseNumber))
                diffs[IntervalQuality.Diminished] = -1;
            else
                diffs[IntervalQuality.Diminished] = -2;

            return diffs;
        }
    }

    private class Validations
    {
        internal delegate bool IsPerfectType(int intervalBaseNumber);

        private readonly IsPerfectType _isPerfectTypeCallback;

        internal Validations(IsPerfectType callback)
        {
            _isPerfectTypeCallback = callback;
        }

        internal void CheckValidDiatonicScaleDegree(int number)
        {
            if (number <= 0) throw new ArgumentOutOfRangeException();
        }

        internal bool IsNumberQualityCombinationValid(int diatonicScaleDegree, IntervalQuality quality)
        {
            var primaDeviation = diatonicScaleDegree == 1 && quality == IntervalQuality.Diminished;
            return !primaDeviation && GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(diatonicScaleDegree)
                .Contains(quality);
        }

        private IntervalQuality[] GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(int diatonicScaleDegree)
        {
            IntervalQuality[] validQualities;
            var isPerfectType = _isPerfectTypeCallback(diatonicScaleDegree);
            if (isPerfectType)
                validQualities = new[]
                {
                    IntervalQuality.Perfect,
                    IntervalQuality.Diminished,
                    IntervalQuality.Augmented
                };
            else
                validQualities = new[]
                {
                    IntervalQuality.Major,
                    IntervalQuality.Minor,
                    IntervalQuality.Diminished,
                    IntervalQuality.Augmented
                };
            return validQualities;
        }
    }
}