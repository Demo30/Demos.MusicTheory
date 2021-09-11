using Demos.MusicTheory.Abstractions.PhysicalContext;
using Demos.MusicTheory.Commons;
using System;
using System.Linq;

namespace Demos.MusicTheory.Contexts.PhysicalContext
{
    public class ToneSpace : MusicalEntitySpace<ITone>, IToneSpace
    {
        public ToneSpace(
            IToneSpaceFrequencyValidator frequencyValidator)
        {
            AddValidator(frequencyValidator);
        }

        public ITone GetToneByFrequency(double frequency, double epsilon = 0.5)
        {
            return MusicalEntities.FirstOrDefault(
                tone => Math.Abs(tone.Frequency - frequency) <= epsilon);
        }
    }
}
