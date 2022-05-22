using System;

namespace Demos.MusicTheory.ChromaticContext;

public abstract class IntervalBase : IChromaticIndexSpan
{
    public int ChromaticIndexSpan
    {
        get => _chromaticIndexSpan;
        private init => _chromaticIndexSpan = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(ChromaticIndexSpan));
    }

    private readonly int _chromaticIndexSpan;

    protected IntervalBase(int chromaticIndexSpan)
    {
        ChromaticIndexSpan = chromaticIndexSpan;
    }
}