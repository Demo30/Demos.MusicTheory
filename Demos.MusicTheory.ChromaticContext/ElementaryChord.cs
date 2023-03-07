using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext;

internal class ElementaryChord
{
    public ChordStructure MainChordStructure { get; }

    public ElementaryChord(ChordStructure mainChordStructure)
    {
        MainChordStructure = mainChordStructure ?? throw new ArgumentNullException(nameof(mainChordStructure));
    }

    public IEnumerable<ChordStructure> GetChordStructureInversions()
    {
        var inversions = new List<ChordStructure>();
        var degree = MainChordStructure.OrderedIntervalStructure.Count;
        var auxDegree = degree;
        
        while (auxDegree > 0)
        {
            var inversionStructure = new List<ElementaryInterval>();
            inversionStructure.AddRange(MainChordStructure.OrderedIntervalStructure.Skip(auxDegree).Take(degree - auxDegree));
            inversionStructure.AddRange(MainChordStructure.OrderedIntervalStructure.Take(auxDegree));
            inversions.Add(new ChordStructure(inversionStructure));
            auxDegree--;
        }
        
        return inversions;
    }
}