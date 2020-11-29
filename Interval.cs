using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    public class Interval
    {
        public int Number { get; }

        public Interval(int number)
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
