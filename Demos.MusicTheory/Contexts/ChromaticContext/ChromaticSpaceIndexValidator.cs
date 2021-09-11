using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Abstractions.Commons;
using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticSpaceIndexValidator : MusicalEntitySpaceValidator, IChromaticSpaceIndexValidator
    {
        public override Type CompatibleType => typeof(IChromaticNote);

        public override bool ValidateEntityCompatibility<T>(T entity, IEnumerable<T> compatibleEntities)
        {
            base.ValidateEntityCompatibility(entity, compatibleEntities);

            IChromaticNote chromaticNote = (IChromaticNote)entity;
            IEnumerable<IChromaticNote> compatibleChromaticNotes =
                compatibleEntities.Select(entity => (IChromaticNote)entity);

            bool conflictingIndexesExist = compatibleChromaticNotes.Count(note => note.ChromaticContextIndex.Equals(chromaticNote.ChromaticContextIndex)) > 0;
            if (conflictingIndexesExist)
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
