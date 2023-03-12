# Installation

- via package manager: *NuGet\Install-Package Demos.MusicTheory -Version 2.0.1*
- or just add using Manage NuGet Packages GUI in IDE

# Usage

- for your convenience, just copy-paste this code and run it right away! 🤩

```
using Demos.MusicTheory;
using Demos.MusicTheory.Setup;

// ✅ SETUP

// 🍕 Start by calling Setup method (call only once on startup)
MusicTheorySetup.Setup();

// ✅ NOTES

// 🍔 Create some notes
var noteCSharp4 = new Note(NoteQuality.C, NoteModifier.Sharp, 4);
var noteC4 = new Note(NoteQuality.C, NoteModifier.Natural, 4);
var noteCSharp5 = new Note(NoteQuality.C, NoteModifier.Sharp, 5);
var noteD4 = new Note(NoteQuality.D, NoteModifier.Natural, 4);
var noteE4 = new Note(NoteQuality.E, NoteModifier.Natural, 4);

// ✅ INTERVALS

// 🍟 Create some intervals
var minorSecond = new Interval(2, IntervalQuality.Minor);
var majorSecond = Interval.MajorSecond; // for convenience

// 🌭 Use intervals to find another note(s) from a reference note
var enharmonicNotes = noteC4.GetEnharmonicNotesByInterval(majorSecond, Direction.Right); // There are enharmonic equivalents of D4

// 🍿 Get enharmonic equivalents of a note
var enharmonicsOfD4 = noteD4.GetEnharmonics();

// 🧂 Move by semitones from note to get enharmonic notes fitting the movement
noteC4.GetEnharmonicNotesBySemitoneDistance(1, Direction.Right);

// 🥓 Find out what interval is between two notes
var majorSecondFromOtherNote = noteC4.GetIntervalFromOtherNote(noteD4);

// 🥚 Find out what different enharmonically equivalent intervals fit into common distance in semitones
var enharmonicIntervals = MusicTheoryService.Instance.GetIntervalsBySemitoneDistance(2);

// ✅ SCALES

// 🍳 Get basic note qualities of given scale
var scaleCMajor = new Scale(NoteQuality.C, NoteModifier.Natural, ScaleQuality.Major);
var noteQualitiesFittingCMajorScale = MusicTheoryService.Instance.GetElementaryNotesByScale(scaleCMajor);

// 🧇 Get fully qualified notes from a scale
var notes = MusicTheoryService.Instance.GetNotesByScale(scaleCMajor, 4);

// 🥞 Get a note distant by some diatonic degrees within a scale
var thirdFromC = noteC4.GetNoteByDiatonicStepsWithinScale(scaleCMajor, 2);

// 🧈 Get all such scales that contain all of the given set of notes or just elementary notes
var scalesWithCAndE = MusicTheoryService.Instance.GetScalesByNotes(new[] {noteC4, noteE4});
scalesWithCAndE = MusicTheoryService.Instance.GetScalesByElementaryNotes(new[] { (NoteQuality.C, NoteModifier.Natural), (NoteQuality.E, NoteModifier.Natural)});


// ✅ CHORDS

// 🍞 Get some chords
var cMajorChord = new Chord(new Note(NoteQuality.C, NoteModifier.Natural, 4), ChordQuality.MajorTriad);
var gMajorChord = new Chord(new Note(NoteQuality.G, NoteModifier.Natural, 4), ChordQuality.MajorTriad);

// 🥐 Get scales containing these chords
var scalesWithCMajorChord = cMajorChord.GetMatchingScales();


// ✅ MIDI
// 🥨 Get notes based on MIDI index value
var c4AndEnharmonicsFromMidi = MusicTheoryService.Instance.GetEnharmonicNotesFromMidiIndex(60);
```