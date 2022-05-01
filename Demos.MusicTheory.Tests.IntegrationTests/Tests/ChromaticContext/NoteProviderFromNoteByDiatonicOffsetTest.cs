using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class NoteProviderFromNoteByDiatonicOffsetTest : TestBase
{
    private NoteProviderFromNoteByDiatonicOffset _provider = null!;

    [SetUp]
    public void SetUp()
    {
        RegisterService<ElementaryNoteFromDiatonicScaleKeySignatureProvider>();
        RegisterService<ElementaryNotesProviderFromDiatonicScale>();
        _provider = new NoteProviderFromNoteByDiatonicOffset();
    }

    [Test]
    [TestCaseSource(nameof(ShouldReturnNoteForGivenScaleAndOffsetDataTestCases))]
    public void ShouldReturnNoteForGivenScaleAndOffsetData(DiatonicScale scale, Note referenceNote, int diatonicSteps, Note expectedNote)
    {
        // Given

        // When
        var note = _provider.GetNote(scale, referenceNote, diatonicSteps);

        // Then
        note.Should().BeEquivalentTo(expectedNote);
    }

    static IEnumerable<TestCaseData> ShouldReturnNoteForGivenScaleAndOffsetDataTestCases()
    {
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.C, 1, NotationSymbols.None),
            1,
            new Note(NoteQuality.D, 1, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.C, 1, NotationSymbols.None),
            3,
            new Note(NoteQuality.F, 1, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.C, 1, NotationSymbols.None),
            7,
            new Note(NoteQuality.C, 2, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.C, 1, NotationSymbols.None),
            14,
            new Note(NoteQuality.C, 3, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.C, 2, NotationSymbols.None),
            7,
            new Note(NoteQuality.C, 3, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.C, 2, NotationSymbols.None),
            -7,
            new Note(NoteQuality.C, 1, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.E, 2, NotationSymbols.None),
            2,
            new Note(NoteQuality.G, 2, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.G, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.G, 2, NotationSymbols.None),
            7,
            new Note(NoteQuality.G, 3, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.G, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.G, 2, NotationSymbols.None),
            6,
            new Note(NoteQuality.F, 3, NotationSymbols.Sharp));
    }
    
    [Test]
    [TestCaseSource(nameof(ShouldThrowErrorWhenNoteOutsideTheScaleIsProvidedTestCases))]
    public void ShouldThrowErrorWhenNoteOutsideTheScaleIsProvided(DiatonicScale scale, Note referenceNote)
    {
        // Given

        // When
        var call = () => _provider.GetNote(scale, referenceNote, 1);

        // Then
        call.Should().Throw<ArgumentException>();
    }
    
    static IEnumerable<TestCaseData> ShouldThrowErrorWhenNoteOutsideTheScaleIsProvidedTestCases()
    {
        yield return new TestCaseData(
            new DiatonicScale(NoteQuality.G, NotationSymbols.None, DiatonicScaleType.Major),
            new Note(NoteQuality.F, 1, NotationSymbols.None));
    }
}