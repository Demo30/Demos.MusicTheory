using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.Commons;

public abstract class MusicalEntitySpaceValidator : IMusicalEntitySpaceValidator
{
    public abstract Type CompatibleType { get; }

    public virtual bool ValidateEntityCompatibility<T>(T entity, IEnumerable<T> compatibleEntities) where T : IMusicalEntity
    {
        return CheckCompatibility(typeof(T));
    }

    private bool CheckCompatibility(Type entityType)
    {
        bool compatible =
            CompatibleType.IsAssignableFrom(entityType);

        return compatible ? true : throw new ArgumentException("Incompatible musical entity type.");
    }
}