using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    internal static class DatabaseValuesCodec
    {
        public static NotationSymbols DecodeNotationSymbol(string value)
        {
            switch(value.ToUpper())
            {
                case "NONE": return NotationSymbols.None;
                case "FLAT": return NotationSymbols.Flat;
                case "SHARP": return NotationSymbols.Sharp;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static ChromaticNoteQuality DecodeElementaryChromaticNote(string value)
        {
            switch (value.ToUpper())
            {
                case "C": return ChromaticNoteQuality.C;
                case "D": return ChromaticNoteQuality.D;
                case "E": return ChromaticNoteQuality.E;
                case "F": return ChromaticNoteQuality.F;
                case "G": return ChromaticNoteQuality.G;
                case "A": return ChromaticNoteQuality.A;
                case "B": return ChromaticNoteQuality.B;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
