using Demos.MusicTheory.Contexts.PhysicalContext;
using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticNoteToToneConvertor : ToneProvider<ChromaticNoteToToneConvertorData>
    {
        public ChromaticNoteToToneConvertor(ChromaticNoteToToneConvertorData parameters) : base(parameters)
        {
        }

        public override IEnumerable<PhysicalContext.Tone> GetTones()
        {
            throw new NotImplementedException();
        }
    }
}
