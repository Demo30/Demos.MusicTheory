using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.Contexts.PhysicalContext
{
    public abstract class ToneProviderBase
    {
        public abstract Type Type { get; }

        public abstract IEnumerable<Tone> GetTones();
    }
}
