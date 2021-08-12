using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    /// <summary>
    /// Fully qualified ordinary (chromatic) interval
    /// </summary>
    public class IntervalChromaticFullyQualified : IntervalChromaticBase
    {
        public IntervalChromaticQuality Quality { get; }

        protected bool PerfectType { get { return this.IsPerfectType(this.IntervalBaseNumber); } }

        /// <summary>
        /// Starting from 0. One octave has one suboctave.
        /// </summary>
        protected int Suboctaves { get { return this.GetSuboctaves(this.IntervalBaseNumber); } }

        /// <summary>
        /// Subtracts compound octave intervals from the overal base number.
        /// </summary>
        /// <returns></returns>
        protected int SimpleBaseNumber { get { return this.GetSimpleBaseNumber(this.IntervalBaseNumber); } }

        public IntervalChromaticFullyQualified(int intervalBaseNumber, IntervalChromaticQuality quality) : base(intervalBaseNumber)
        {
            if (this.IsNumberQualityCombinationValid(intervalBaseNumber, quality))
            {
                this.Quality = quality;
            } else
            {
                throw new ArgumentException("Invalid combination of interval base number and quality.");
            }
        }

        public int GetSemitoneCount()
        {
            var basicSemitoneCountCorrections = GetBasicSemitoneCountCorrections();
            int diatonicCorrection = this.SimpleBaseNumber / 4;
            int baseSemitoneCount = ((this.SimpleBaseNumber - 1) * 2);
            int semitones = (this.Suboctaves * 12) + baseSemitoneCount - diatonicCorrection + basicSemitoneCountCorrections[this.Quality];

            return semitones;
        }

        protected int GetSimpleBaseNumber(int intervalBaseNumber)
        {
            return intervalBaseNumber - (this.GetSuboctaves(intervalBaseNumber) * 7);
        }

        protected int GetSuboctaves(int intervalBaseNumber)
        {
            /* Beware changing this - affects both perfect type evaluation and semitone count */
            return (int)((intervalBaseNumber - 1) / 7);
        }

        private Dictionary<IntervalChromaticQuality, int> GetBasicSemitoneCountCorrections()
        {
            Dictionary<IntervalChromaticQuality, int> diffs = new Dictionary<IntervalChromaticQuality, int>()
            {
                { IntervalChromaticQuality.Perfect, 0 },
                { IntervalChromaticQuality.Major, 0 },
                { IntervalChromaticQuality.Minor, - 1},
                { IntervalChromaticQuality.Augmented, 1},
            };
            if (this.IntervalBaseNumber == 1)
            {
                diffs[IntervalChromaticQuality.Diminished] = 0;
            }
            else if (this.PerfectType)
            {
                diffs[IntervalChromaticQuality.Diminished] = -1;
            }
            else
            {
                diffs[IntervalChromaticQuality.Diminished] = -2;
            }

            return diffs;
        }

        private bool IsNumberQualityCombinationValid(int intervalBaseNumber, IntervalChromaticQuality quality)
        {
            if (intervalBaseNumber == 1 && quality == IntervalChromaticQuality.Diminished)
            {
                return false;
            }
            else
            {
                return GetValidIntervalChromaticQualitiesForIntervalBaseNumber(intervalBaseNumber).Contains(quality);
            }
        }

        private IntervalChromaticQuality[] GetValidIntervalChromaticQualitiesForIntervalBaseNumber(int intervalBaseNumber)
        {
            IntervalChromaticQuality[] validQualities;
            bool isPerfectType = this.IsPerfectType(intervalBaseNumber);
            if (isPerfectType)
            {
                validQualities = new IntervalChromaticQuality[]
                {
                    IntervalChromaticQuality.Perfect,
                    IntervalChromaticQuality.Diminished,
                    IntervalChromaticQuality.Augmented
                };
            }
            else
            {
                validQualities = new IntervalChromaticQuality[]
                {
                    IntervalChromaticQuality.Major,
                    IntervalChromaticQuality.Minor,
                    IntervalChromaticQuality.Diminished,
                    IntervalChromaticQuality.Augmented
                };
            }
            return validQualities;
        }

        /// <summary>
        /// This distinction probably does not exist in proper music theory, but seems reasonable enough and a handy one too
        /// </summary>
        /// <param name="intervalBaseNumber"></param>
        /// <returns></returns>
        protected bool IsPerfectType(int intervalBaseNumber)
        {
            return new int[] { 1, 4, 5 }.Contains(this.GetSimpleBaseNumber(intervalBaseNumber));
        }
    }
}
