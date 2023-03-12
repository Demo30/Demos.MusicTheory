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
    public void GivenChromaticIndex_EnharmonicsShouldContainExpectedNotes(int chromaticIndex, NoteInternal expectedNoteInternal)
    {
        // When
        var enharmonics = _provider.GetEnharmonics(chromaticIndex);
        
        // Then
        enharmonics.Notes.Should().ContainEquivalentOf(expectedNoteInternal);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        const int baseOffset = 0;
        
        yield return new TestCaseData(baseOffset + 0, new NoteInternal(NoteQualityInternal.C, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 0, new NoteInternal(NoteQualityInternal.D, 0, NotationSymbols.DoubleFlat));
        yield return new TestCaseData(baseOffset + 1, new NoteInternal(NoteQualityInternal.C, 0, NotationSymbols.Sharp));
        yield return new TestCaseData(baseOffset + 1, new NoteInternal(NoteQualityInternal.D, 0, NotationSymbols.Flat));
        yield return new TestCaseData(baseOffset + 7, new NoteInternal(NoteQualityInternal.G, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 7, new NoteInternal(NoteQualityInternal.F, 0, NotationSymbols.DoubleSharp));
        yield return new TestCaseData(baseOffset + 9, new NoteInternal(NoteQualityInternal.A, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 9, new NoteInternal(NoteQualityInternal.B, 0, NotationSymbols.DoubleFlat));
        yield return new TestCaseData(baseOffset + 9, new NoteInternal(NoteQualityInternal.G, 0, NotationSymbols.DoubleSharp));
        yield return new TestCaseData(baseOffset + 11, new NoteInternal(NoteQualityInternal.B, 0, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 11, new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.Flat));
        yield return new TestCaseData(baseOffset + 11, new NoteInternal(NoteQualityInternal.A, 0, NotationSymbols.DoubleSharp));
        yield return new TestCaseData(baseOffset + 12, new NoteInternal(NoteQualityInternal.C, 1, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 21, new NoteInternal(NoteQualityInternal.A, 1, NotationSymbols.None));
        yield return new TestCaseData(baseOffset + 22, new NoteInternal(NoteQualityInternal.B, 1, NotationSymbols.Flat));
        yield return new TestCaseData(baseOffset + 22, new NoteInternal(NoteQualityInternal.A, 1, NotationSymbols.Sharp));
        yield return new TestCaseData(baseOffset + 22, new NoteInternal(NoteQualityInternal.C, 2, NotationSymbols.DoubleFlat));
        yield return new TestCaseData(baseOffset + 33, new NoteInternal(NoteQualityInternal.A, 2, NotationSymbols.None));
    }
}