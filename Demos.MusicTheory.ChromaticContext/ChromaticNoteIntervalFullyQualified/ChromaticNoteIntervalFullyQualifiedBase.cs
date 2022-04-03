using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

public abstract class ChromaticNoteIntervalFullyQualifiedBase : ChromaticNoteInterval
{
    public ChromaticNoteIntervalQuality Quality { get; }

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


    protected ChromaticNoteIntervalFullyQualifiedBase(int intervalBaseNumber, ChromaticNoteIntervalQuality quality) : base(GetChromaticIndexSpan(intervalBaseNumber, quality))
    {
        if (!_validations.IsNumberQualityCombinationValid(intervalBaseNumber, quality))
            throw new ArgumentException("Invalid combination of interval base number and quality.");

        Quality = quality;
        DiatonicScaleDegree = intervalBaseNumber;
    }

    private static int GetChromaticIndexSpan(int intervalBaseNumber, ChromaticNoteIntervalQuality quality) =>
        _chromaticIndexSpanCounter.GetChromaticIndexSpan(intervalBaseNumber, quality);

    /// <summary>
    /// This distinction probably does not exist in proper music theory, but seems reasonable enough and a handy one too
    /// </summary>
    /// <param name="intervalBaseNumber"></param>
    /// <returns></returns>
    public static bool IsPerfectType(int intervalBaseNumber)
    {
        return new [] { 1, 4, 5 }.Contains(ChromaticIndexSpanCounter.GetSimpleBaseNumber(intervalBaseNumber));
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

        internal int GetChromaticIndexSpan(int intervalBaseNumber, ChromaticNoteIntervalQuality quality)
        {
            var basicSemitoneCountCorrections = GetBasicSemitoneCountCorrections(intervalBaseNumber);
            var simpleBaseNumber = GetSimpleBaseNumber(intervalBaseNumber);
            var diatonicCorrection = simpleBaseNumber / 4;
            var baseSemitoneCount = (simpleBaseNumber - 1) * 2;
            var semitones =
                (GetSubOctaves(intervalBaseNumber) * 12) + 
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
        internal static int GetSimpleBaseNumber(int intervalBaseNumber) => intervalBaseNumber - (GetSubOctaves(intervalBaseNumber) * 7);

        internal static int GetSubOctaves(int intervalBaseNumber) => (intervalBaseNumber - 1) / 7;

        private Dictionary<ChromaticNoteIntervalQuality, int> GetBasicSemitoneCountCorrections(int intervalBaseNumber)
        {
            var diffs = new Dictionary<ChromaticNoteIntervalQuality, int>()
            {
                { ChromaticNoteIntervalQuality.Perfect, 0 },
                { ChromaticNoteIntervalQuality.Major, 0 },
                { ChromaticNoteIntervalQuality.Minor, - 1},
                { ChromaticNoteIntervalQuality.Augmented, 1},
            };
            
            if (intervalBaseNumber == 1)
            {
                diffs[ChromaticNoteIntervalQuality.Diminished] = 0;
            }
            else if (_isPerfectTypeCallback(intervalBaseNumber))
            {
                diffs[ChromaticNoteIntervalQuality.Diminished] = -1;
            }
            else
            {
                diffs[ChromaticNoteIntervalQuality.Diminished] = -2;
            }

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
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        internal bool IsNumberQualityCombinationValid(int diatonicScaleDegree, ChromaticNoteIntervalQuality quality)
        {
            var primaDeviation = diatonicScaleDegree == 1 && quality == ChromaticNoteIntervalQuality.Diminished;
            return !primaDeviation && GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(diatonicScaleDegree).Contains(quality);
        }

        private ChromaticNoteIntervalQuality[] GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(int diatonicScaleDegree)
        {
            ChromaticNoteIntervalQuality[] validQualities;
            var isPerfectType = _isPerfectTypeCallback(diatonicScaleDegree);
            if (isPerfectType)
            {
                validQualities = new []
                {
                    ChromaticNoteIntervalQuality.Perfect,
                    ChromaticNoteIntervalQuality.Diminished,
                    ChromaticNoteIntervalQuality.Augmented
                };
            }
            else
            {
                validQualities = new []
                {
                    ChromaticNoteIntervalQuality.Major,
                    ChromaticNoteIntervalQuality.Minor,
                    ChromaticNoteIntervalQuality.Diminished,
                    ChromaticNoteIntervalQuality.Augmented
                };
            }
            return validQualities;
        }
    }
}