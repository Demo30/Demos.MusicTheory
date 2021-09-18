using Demos.MusicTheory.Commons;
using System;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext
{
    // IoC?? Spaghetti??
    public static class ChromaticNoteModifierSymbolVerifier
    {
        public static bool SymbolIsValidChromaticNoteModifier(NotationSymbols symbol) => _relevantNotationSymbols.Contains(symbol);

        private static NotationSymbols[] _relevantNotationSymbols = new NotationSymbols[]
        {
            NotationSymbols.None,
            NotationSymbols.Sharp,
            NotationSymbols.Flat,
            NotationSymbols.DoubleSharp,
            NotationSymbols.DoubleFlat
        };
    }
}
