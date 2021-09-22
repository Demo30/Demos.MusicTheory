using System;

namespace Demos.MusicTheory.ChromaticContext
{
    public class ChromaticNoteInterval : IChromaticIndexSpan
    {
        public int ChromaticIndexSpan
        {
            get => _chromaticIndexSpan;
            init
            {
                _chromaticIndexSpan = value >= 0 ? value : throw new ArgumentOutOfRangeException();
            }
        }

        private int _chromaticIndexSpan;

        public ChromaticNoteInterval(int _chromaticIndexSpan)
        {
            ChromaticIndexSpan = _chromaticIndexSpan;
        }
    }
}
