using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.Commons;
using ChromaticNote = Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;

public class ChromaticNoteDiatonicScaleKeySignatureProvider
{
    public ChromaticNote.ChromaticNoteFullyQualified[] GetNotes(KeySignatures key, int count)
    {
        List<ChromaticNote.ChromaticNoteFullyQualified> notes = new();

        var order = -1;
        ChromaticNoteElementary[] characteristics = GetBaseCharacteristics(key).ToArray();
        for (int i = 0; i < count; i++)
        {
            var repeatingCharacteristicIndex = i % characteristics.Length;
            order += repeatingCharacteristicIndex == 0 ? 1 : 0;
            var characteristic = characteristics[repeatingCharacteristicIndex];
            var note = new ChromaticNote.ChromaticNoteFullyQualified(characteristic.Quality, order, characteristic.Modifier);
            notes.Add(note);
        }
        return notes.ToArray();
    }

    private IEnumerable<ChromaticNoteElementary> GetBaseCharacteristics(KeySignatures key)
    {
        DiatonicScaleKeySignatureBaseCharacteristicsProvider baseCharacteristicsProvider = new();
        return baseCharacteristicsProvider.GetBaseChromaticCharacteristic(key);
    }
}