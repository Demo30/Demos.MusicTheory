﻿using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticSpaceIndexValidator : MusicalEntitySpaceValidator, IChromaticSpaceIndexValidator
    {
        public override Type CompatibleType => typeof(IChromaticEntity);

        public override bool ValidateEntityCompatibility<T>(T entity, IEnumerable<T> compatibleEntities)
        {
            base.ValidateEntityCompatibility(entity, compatibleEntities);

            IChromaticEntity chromaticNote = (IChromaticEntity)entity;
            IEnumerable<IChromaticEntity> compatibleChromaticNotes =
                compatibleEntities.Select(entity => (IChromaticEntity)entity);

            bool noConflictingNotes = compatibleChromaticNotes
                .Count(note => note.ChromaticContextIndex.Equals(chromaticNote.ChromaticContextIndex))
                .Equals(0);
            
            return noConflictingNotes ? true : throw new MusicalEntityValidatorException(this);
        }
    }
}
