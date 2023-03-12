using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.PhysicalContext;

public interface ITone : IMusicalEntity
{
    public double Frequency { get; }
}