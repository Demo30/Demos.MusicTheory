using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Commons;
using ChromaticNote = Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

public class ChromaticNoteDiatonicScaleKeySignatureProvider
{
    public ChromaticNote.ChromaticNoteFullyQualified[] GetNotes(KeySignatures key, int count)
    {
        // This can possibly be implemented using the chromatic index provider and precomputed map instead
        
        List<ChromaticNote.ChromaticNoteFullyQualified> notes = new();

        var order = -1;
        var characteristics = GetBaseCharacteristics(key).ToArray();
        for (var i = 0; i < count; i++)
        {
            var repeatingCharacteristicIndex = i % characteristics.Length;
            order += repeatingCharacteristicIndex == 0 ? 1 : 0;
            var characteristic = characteristics[repeatingCharacteristicIndex];
            var note = new ChromaticNote.ChromaticNoteFullyQualified(characteristic.Quality, order, characteristic.Modifier);
            notes.Add(note);
        }
        return notes.ToArray();
    }

    private static IEnumerable<ChromaticNoteElementary> GetBaseCharacteristics(KeySignatures key)
    {
        var baseCharacteristicsProvider = new DiatonicScaleKeySignatureBaseCharacteristicsProvider();
        return baseCharacteristicsProvider.GetBaseChromaticCharacteristic(key);
    }
}