using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteProviderFromIntervalRangeTest : TestBase
{
    private IntervalProviderFromNoteRange? _provider;

    [SetUp]
    public void SetUp()
    {
        RegisterService<IntervalProviderFromIndexSpan>();
        _provider = new IntervalProviderFromNoteRange();
    }

    [Theory]
    [TestCaseSource(nameof(GetTestCases))]
    public void ValidResults(
        NoteQualityInternal quality1, int octaveOrder1, NotationSymbols modifier1,
        NoteQualityInternal quality2, int octaveOrder2, NotationSymbols modifier2,
        IntervalInternal expectedIntervalInternal)
    {
        // Given
        var range = new NoteRangeInternal(
            new NoteInternal(quality1, octaveOrder1, modifier1),
            new NoteInternal(quality2, octaveOrder2, modifier2));

        // When
        var result = _provider!.GetInterval(range);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedIntervalInternal);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.C, 1,
            NotationSymbols.None, new IntervalInternal(1, IntervalQualityInternal.Perfect));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.C, 1,
            NotationSymbols.Sharp, new IntervalInternal(1, IntervalQualityInternal.Augmented));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.Sharp, NoteQualityInternal.C, 1,
            NotationSymbols.Sharp, new IntervalInternal(1, IntervalQualityInternal.Perfect));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.Sharp, NoteQualityInternal.D, 1,
            NotationSymbols.None, new IntervalInternal(2, IntervalQualityInternal.Minor));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.Sharp, NoteQualityInternal.D, 1,
            NotationSymbols.Sharp, new IntervalInternal(2, IntervalQualityInternal.Major));
        yield return new TestCaseData(NoteQualityInternal.D, 2, NotationSymbols.None, NoteQualityInternal.E, 2,
            NotationSymbols.None, new IntervalInternal(2, IntervalQualityInternal.Major));
        yield return new TestCaseData(NoteQualityInternal.E, 1, NotationSymbols.None, NoteQualityInternal.F, 1,
            NotationSymbols.None, new IntervalInternal(2, IntervalQualityInternal.Minor));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.C, 2,
            NotationSymbols.None, new IntervalInternal(8, IntervalQualityInternal.Perfect));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.D, 2,
            NotationSymbols.Flat, new IntervalInternal(9, IntervalQualityInternal.Minor));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.D, 2,
            NotationSymbols.None, new IntervalInternal(9, IntervalQualityInternal.Major));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.F, 1,
            NotationSymbols.None, new IntervalInternal(4, IntervalQualityInternal.Perfect));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.F, 2,
            NotationSymbols.None, new IntervalInternal(11, IntervalQualityInternal.Perfect));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.F, 2,
            NotationSymbols.Sharp, new IntervalInternal(11, IntervalQualityInternal.Augmented));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.G, 1,
            NotationSymbols.None, new IntervalInternal(5, IntervalQualityInternal.Perfect));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.G, 2,
            NotationSymbols.None, new IntervalInternal(12, IntervalQualityInternal.Perfect));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.G, 2,
            NotationSymbols.Sharp, new IntervalInternal(12, IntervalQualityInternal.Augmented));
        yield return new TestCaseData(NoteQualityInternal.C, 1, NotationSymbols.None, NoteQualityInternal.A, 2,
            NotationSymbols.None, new IntervalInternal(13, IntervalQualityInternal.Major));
    }
}