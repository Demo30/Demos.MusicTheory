using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified
{
    public abstract class ChromaticNoteIntervalFullyQualifiedBase : ChromaticNoteInterval
    {
        public ChromaticNoteIntervalQuality Quality { get; }

        /// <summary>
        /// Basic interval number identification such as: second, third, sixth etc.
        /// </summary>
        public int DiatonicScaleDegree
        {
            get => _diatonicScaleDegree;
            init
            {
                _validations.CheckValidDiatonicScaleDegree(value);
                _diatonicScaleDegree = value;
            }
        }

        private int _diatonicScaleDegree;

        protected static ChromaticIndexSpanCounter _chromaticIndexSpanCounter => new ChromaticIndexSpanCounter(IsPerfectType);

        private Validations _validations => new Validations(IsPerfectType);
        

        public ChromaticNoteIntervalFullyQualifiedBase(int intervalBaseNumber, ChromaticNoteIntervalQuality quality) : base(GetChromaticIndexSpan(intervalBaseNumber, quality))
        {
            if (!_validations.IsNumberQualityCombinationValid(intervalBaseNumber, quality))
                throw new ArgumentException("Invalid combination of interval base number and quality.");

            this.Quality = quality;
            this.DiatonicScaleDegree = intervalBaseNumber;
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
            return new int[] { 1, 4, 5 }.Contains(_chromaticIndexSpanCounter.GetSimpleBaseNumber(intervalBaseNumber));
        }

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
                int diatonicCorrection = GetSimpleBaseNumber(intervalBaseNumber) / 4;
                int baseSemitoneCount = ((GetSimpleBaseNumber(intervalBaseNumber) - 1) * 2);
                int semitones =
                    (GetSuboctaves(intervalBaseNumber) * 12) + 
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
            internal int GetSimpleBaseNumber(int intervalBaseNumber) =>
                intervalBaseNumber - (GetSuboctaves(intervalBaseNumber) * 7);

            internal int GetSuboctaves(int intervalBaseNumber) =>
                ((intervalBaseNumber - 1) / 7);

            internal Dictionary<ChromaticNoteIntervalQuality, int> GetBasicSemitoneCountCorrections(int intervalBaseNumber)
            {
                Dictionary<ChromaticNoteIntervalQuality, int> diffs = new Dictionary<ChromaticNoteIntervalQuality, int>()
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
                bool primaDeviation = diatonicScaleDegree == 1 && quality == ChromaticNoteIntervalQuality.Diminished;
                return !primaDeviation ?
                    GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(diatonicScaleDegree).Contains(quality) :
                    false;
            }

            private ChromaticNoteIntervalQuality[] GetValidIntervalChromaticQualitiesForDiatonicScaleDegree(int diatonicScaleDegree)
            {
                ChromaticNoteIntervalQuality[] validQualities;
                bool isPerfectType = _isPerfectTypeCallback(diatonicScaleDegree);
                if (isPerfectType)
                {
                    validQualities = new ChromaticNoteIntervalQuality[]
                    {
                    ChromaticNoteIntervalQuality.Perfect,
                    ChromaticNoteIntervalQuality.Diminished,
                    ChromaticNoteIntervalQuality.Augmented
                    };
                }
                else
                {
                    validQualities = new ChromaticNoteIntervalQuality[]
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

}
