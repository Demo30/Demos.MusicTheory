using System.Collections.Generic;

namespace Demos.MusicTheory.Contexts.PhysicalContext
{
    public class ToneSingleFrequencyProvider : ToneProvider<ToneSingleFrequencyProviderData>
    {
        public ToneSingleFrequencyProvider(ToneSingleFrequencyProviderData parameters) : base(parameters)
        {

        }

        public override IEnumerable<Tone> GetTones()
        {
            return new Tone[] { new Tone(BuildParameters.Frequency) };
        }
    }
}
