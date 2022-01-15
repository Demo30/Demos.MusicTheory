using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext;

public class ChromaticSpaceIndexValidator : MusicalEntitySpaceValidator, IChromaticSpaceIndexValidator
{
    public override Type CompatibleType => typeof(IChromaticEntity);

    public override bool ValidateEntityCompatibility<T>(T entity, IEnumerable<T> compatibleEntities)
    {
        compatibleEntities = compatibleEntities.ToList();
        base.ValidateEntityCompatibility(entity, compatibleEntities);

        var chromaticNote = (IChromaticEntity)entity;
        var compatibleChromaticNotes = compatibleEntities.Select(e => (IChromaticEntity) e);
        var noConflictingNotes = !compatibleChromaticNotes.Any(note => note.ChromaticContextIndex.Equals(chromaticNote.ChromaticContextIndex));
        
        return noConflictingNotes ? true : throw new MusicalEntityValidatorException(this);
    }
}