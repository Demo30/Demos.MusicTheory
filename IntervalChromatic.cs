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
    public class IntervalChromatic : IntervalChromaticBase
    {
        public IntervalChromaticQuality Quality { get; }

        protected bool PerfectType { get { return this.IsPerfectType(this.Number); } }

        /// <summary>
        /// Starting from 0. One octave has one suboctave.
        /// </summary>
        protected int Suboctaves { get { return this.GetSuboctaves(this.Number); } }

        /// <summary>
        /// Subtracts compound octave intervals from the overal base number.
        /// </summary>
        /// <returns></returns>
        protected int SimpleBaseNumber { get { return this.GetSimpleBaseNumber(this.Number); } }

        public IntervalChromatic(int number, IntervalChromaticQuality quality) : base(number)
        {
            if (this.IsCombinationValid(number, quality))
            {
                this.Quality = quality;
            } else
            {
                throw new ArgumentException("Invalid combination of interval base number and quality.");
            }
        }

        public int GetSemitoneCount()
        {
            Dictionary<IntervalChromaticQuality, int> diffs = new Dictionary<IntervalChromaticQuality, int>();
            diffs[IntervalChromaticQuality.Perfect] = 0;
            diffs[IntervalChromaticQuality.Major] = 0;
            diffs[IntervalChromaticQuality.Minor] = -1;
            diffs[IntervalChromaticQuality.Augmented] = 1;
            if (this.Number == 1)
            {
                diffs[IntervalChromaticQuality.Diminished] = 0;
            } else if (this.PerfectType)
            {
                diffs[IntervalChromaticQuality.Diminished] = -1;
            } else
            {
                diffs[IntervalChromaticQuality.Diminished] = -2;
            }

            int diatonicCorrection = this.SimpleBaseNumber / 4;
            int baseSemitoneCount = ((this.SimpleBaseNumber - 1) * 2);
            int semitones = (this.Suboctaves * 12) + baseSemitoneCount - diatonicCorrection + diffs[this.Quality];

            return semitones;
        }

        protected int GetSimpleBaseNumber(int number)
        {
            return number - (this.GetSuboctaves(number) * 7);
        }

        protected int GetSuboctaves(int number)
        {
            /* Beware changing this - affects both perfect type evaluation and semitone count */
            return (int)((number - 1) / 7);
        }

        private bool IsCombinationValid(int number, IntervalChromaticQuality quality)
        {
            if (number == 1 && quality == IntervalChromaticQuality.Diminished)
            {
                return false;
            } else
            {
                bool perfectType = this.IsPerfectType(number);
                Dictionary<bool, IntervalChromaticQuality[]> validQualities = new Dictionary<bool, IntervalChromaticQuality[]>()
                {
                    { true, new IntervalChromaticQuality[] {
                        IntervalChromaticQuality.Perfect,
                        IntervalChromaticQuality.Diminished,
                        IntervalChromaticQuality.Augmented
                    } },
                    { false, new IntervalChromaticQuality[]
                    {
                        IntervalChromaticQuality.Major,
                        IntervalChromaticQuality.Minor,
                        IntervalChromaticQuality.Diminished,
                        IntervalChromaticQuality.Augmented
                    }}
                };
                return validQualities[perfectType].Contains(quality);
            }
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
