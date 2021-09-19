using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory
{
    public abstract class ChromaticNoteFullyQualifiedIntervalBase
    {
        public ChromaticNoteIntervalQuality Quality { get; }

        /// <summary>
        /// Basic interval number identification such as: second, third, sixth etc.
        /// </summary>
        public int IntervalBaseNumber
        {
            get => _intervalBaseNumber;
            init
            {
                _validations.CheckValidIntervalNumber(value);
                _intervalBaseNumber = value;
            }
        }

        protected SemitoneCounter _semitoneCounter => new SemitoneCounter(IsPerfectType, IntervalBaseNumber, Quality);

        private int _intervalBaseNumber;
        private Validations _validations => new Validations(IsPerfectType);


        public ChromaticNoteFullyQualifiedIntervalBase(int intervalBaseNumber, ChromaticNoteIntervalQuality quality)
        {
            if (!_validations.IsNumberQualityCombinationValid(intervalBaseNumber, quality))
                throw new ArgumentException("Invalid combination of interval base number and quality.");

            this.Quality = quality;
            this.IntervalBaseNumber = intervalBaseNumber;
        }

        /// <summary>
        /// This distinction probably does not exist in proper music theory, but seems reasonable enough and a handy one too
        /// </summary>
        /// <param name="intervalBaseNumber"></param>
        /// <returns></returns>
        private bool IsPerfectType(int intervalBaseNumber)
        {
            return new int[] { 1, 4, 5 }.Contains(_semitoneCounter.GetSimpleBaseNumber(intervalBaseNumber));
        }

        protected class SemitoneCounter
        {
            internal delegate bool IsPerfectType(int intervalBaseNumber);

            private readonly IsPerfectType _isPerfectTypeCallback;
            private readonly int _intervalBaseNumber;
            private readonly ChromaticNoteIntervalQuality _quality;

            internal SemitoneCounter(IsPerfectType callback, int intervalBaseNumber, ChromaticNoteIntervalQuality quality)
            {
                _isPerfectTypeCallback = callback;
                _intervalBaseNumber = intervalBaseNumber;
                _quality = quality;
            }

            internal int GetSemitoneCount()
            {
                var basicSemitoneCountCorrections = GetBasicSemitoneCountCorrections();
                int diatonicCorrection = GetSimpleBaseNumber(_intervalBaseNumber) / 4;
                int baseSemitoneCount = ((GetSimpleBaseNumber(_intervalBaseNumber) - 1) * 2);
                int semitones =
                    (GetSuboctaves(_intervalBaseNumber) * 12) + 
                    baseSemitoneCount - 
                    diatonicCorrection + 
                    basicSemitoneCountCorrections[_quality];

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

            internal Dictionary<ChromaticNoteIntervalQuality, int> GetBasicSemitoneCountCorrections()
            {
                Dictionary<ChromaticNoteIntervalQuality, int> diffs = new Dictionary<ChromaticNoteIntervalQuality, int>()
            {
                { ChromaticNoteIntervalQuality.Perfect, 0 },
                { ChromaticNoteIntervalQuality.Major, 0 },
                { ChromaticNoteIntervalQuality.Minor, - 1},
                { ChromaticNoteIntervalQuality.Augmented, 1},
            };
                if (_intervalBaseNumber == 1)
                {
                    diffs[ChromaticNoteIntervalQuality.Diminished] = 0;
                }
                else if (_isPerfectTypeCallback(_intervalBaseNumber))
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

            internal void CheckValidIntervalNumber(int number)
            {
                if (number <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            internal bool IsNumberQualityCombinationValid(int intervalBaseNumber, ChromaticNoteIntervalQuality quality)
            {
                bool primaDeviation = intervalBaseNumber == 1 && quality == ChromaticNoteIntervalQuality.Diminished;
                return !primaDeviation ?
                    GetValidIntervalChromaticQualitiesForIntervalBaseNumber(intervalBaseNumber).Contains(quality) :
                    false;
            }

            private ChromaticNoteIntervalQuality[] GetValidIntervalChromaticQualitiesForIntervalBaseNumber(int intervalBaseNumber)
            {
                ChromaticNoteIntervalQuality[] validQualities;
                bool isPerfectType = _isPerfectTypeCallback(intervalBaseNumber);
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
