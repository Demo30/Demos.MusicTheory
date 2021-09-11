using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    public class ChromaticScale
    {
        private List<ChromaticNote> ChromaticScaleOrderedNotes { get; set; }

        private void Init()
        {
            BuildChromaticScale(1, 50);


        }

        private void BuildChromaticScale(int fromIndex, int span)
        {
            ChromaticScaleOrderedNotes = ChromaticScaleOrderedNotes ?? new List<ChromaticNote>();
            ChromaticScaleOrderedNotes.Clear();
        }
    }
}
