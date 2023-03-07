using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteProviderFromIndexTest : TestBase
{
    private NoteProviderFromIndex _provider = null!;

    [SetUp]
    public void SetUp()
    {
        _provider = new NoteProviderFromIndex();
    }

    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void GivenChromaticIndex_EnharmonicsShouldContainExpectedNotes(int chromaticIndex, Note expectedNote)
    {
        // When
        var enharmonics = _provider.GetEnharmonics(chromaticIndex);
        
        // Then
        enharmonics.Notes.Should().ContainEquivalentOf(expectedNote);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        const int baseOffset = 0;
        
        yield return new TestCaseData(baseOffset + 0, new Note(NoteQuality.C, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 0, new Note(NoteQuality.D, 0, NotationSymbols.DoubleFlat));
        yield return new TestCaseData(baseOffset + 1, new Note(NoteQuality.C, 0, NotationSymbols.Sharp));
        yield return new TestCaseData(baseOffset + 1, new Note(NoteQuality.D, 0, NotationSymbols.Flat));
        yield return new TestCaseData(baseOffset + 7, new Note(NoteQuality.G, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 7, new Note(NoteQuality.F, 0, NotationSymbols.DoubleSharp));
        yield return new TestCaseData(baseOffset + 9, new Note(NoteQuality.A, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 9, new Note(NoteQuality.B, 0, NotationSymbols.DoubleFlat));
        yield return new TestCaseData(baseOffset + 9, new Note(NoteQuality.G, 0, NotationSymbols.DoubleSharp));
        yield return new TestCaseData(baseOffset + 11, new Note(NoteQuality.B, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 11, new Note(NoteQuality.C, 1, NotationSymbols.Flat));
        yield return new TestCaseData(baseOffset + 11, new Note(NoteQuality.A, 0, NotationSymbols.DoubleSharp));
        yield return new TestCaseData(baseOffset + 12, new Note(NoteQuality.C, 1, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 21, new Note(NoteQuality.A, 1, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 22, new Note(NoteQuality.B, 1, NotationSymbols.Flat));
        yield return new TestCaseData(baseOffset + 22, new Note(NoteQuality.A, 1, NotationSymbols.Sharp));
        yield return new TestCaseData(baseOffset + 22, new Note(NoteQuality.C, 2, NotationSymbols.DoubleFlat));
        yield return new TestCaseData(baseOffset + 33, new Note(NoteQuality.A, 2, NotationSymbols.None));
    }
}