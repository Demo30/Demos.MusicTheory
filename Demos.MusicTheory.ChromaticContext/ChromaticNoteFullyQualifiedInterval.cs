using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory
{
    /// <summary>
    /// Ordinary interval between two fully qualified chromatic notes
    /// </summary>
    public class ChromaticNoteFullyQualifiedInterval
    {
        public ChromaticNoteIntervalQuality Quality { get; }

        public int IntervalBaseNumber
        {
            get => _intervalBaseNumber;
            init
            {
                CheckValidIntervalNumber(value);
                _intervalBaseNumber = value;
            }
        }

        public int SemitoneCount => GetSemitoneCount();

        protected bool PerfectType => IsPerfectType(IntervalBaseNumber);

        /// <summary>
        /// Starting from 0. One octave has one suboctave.
        /// </summary>
        protected int Suboctaves => GetSuboctaves(IntervalBaseNumber);

        /// <summary>
        /// Subtracts compound octave intervals from the overal base number.
        /// </summary>
        /// <returns></returns>
        protected int SimpleBaseNumber => GetSimpleBaseNumber(IntervalBaseNumber);

        private int _intervalBaseNumber;

        public ChromaticNoteFullyQualifiedInterval(int intervalBaseNumber, ChromaticNoteIntervalQuality quality) : base()
        {
            if (this.IsNumberQualityCombinationValid(intervalBaseNumber, quality))
            {
                this.Quality = quality;
                this.IntervalBaseNumber = intervalBaseNumber;
            }
            else
            {
                throw new ArgumentException("Invalid combination of interval base number and quality.");
            }
        }

        private int GetSemitoneCount()
        {
            var basicSemitoneCountCorrections = GetBasicSemitoneCountCorrections();
            int diatonicCorrection = this.SimpleBaseNumber / 4;
            int baseSemitoneCount = ((this.SimpleBaseNumber - 1) * 2);
            int semitones = (this.Suboctaves * 12) + baseSemitoneCount - diatonicCorrection + basicSemitoneCountCorrections[this.Quality];

            return semitones;
        }

        /// <summary>
        /// Gets interval base number disregarding possible compound interval characteristic
        /// </summary>
        /// <param name="intervalBaseNumber"></param>
        /// <returns></returns>
        private int GetSimpleBaseNumber(int intervalBaseNumber) =>
            intervalBaseNumber - (GetSuboctaves(intervalBaseNumber) * 7);

        private int GetSuboctaves(int intervalBaseNumber) =>
            ((intervalBaseNumber - 1) / 7);

        private Dictionary<ChromaticNoteIntervalQuality, int> GetBasicSemitoneCountCorrections()
        {
            Dictionary<ChromaticNoteIntervalQuality, int> diffs = new Dictionary<ChromaticNoteIntervalQuality, int>()
            {
                { ChromaticNoteIntervalQuality.Perfect, 0 },
                { ChromaticNoteIntervalQuality.Major, 0 },
                { ChromaticNoteIntervalQuality.Minor, - 1},
                { ChromaticNoteIntervalQuality.Augmented, 1},
            };
            if (this.IntervalBaseNumber == 1)
            {
                diffs[ChromaticNoteIntervalQuality.Diminished] = 0;
            }
            else if (this.PerfectType)
            {
                diffs[ChromaticNoteIntervalQuality.Diminished] = -1;
            }
            else
            {
                diffs[ChromaticNoteIntervalQuality.Diminished] = -2;
            }

            return diffs;
        }

        private ChromaticNoteIntervalQuality[] GetValidIntervalChromaticQualitiesForIntervalBaseNumber(int intervalBaseNumber)
        {
            ChromaticNoteIntervalQuality[] validQualities;
            bool isPerfectType = this.IsPerfectType(intervalBaseNumber);
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

        /// <summary>
        /// This distinction probably does not exist in proper music theory, but seems reasonable enough and a handy one too
        /// </summary>
        /// <param name="intervalBaseNumber"></param>
        /// <returns></returns>
        private bool IsPerfectType(int intervalBaseNumber)
        {
            return new int[] { 1, 4, 5 }.Contains(this.GetSimpleBaseNumber(intervalBaseNumber));
        }

        private bool IsNumberQualityCombinationValid(int intervalBaseNumber, ChromaticNoteIntervalQuality quality)
        {
            bool primaDeviation = intervalBaseNumber == 1 && quality == ChromaticNoteIntervalQuality.Diminished;
            return !primaDeviation ?
                GetValidIntervalChromaticQualitiesForIntervalBaseNumber(intervalBaseNumber).Contains(quality) :
                false;
        }

        private void CheckValidIntervalNumber(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
