using Demos.MusicTheory.Abstractions.Commons;

namespace Demos.MusicTheory.Abstractions.ChromaticContext
{
    public interface IChromaticEntity : IMusicalEntity
    {
        public int ChromaticContextIndex { get; }
    }
}
