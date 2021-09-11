using Demos.MusicTheory.Abstractions.Commons;

namespace Demos.MusicTheory.Abstractions.ChromaticContext
{
    public interface IChromaticNote : IMusicalEntity
    {
        public int ChromaticContextIndex { get; }
    }
}
