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

        public static ElementaryChromaticNotes DecodeElementaryChromaticNote(string value)
        {
            switch (value.ToUpper())
            {
                case "C": return ElementaryChromaticNotes.C;
                case "D": return ElementaryChromaticNotes.D;
                case "E": return ElementaryChromaticNotes.E;
                case "F": return ElementaryChromaticNotes.F;
                case "G": return ElementaryChromaticNotes.G;
                case "A": return ElementaryChromaticNotes.A;
                case "B": return ElementaryChromaticNotes.B;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
