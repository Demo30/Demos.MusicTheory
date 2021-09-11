using Demos.MusicTheory.Abstractions.PhysicalContext;
using Demos.MusicTheory.Commons;
using System;

namespace Demos.MusicTheory.Contexts.PhysicalContext
{
    public class Tone : IContentEqual<Tone>, ITone
    {
        public double Frequency { get; }

        public Tone(double frequency)
        {
            this.Frequency = frequency;
        }

        public bool IsEqualByContent(Tone comparedNote)
        {
            comparedNote = comparedNote ?? throw new ArgumentNullException();
            return this.Frequency == comparedNote.Frequency;
        }
    }
}
