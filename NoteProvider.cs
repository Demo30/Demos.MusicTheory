using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using Demos.DemosHelpers;

namespace Demos.MusicTheory
{
    public class NoteProvider
    {
        public static NoteProvider Instance
        {
            get
            {
                if (NoteProvider._instance == null)
                {
                    NoteProvider._instance = new NoteProvider();
                }

                return NoteProvider._instance;
            }
        }

        public Tone[] AllTones { get { return this._allTones; } }

        private Tone[] _allTones = null;

        private static NoteProvider _instance = null;

        private NoteProvider()
        {
            this.InitializeTones();
        }

        public Tone GetTone(ChromaticNote chromaticNote)
        {
            foreach (Tone curTone in this._allTones)
            {
                foreach (ChromaticNote curChNote in curTone.EnharmonicNotes)
                {
                    if (curChNote.CompareTo(chromaticNote))
                    {
                        return curTone;
                    }
                }
            }

            return null;
        }

        public ChromaticNote[] GetNotesByStaffPosition(int staffPositionIndex)
        {
            List<ChromaticNote> notes = new List<ChromaticNote>();
            foreach(Tone curTone in this._allTones)
            {
                foreach(ChromaticNote note in curTone.EnharmonicNotes)
                {
                    if (note.StaffPositionIndex == staffPositionIndex && !this.DoesSetContainNote(notes, note))
                    {

                        notes.Add(note);
                    }
                }
            }

            return notes.ToArray();
        }

        public Tone GetToneByMidiIndex(int midiIndex)
        {
            foreach (Tone curTone in this._allTones)
            {
                if (curTone.MidiMapping == midiIndex)
                {
                    return curTone;
                }
            }

            return null;
        }

        public Tuple<ElementaryChromaticNotes, NotationSymbols>[] GetNotesFromKeySignature(KeySignatures key)
        {
            List<Tuple<ElementaryChromaticNotes, NotationSymbols>> notes = new List<Tuple<ElementaryChromaticNotes, NotationSymbols>>();

            switch(key)
            {
                case KeySignatures.Simple:
                    break;
                case KeySignatures.Flats_1:
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.B, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_2:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Flats_1));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.E, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_3:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Flats_2));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.A, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_4:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Flats_3));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.D, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_5:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Flats_4));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.G, NotationSymbols.Flat));
                    break;
                case KeySignatures.Flats_6:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Flats_5));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.C, NotationSymbols.Flat));
                    break;
                case KeySignatures.Sharps_1:
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.F, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_2:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Sharps_1));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.C, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_3:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Sharps_2));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.G, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_4:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Sharps_3));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.D, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_5:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Sharps_4));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.A, NotationSymbols.Sharp));
                    break;
                case KeySignatures.Sharps_6:
                    notes.AddRange(this.GetNotesFromKeySignature(KeySignatures.Sharps_5));
                    notes.Add(new Tuple<ElementaryChromaticNotes, NotationSymbols>(ElementaryChromaticNotes.E, NotationSymbols.Sharp));
                    break;
                default:
                    throw new Exception();
            }

            return notes.ToArray();
        }

        public bool DoesSetContainTone(ICollection<Tone> set, Tone note)
        {
            if (set == null || set.Count == 0)
            {
                return false;
            }

            foreach (Tone curNote in set)
            {
                if (curNote.IsSameTone(note))
                {
                    return true;
                }
            }

            return false;
        }

        public bool DoesSetContainTone(ICollection<ChromaticNote> set, Tone tone)
        {
            foreach (ChromaticNote curChrNote in set)
            {
                foreach (ChromaticNote curChrNote2 in tone.EnharmonicNotes)
                {
                    if (curChrNote.CompareTo(curChrNote2))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool DoesSetContainNote(ICollection<Tone> set, ChromaticNote note)
        {
            foreach(Tone curTone in set)
            {
                foreach(ChromaticNote curChrNote in curTone.EnharmonicNotes)
                {
                    if (curChrNote.CompareTo(note))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool DoesSetContainNote(ICollection<ChromaticNote> set, ChromaticNote note)
        {
            foreach (ChromaticNote curNote in set)
            {
                if (curNote.CompareTo(note))
                {
                    return true;
                }
            }

            return false;
        }

        public ChromaticNote GetChromaticNote(ElementaryChromaticNotes elNote, int octaveOrder, NotationSymbols modifier)
        {
            foreach(Tone curTone in this._allTones)
            {
                foreach(ChromaticNote chromaticNote in curTone.EnharmonicNotes)
                {
                    bool conds =
                        chromaticNote.ElementaryNote == elNote &&
                        chromaticNote.OctaveOrder == octaveOrder &&
                        chromaticNote.NoteModifierSymbol == modifier;
                    
                    if (conds)
                    {
                        return chromaticNote;
                    }
                }
            }

            return null;
        }


        private void InitializeTones()
        {
            DataTable data = null;

            using (IDbConnection conn = new SQLiteConnection("Data Source=MusicTheory.db;Version=3;"))
            {
                conn.Open();

                string query = @"
                    Select c.ID, e.name ELEMENTARY_NOTE, m.name MODIFIER, c.OCTAVE_ORDER, t.FREQUENCY, c.OVERALL_ORDER, t.MIDI_MAPPING
                    From ChromaticNote c
                    join Tone t on t.id = c.ID_TONE
                    join ElementaryChromaticNote e on e.id = c.ID_ELEMENTARY_CHROMATIC_NOTE
                    join NoteModifier m on m.id = c.ID_NOTE_MODIFIER
                    Order by c.OCTAVE_ORDER asc, e.id";

                data = UnifiedDatabaseHelperClass.GetResultsDataTable(query, conn);
            }

            List<Tone> tones = new List<Tone>();

            foreach(DataRow dr in data.Rows)
            {
                double frequency = Double.Parse(dr["FREQUENCY"].ToString());
                int octaveOrder = dr["OCTAVE_ORDER"].ToInt32(GeneralHelperClass.ToInt32ConversionTypes.IntOrException);
                ElementaryChromaticNotes elementaryNote = DatabaseValuesCodec.DecodeElementaryChromaticNote(dr["ELEMENTARY_NOTE"].ToString());
                NotationSymbols modifier = DatabaseValuesCodec.DecodeNotationSymbol(dr["MODIFIER"].ToString());
                int overallOrder = dr["OVERALL_ORDER"].ToInt32(GeneralHelperClass.ToInt32ConversionTypes.IntOrException);
                int midiMapping = dr["MIDI_MAPPING"].ToInt32(GeneralHelperClass.ToInt32ConversionTypes.IntOrException);

                ChromaticNote curNote = new ChromaticNote(elementaryNote, octaveOrder, modifier, overallOrder);

#warning there can be more rows for one tone...currently not supported....
                Tone curTone = new Tone(frequency, new ChromaticNote[] { curNote }, midiMapping);

                tones.Add(curTone);
            }


            this._allTones = tones.ToArray();
        }
    }
}
