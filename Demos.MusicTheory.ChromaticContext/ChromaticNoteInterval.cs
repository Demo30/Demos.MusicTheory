using System;

namespace Demos.MusicTheory.ChromaticContext;

public abstract class ChromaticNoteInterval : IChromaticIndexSpan
{
    public int ChromaticIndexSpan
    {
        get => _chromaticIndexSpan;
        private init => _chromaticIndexSpan = value >= 0 ? value : throw new ArgumentOutOfRangeException();
    }

    private readonly int _chromaticIndexSpan;

    protected ChromaticNoteInterval(int chromaticIndexSpan)
    {
        ChromaticIndexSpan = chromaticIndexSpan;
    }
}