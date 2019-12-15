using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    public class ChromaticNote
    {
        public ElementaryChromaticNotes ElementaryNote { get; }
        public int OctaveOrder { get; set; } = 0;
        public NotationSymbols NoteModifierSymbol { get; } = NotationSymbols.None;
        public int StaffPositionIndex { get; set; }

        //public Tone Tone { get; } // freq?

        public ChromaticNote(ElementaryChromaticNotes elementaryNote, int octaveOrder, NotationSymbols modifier, int staffPosition)
        {
            if (octaveOrder < 0)
            {
                throw new ArgumentException("Octave order cannot be negative.");
            }

            if (staffPosition < 0)
            {
                throw new ArgumentException("Staff position index cannot be negative.");
            }

            NotationSymbols[] acceptedSymbols = new NotationSymbols[] { NotationSymbols.Sharp, NotationSymbols.Flat, NotationSymbols.None };

            if (!acceptedSymbols.Contains(modifier))
            {
                throw new ArgumentException("Notation symbol is none of accepted types.");
            }

            this.ElementaryNote = elementaryNote;
            this.OctaveOrder = octaveOrder;
            this.NoteModifierSymbol = modifier;
            this.StaffPositionIndex = staffPosition;
        }

        public bool CompareTo(ChromaticNote comparedNote)
        {
            bool conds =
                comparedNote.ElementaryNote == this.ElementaryNote &&
                comparedNote.NoteModifierSymbol == this.NoteModifierSymbol &&
                comparedNote.OctaveOrder == this.OctaveOrder;

            return conds;
        }

        public override string ToString()
        {
            string modifier = this.NoteModifierSymbol == NotationSymbols.None ? "" : this.NoteModifierSymbol.ToString();
            return $"{this.ElementaryNote.ToString()}{this.OctaveOrder.ToString()}{modifier}";
        }
    }
}
