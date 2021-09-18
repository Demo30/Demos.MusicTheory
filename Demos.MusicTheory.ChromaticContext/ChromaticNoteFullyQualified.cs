using Demos.MusicTheory.Abstractions.Commons;
using Demos.MusicTheory.Commons;
using System;

namespace Demos.MusicTheory.ChromaticContext
{
    public class ChromaticNoteFullyQualified : IContentEqual<ChromaticNoteFullyQualified>
    {
        public ChromaticNoteQuality Quality { get; }
        public NotationSymbols Modifier { get; } = NotationSymbols.None;
        public int Order { get; } = 0;
        
        public ChromaticNoteFullyQualified(ChromaticNoteQuality qualifier, int Order, NotationSymbols modifier)
        {
            CheckConstructionArgumentValidity(qualifier, Order, modifier);

            this.Quality = qualifier;
            this.Order = Order;
            this.Modifier = modifier;
        }

        public bool IsEqualByContent(ChromaticNoteFullyQualified comparedNote)
        {
            return
                comparedNote.Quality == this.Quality &&
                comparedNote.Modifier == this.Modifier &&
                comparedNote.Order == this.Order;
        }

        public override string ToString()
        {
            return $"{Quality}{Order}{GetModifierString(Modifier)}";
        }

        private string GetModifierString(NotationSymbols modifier)
        {
            return Modifier == NotationSymbols.None ? "" : Modifier.ToString();
        }

        private void CheckConstructionArgumentValidity(ChromaticNoteQuality quality, int octaveOrder, NotationSymbols modifier)
        {
            if (octaveOrder < 0)
            {
                throw new ArgumentException("Octave order cannot be negative.");
            }

            if (quality == default)
            {
                throw new ArgumentException("Chromatic note quality needs to be specified.");
            }

            if (!ChromaticNoteModifierSymbolVerifier.SymbolIsValidChromaticNoteModifier(modifier))
            {
                throw new ArgumentException("Notation symbol is none of accepted types.");
            }
        }
    }
}
