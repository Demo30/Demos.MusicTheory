using System;
using System.Collections.Generic;
using System.Linq;
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
                _instance = _instance ?? new NoteProvider();
                return _instance;
            }
        }

        public Tone[] AllTones { get { return this._allTones; } }

        private Tone[] _allTones = null;

        private static NoteProvider _instance = null;

        private NoteProvider()
        {
            this.InitializeTones();
        }

        public Tone GetTone(ChromaticNote searchedChromaticNote) =>
            _allTones
                .Where(tone => tone.EnharmonicNotes.Any(chromatic => chromatic.IsEqualByContent(searchedChromaticNote)))
                .FirstOrDefault();

        public ChromaticNote[] GetNotesByStaffPosition(int staffPositionIndex) =>
            _allTones
                .SelectMany(tone => tone.EnharmonicNotes)
                .Where(chromatic => chromatic.StaffPositionIndex == staffPositionIndex)
                .Distinct()
                .ToArray();

        public Tone GetToneByMidiIndex(int midiIndex) => 
            _allTones
            .Where(tone => tone.MidiMapping == midiIndex)
            .FirstOrDefault();

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

        public bool DoesSetContainTone(ICollection<Tone> set, Tone note) => (set ?? new Tone[0]).Any(currentNote => currentNote.IsEqual(note));

        public bool DoesSetContainTone(ICollection<ChromaticNote> set, Tone tone) => (set ?? new ChromaticNote[0]).Any(currentChromaticNote => tone.EnharmonicNotes.Any(enharmonicNote => enharmonicNote.IsEqualByContent(currentChromaticNote)));

        public bool DoesSetContainNote(ICollection<Tone> set, ChromaticNote note) => (set ?? new Tone[0]).SelectMany(tone => tone.EnharmonicNotes).Any(chromaticTone => chromaticTone.IsEqualByContent(note));

        public bool DoesSetContainNote(ICollection<ChromaticNote> set, ChromaticNote note) => (set ?? new ChromaticNote[0]).Any(currentNote => currentNote.IsEqualByContent(note));

        public ChromaticNote GetChromaticNote(ElementaryChromaticNotes elNote, int octaveOrder, NotationSymbols modifier)
        {
            Func<ChromaticNote, bool> matchingExpression = (enharmonic) =>
                    enharmonic.ElementaryNote == elNote &&
                    enharmonic.OctaveOrder == octaveOrder &&
                    enharmonic.NoteModifierSymbol == modifier;
            return _allTones.SelectMany(tone => tone.EnharmonicNotes).Where(matchingExpression).SingleOrDefault();
        }

        private void InitializeTones()
        {
            this._allTones = GetAllTones();
        }

        private Tone[] GetAllTones()
        {
            DataTable data = null;

            using (IDbConnection conn = GetMusicTheoryDbConnection())
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

            int dataRowsCount = data.Rows.Count;
            Tone[] tones = new Tone[dataRowsCount];
            
            for(int i = 0; i < dataRowsCount; i++)
            {
                DataRow dataRow = data.Rows[i];

                double frequency = Double.Parse(dataRow["FREQUENCY"].ToString());
                int octaveOrder = dataRow["OCTAVE_ORDER"].ToInt32(GeneralHelperClass.ToInt32ConversionTypes.IntOrException);
                ElementaryChromaticNotes elementaryNote = DatabaseValuesCodec.DecodeElementaryChromaticNote(dataRow["ELEMENTARY_NOTE"].ToString());
                NotationSymbols modifier = DatabaseValuesCodec.DecodeNotationSymbol(dataRow["MODIFIER"].ToString());
                int overallOrder = dataRow["OVERALL_ORDER"].ToInt32(GeneralHelperClass.ToInt32ConversionTypes.IntOrException);
                int midiMapping = dataRow["MIDI_MAPPING"].ToInt32(GeneralHelperClass.ToInt32ConversionTypes.IntOrException);

                ChromaticNote curNote = new ChromaticNote(elementaryNote, octaveOrder, modifier, overallOrder);
#warning TODO there can be more rows for one tone...currently not supported....
                Tone currentTone = new Tone(frequency, new ChromaticNote[] { curNote }, midiMapping);

                tones[i] = currentTone;
            }

            return tones.ToArray();
        }

        private IDbConnection GetMusicTheoryDbConnection() => new SQLiteConnection("Data Source=MusicTheory.db;Version=3;");
    }
}
