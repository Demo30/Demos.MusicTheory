using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext;

internal interface IChromaticEntity : IMusicalEntity
{
    public int ChromaticContextIndex { get; }
}