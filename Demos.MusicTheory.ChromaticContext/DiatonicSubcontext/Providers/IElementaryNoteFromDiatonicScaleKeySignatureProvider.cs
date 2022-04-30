using System.Collections.Generic;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

public interface IElementaryNoteFromDiatonicScaleKeySignatureProvider
{
    public IEnumerable<ElementaryNote> GetChromaticElementaryNotes(KeySignatures key);
}