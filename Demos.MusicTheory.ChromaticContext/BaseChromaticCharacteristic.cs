using Demos.MusicTheory.Commons;
using System;

namespace Demos.MusicTheory.ChromaticContext
{
    public class BaseChromaticCharacteristic
    {
        public ChromaticNoteQuality Quality { get; private init; }
        public NotationSymbols Modifier
        {
            get => _modifier;
            private init 
            {
                _modifier = ChromaticNoteModifierSymbolVerifier.SymbolIsValidChromaticNoteModifier(value) ?
                    value :
                    throw new ArgumentException($"Provided notation symbol: \"{value}\" is not a valid symbol for a chromatic note.");
            }
        }

        public BaseChromaticCharacteristic(ChromaticNoteQuality quality, NotationSymbols modifier)
        {
            Quality = quality;
            Modifier = modifier;
        }

        private NotationSymbols _modifier = NotationSymbols.None;
    }
}
