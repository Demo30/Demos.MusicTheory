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
        public int IntervalBaseNumber
        { 
            get => _number;
            set
            {
                CheckValidIntervalNumber(value);
                _number = value;
            }
        }

        private int _number;

        public IntervalChromaticBase(int number)
        {
            this.IntervalBaseNumber = number;
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
