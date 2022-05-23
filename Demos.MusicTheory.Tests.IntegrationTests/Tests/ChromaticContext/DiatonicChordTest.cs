using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class DiatonicChordTest : TestBase
{
    [SetUp]
    protected void SetUp()
    {
        RegisterService<NoteProviderFromIndex>();
        RegisterService<NoteProviderFromNoteByInterval>();
    }

    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void DiatonicChordMainNotesTest(DiatonicChord diatonicChord, List<Note> expectedMainChordNotes) // TODO TW: just first check
    {
        // Given
        
        //When
        var notes = diatonicChord.ChordNotes;
        
        // Then
        notes.Should().BeEquivalentTo(expectedMainChordNotes);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(
            new DiatonicChord(new Note(NoteQuality.C, 1, NotationSymbols.None), DiatonicChordQuality.MajorTriad),
            new List<Note>
            {
                new (NoteQuality.C, 1, NotationSymbols.None),
                new (NoteQuality.E, 1, NotationSymbols.None),
                new (NoteQuality.G, 1, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new Note(NoteQuality.C, 1, NotationSymbols.Sharp), DiatonicChordQuality.MajorTriad),
            new List<Note>
            {
                new (NoteQuality.C, 1, NotationSymbols.Sharp),
                new (NoteQuality.E, 1, NotationSymbols.Sharp),
                new (NoteQuality.G, 1, NotationSymbols.Sharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new Note(NoteQuality.D, 1, NotationSymbols.Flat), DiatonicChordQuality.MajorTriad),
            new List<Note>
            {
                new (NoteQuality.D, 1, NotationSymbols.Flat),
                new (NoteQuality.F, 1, NotationSymbols.None),
                new (NoteQuality.A, 1, NotationSymbols.Flat)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new Note(NoteQuality.D, 1, NotationSymbols.None), DiatonicChordQuality.MajorTriad),
            new List<Note>
            {
                new (NoteQuality.D, 1, NotationSymbols.None),
                new (NoteQuality.F, 1, NotationSymbols.Sharp),
                new (NoteQuality.A, 1, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new Note(NoteQuality.D, 1, NotationSymbols.Sharp), DiatonicChordQuality.MajorTriad),
            new List<Note>
            {
                new (NoteQuality.D, 1, NotationSymbols.Sharp),
                new (NoteQuality.F, 1, NotationSymbols.DoubleSharp),
                new (NoteQuality.A, 1, NotationSymbols.Sharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new Note(NoteQuality.C, 1, NotationSymbols.None), DiatonicChordQuality.MinorTriad),
            new List<Note>
            {
                new (NoteQuality.C, 1, NotationSymbols.None),
                new (NoteQuality.E, 1, NotationSymbols.Flat),
                new (NoteQuality.G, 1, NotationSymbols.None)
            });
    }
}