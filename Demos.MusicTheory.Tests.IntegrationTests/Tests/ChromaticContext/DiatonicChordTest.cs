using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
internal class DiatonicChordTest : TestBase
{
    [SetUp]
    protected void SetUp()
    {
        RegisterService<NoteProviderFromIndex>();
        RegisterService<NoteProviderFromNoteBySpan>();
    }

    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void DiatonicChordMainNotesTest(DiatonicChord diatonicChord, List<NoteInternal> expectedMainChordNotes)
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
            new DiatonicChord(new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None), DiatonicChordQuality.MajorTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.C, 1, NotationSymbols.None),
                new (NoteQualityInternal.E, 1, NotationSymbols.None),
                new (NoteQualityInternal.G, 1, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.Sharp), DiatonicChordQuality.MajorTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.C, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.E, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.G, 1, NotationSymbols.Sharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.D, 1, NotationSymbols.Flat), DiatonicChordQuality.MajorTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.D, 1, NotationSymbols.Flat),
                new (NoteQualityInternal.F, 1, NotationSymbols.None),
                new (NoteQualityInternal.A, 1, NotationSymbols.Flat)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.D, 1, NotationSymbols.None), DiatonicChordQuality.MajorTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.D, 1, NotationSymbols.None),
                new (NoteQualityInternal.F, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.A, 1, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.D, 1, NotationSymbols.Sharp), DiatonicChordQuality.MajorTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.D, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.F, 1, NotationSymbols.DoubleSharp),
                new (NoteQualityInternal.A, 1, NotationSymbols.Sharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None), DiatonicChordQuality.MinorTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.C, 1, NotationSymbols.None),
                new (NoteQualityInternal.E, 1, NotationSymbols.Flat),
                new (NoteQualityInternal.G, 1, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None), DiatonicChordQuality.DiminishedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.C, 1, NotationSymbols.None),
                new (NoteQualityInternal.E, 1, NotationSymbols.Flat),
                new (NoteQualityInternal.G, 1, NotationSymbols.Flat)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.D, 1, NotationSymbols.None), DiatonicChordQuality.DiminishedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.D, 1, NotationSymbols.None),
                new (NoteQualityInternal.F, 1, NotationSymbols.None),
                new (NoteQualityInternal.A, 1, NotationSymbols.Flat)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.E, 1, NotationSymbols.Sharp), DiatonicChordQuality.DiminishedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.E, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.G, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.B, 1, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.E, 1, NotationSymbols.Sharp), DiatonicChordQuality.DiminishedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.E, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.G, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.B, 1, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.G, 1, NotationSymbols.None), DiatonicChordQuality.DiminishedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.G, 1, NotationSymbols.None),
                new (NoteQualityInternal.B, 1, NotationSymbols.Flat),
                new (NoteQualityInternal.D, 2, NotationSymbols.Flat)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.A, 1, NotationSymbols.Sharp), DiatonicChordQuality.DiminishedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.A, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.C, 2, NotationSymbols.Sharp),
                new (NoteQualityInternal.E, 2, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.F, 1, NotationSymbols.Sharp), DiatonicChordQuality.DiminishedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.F, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.A, 1, NotationSymbols.None),
                new (NoteQualityInternal.C, 2, NotationSymbols.None)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None), DiatonicChordQuality.AugmentedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.C, 1, NotationSymbols.None),
                new (NoteQualityInternal.E, 1, NotationSymbols.None),
                new (NoteQualityInternal.G, 1, NotationSymbols.Sharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.D, 1, NotationSymbols.Sharp), DiatonicChordQuality.AugmentedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.D, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.F, 1, NotationSymbols.DoubleSharp),
                new (NoteQualityInternal.A, 1, NotationSymbols.DoubleSharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.F, 1, NotationSymbols.None), DiatonicChordQuality.AugmentedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.F, 1, NotationSymbols.None),
                new (NoteQualityInternal.A, 1, NotationSymbols.None),
                new (NoteQualityInternal.C, 2, NotationSymbols.Sharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.A, 1, NotationSymbols.None), DiatonicChordQuality.AugmentedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.A, 1, NotationSymbols.None),
                new (NoteQualityInternal.C, 2, NotationSymbols.Sharp),
                new (NoteQualityInternal.E, 2, NotationSymbols.Sharp)
            });
        
        yield return new TestCaseData(
            new DiatonicChord(new NoteInternal(NoteQualityInternal.B, 1, NotationSymbols.Sharp), DiatonicChordQuality.AugmentedTriad),
            new List<NoteInternal>
            {
                new (NoteQualityInternal.B, 1, NotationSymbols.Sharp),
                new (NoteQualityInternal.D, 2, NotationSymbols.DoubleSharp),
                new (NoteQualityInternal.F, 2, NotationSymbols.TripleSharp)
            });
    }
}