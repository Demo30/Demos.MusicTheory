using Demos.MusicTheory.Abstractions.Commons;

namespace Demos.MusicTheory.Abstractions.PhysicalContext
{
    public interface ITone : IMusicalEntity
    {
        public double Frequency { get; }
    }
}
