using Demos.MusicTheory.Abstractions.PhysicalContext;
using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using ITone = Demos.MusicTheory.PhysicalContext.ITone;
using IToneSpaceFrequencyValidator = Demos.MusicTheory.PhysicalContext.IToneSpaceFrequencyValidator;

namespace Demos.MusicTheory.Contexts.PhysicalContext
{
    public class ToneSpaceFrequencyValidator : MusicalEntitySpaceValidator, IToneSpaceFrequencyValidator
    {
        public const double ComparisonEpsilon = 0.5;

        public override Type CompatibleType => typeof(ITone);

        public override bool ValidateEntityCompatibility<T>(T entity, IEnumerable<T> compatibleEntities)
        {
            base.ValidateEntityCompatibility(entity, compatibleEntities);

            ITone tone = (ITone)entity;
            IEnumerable<ITone> compatibleChromaticNotes =
                compatibleEntities.Select(entity => (ITone)entity);

            bool conflictingFrequenciesExist = compatibleChromaticNotes.Count(
                note => Math.Abs(note.Frequency - tone.Frequency) <= ComparisonEpsilon) > 0;

            if (conflictingFrequenciesExist)
            {
                throw new MusicalEntityValidatorException(this);
            }
            else
            {
                return true;
            }
        }

    }
}
