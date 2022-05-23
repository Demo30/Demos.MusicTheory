using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext;

public class ChordStructure
{
    public IReadOnlyList<ElementaryInterval> OrderedIntervalStructure { get; }

    public ChordStructure(IReadOnlyList<ElementaryInterval> orderedIntervalStructure)
    {
        if (orderedIntervalStructure is null || !orderedIntervalStructure.Any())
            throw new ArgumentException($"Parameter {nameof(orderedIntervalStructure)} cannot be null nor empty.");

        OrderedIntervalStructure = orderedIntervalStructure;
    }
}