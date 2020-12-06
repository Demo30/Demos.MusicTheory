using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    /// <summary>
    /// Base of an ordinary (chromatic) interval of notes on the chromatic 12 semitones long scale
    /// </summary>
    public class IntervalChromaticBase : Interval
    {
        public int Number { get; }

        public IntervalChromaticBase(int number)
        {
            if (number > 0)
            {
                this.Number = number;
            } else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
