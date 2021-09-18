using Demos.MusicTheory.Commons;
using System;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext
{
    public class ChromaticNoteElementary
    {
        public ChromaticNoteQuality Quality { get; private init; }
        public NotationSymbols Modifier
        {
            get => _modifier;
            private init 
            {
                _modifier = _relevantNotationSymbols.Contains(value) ?
                    value :
                    throw new ArgumentException($"Provided notation symbol: \"{value}\" is not a valid symbol for a chromatic note.");
            }
        }

        private NotationSymbols _modifier = NotationSymbols.None;

        public static NotationSymbols[] RelevantNotationSymbols => _relevantNotationSymbols;

        public ChromaticNoteElementary(ChromaticNoteQuality quality, NotationSymbols modifier)
        {
            Quality = quality;
            Modifier = modifier;
        }

        private static readonly NotationSymbols[] _relevantNotationSymbols = new NotationSymbols[]
        {
            NotationSymbols.None,
            NotationSymbols.Sharp,
            NotationSymbols.Flat,
            NotationSymbols.DoubleSharp,
            NotationSymbols.DoubleFlat
        };
    }
}
