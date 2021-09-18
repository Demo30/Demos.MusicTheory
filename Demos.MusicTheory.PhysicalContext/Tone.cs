﻿using Demos.MusicTheory.Abstractions.Commons;
using Demos.MusicTheory.Abstractions.PhysicalContext;

namespace Demos.MusicTheory.Contexts.PhysicalContext
{
    public class Tone : IContentEqual<Tone>, ITone
    {
        public double Frequency { get; }

        public Tone(double frequency)
        {
            this.Frequency = frequency;
        }

        public bool IsEqualByContent(Tone comparedNote) => 
            this.Frequency == comparedNote?.Frequency;
    }
}
