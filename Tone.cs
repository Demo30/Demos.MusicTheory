using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.MusicTheory
{
    public class Tone
    {
        public double Frequency { get; }
        public ChromaticNote[] EnharmonicNotes  { get; }
        public int MidiMapping { get; set; }

        public Tone(double frequency, ChromaticNote[] enharmonicNotes, int midiMapping)
        {
            this.Frequency = frequency;
            this.EnharmonicNotes = enharmonicNotes;
            this.MidiMapping = midiMapping;
        }

        public bool IsEqual(Tone comparedNote)
        {
            comparedNote = comparedNote ?? throw new ArgumentNullException();
            return this.Frequency == comparedNote.Frequency;
        }
    }
}
