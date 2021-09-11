using Demos.MusicTheory.Commons;
using System;
using System.Linq;

namespace Demos.MusicTheory
{
    [Obsolete]
    public class ChromaticNote : IContentEqual<ChromaticNote>
    {
        public ElementaryChromaticNotes ElementaryNote { get; }
        public int OctaveOrder { get; } = 0;
        public NotationSymbols NoteModifierSymbol { get; } = NotationSymbols.None;
        
        public int StaffPositionIndex { get; } // This is not really a chromatic note property

        //public Tone Tone { get; } // freq?

        public ChromaticNote(ElementaryChromaticNotes elementaryNote, int octaveOrder, NotationSymbols modifier, int staffPosition)
        {
            CheckConstructionArgumentValidity(elementaryNote, octaveOrder, modifier, staffPosition);

            this.ElementaryNote = elementaryNote;
            this.OctaveOrder = octaveOrder;
            this.NoteModifierSymbol = modifier;
            this.StaffPositionIndex = staffPosition;
        }

        public bool IsEqualByContent(ChromaticNote comparedNote)
        {
            return
                comparedNote.ElementaryNote == this.ElementaryNote &&
                comparedNote.NoteModifierSymbol == this.NoteModifierSymbol &&
                comparedNote.OctaveOrder == this.OctaveOrder;
        }

        public override string ToString()
        {
            string modifier = NoteModifierSymbol == NotationSymbols.None ? "" : NoteModifierSymbol.ToString();
            return $"{ElementaryNote}{OctaveOrder}{modifier}";
        }

        private NotationSymbols[] GetRelevantNotationSymbols() => new NotationSymbols[] { NotationSymbols.Sharp, NotationSymbols.Flat, NotationSymbols.None };

        private void CheckConstructionArgumentValidity(ElementaryChromaticNotes elementaryNote, int octaveOrder, NotationSymbols modifier, int staffPosition)
        {
            if (octaveOrder < 0)
            {
                throw new ArgumentException("Octave order cannot be negative.");
            }

            if (staffPosition < 0)
            {
                throw new ArgumentException("Staff position index cannot be negative.");
            }

            NotationSymbols[] acceptedSymbols = GetRelevantNotationSymbols();
            if (!acceptedSymbols.Contains(modifier))
            {
                throw new ArgumentException("Notation symbol is none of accepted types.");
            }
        }
    }
}
