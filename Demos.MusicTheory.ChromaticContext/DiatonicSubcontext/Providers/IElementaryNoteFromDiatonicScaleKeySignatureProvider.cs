using System.Collections.Generic;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;

internal interface IElementaryNoteFromDiatonicScaleKeySignatureProvider
{
    public IEnumerable<ElementaryNote> GetChromaticElementaryNotes(KeySignatures key);
}