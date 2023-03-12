using System;

namespace Demos.MusicTheory.ChromaticContext;

internal abstract class ElementaryInterval : IChromaticIndexSpan
{
    public int ChromaticIndexSpan
    {
        get => _chromaticIndexSpan;
        private init => _chromaticIndexSpan = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(ChromaticIndexSpan));
    }

    private readonly int _chromaticIndexSpan;

    protected ElementaryInterval(int chromaticIndexSpan)
    {
        ChromaticIndexSpan = chromaticIndexSpan;
    }
}