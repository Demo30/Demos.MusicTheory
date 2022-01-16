using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.Commons;

public interface IMusicalEntitySpaceValidator
{
    Type CompatibleType { get; }

    bool ValidateEntityCompatibility<T>(T entity, IEnumerable<T> compatibleEntities) where T : MusicTheory.Commons.IMusicalEntity;
}