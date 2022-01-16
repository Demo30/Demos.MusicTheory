using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext;

public interface IChromaticEntity : IMusicalEntity
{
    public int ChromaticContextIndex { get; }
}