using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    public class IntervalFullyQualified : Interval
    {
        public IntervalQuality Quality { get; }

        public IntervalFullyQualified(int number, IntervalQuality quality) : base(number)
        {
            if (this.IsCombinationValid(number, quality))
            {
                this.Quality = quality;
            } else
            {
                throw new ArgumentException("Invalid combination of interval base number and quality.");
            }
        }

        //public int GetSemitoneCount()
        //{
        //    int semitones =
        //        ((this.Number - 1) * 2) +
        //        (new int[] { 4, 5, 8 }.Contains(this.Number) ? -1 : 0) +
        //        this.GetQualitySemitoneDifference(this.Quality);
        //    return semitones;
        //}



        private int GetQualitySemitoneDifference(int intervalBaseNumber, IntervalQuality quality)
        {
            bool perfect = this.IsPerfectType(intervalBaseNumber);
            Dictionary<IntervalQuality, int> differences = new Dictionary<IntervalQuality, int>()
            {
                { IntervalQuality.Perfect, 0 },
                { IntervalQuality.Minor, -1 },
                { IntervalQuality.Major, 0 },
                { IntervalQuality.Diminished, -2 },
                { IntervalQuality.Augmented, 1 }
            };
            if (differences.ContainsKey(quality))
            {
                return differences[quality];
            } else
            {
                throw new Exception();
            }
        }

        private bool IsCombinationValid(int number, IntervalQuality quality)
        {
            if (number == 1 && quality == IntervalQuality.Diminished)
            {
                return false;
            } else
            {
                bool perfectType = this.IsPerfectType(number);
                Dictionary<bool, IntervalQuality[]> validQualities = new Dictionary<bool, IntervalQuality[]>()
                {
                    { true, new IntervalQuality[] {
                        IntervalQuality.Perfect,
                        IntervalQuality.Diminished,
                        IntervalQuality.Augmented
                    } },
                    { false, new IntervalQuality[]
                    {
                        IntervalQuality.Major,
                        IntervalQuality.Minor,
                        IntervalQuality.Diminished,
                        IntervalQuality.Augmented
                    }}
                };
                return validQualities[perfectType].Contains(quality);
            }
        }

        private bool IsPerfectType(int intervalBaseNumber)
        {
            return
                intervalBaseNumber == 1 ||
                intervalBaseNumber % 4 == 0 ||
                intervalBaseNumber % 5 == 0;
        }
    }
}
