using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteProviderFromNoteByDiatonicOffsetTest : TestBase
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
    public void ShouldReturnNoteForGivenScaleAndOffsetData(DiatonicScale scale, NoteInternal referenceNoteInternal, int diatonicSteps, NoteInternal expectedNoteInternal)
    {
        // Given

        // When
        var note = _provider.GetNote(scale, referenceNoteInternal, diatonicSteps);

        // Then
        note.Should().BeEquivalentTo(expectedNoteInternal);
    }

    static IEnumerable<TestCaseData> ShouldReturnNoteForGivenScaleAndOffsetDataTestCases()
    {
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None),
            1,
            new NoteInternal(NoteQualityInternal.D, 1, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None),
            3,
            new NoteInternal(NoteQualityInternal.F, 1, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None),
            7,
            new NoteInternal(NoteQualityInternal.C, 2, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None),
            14,
            new NoteInternal(NoteQualityInternal.C, 3, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.C, 2, NotationSymbols.None),
            7,
            new NoteInternal(NoteQualityInternal.C, 3, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.C, 2, NotationSymbols.None),
            -7,
            new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.E, 2, NotationSymbols.None),
            2,
            new NoteInternal(NoteQualityInternal.G, 2, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.G, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.G, 2, NotationSymbols.None),
            7,
            new NoteInternal(NoteQualityInternal.G, 3, NotationSymbols.None));
        
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.G, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.G, 2, NotationSymbols.None),
            6,
            new NoteInternal(NoteQualityInternal.F, 3, NotationSymbols.Sharp));
    }
    
    [Test]
    [TestCaseSource(nameof(ShouldThrowErrorWhenNoteOutsideTheScaleIsProvidedTestCases))]
    public void ShouldThrowErrorWhenNoteOutsideTheScaleIsProvided(DiatonicScale scale, NoteInternal referenceNoteInternal)
    {
        // Given

        // When
        var call = () => _provider.GetNote(scale, referenceNoteInternal, 1);

        // Then
        call.Should().Throw<ArgumentException>();
    }
    
    static IEnumerable<TestCaseData> ShouldThrowErrorWhenNoteOutsideTheScaleIsProvidedTestCases()
    {
        yield return new TestCaseData(
            new DiatonicScale(NoteQualityInternal.G, NotationSymbols.None, DiatonicScaleType.Major),
            new NoteInternal(NoteQualityInternal.F, 1, NotationSymbols.None));
    }
}