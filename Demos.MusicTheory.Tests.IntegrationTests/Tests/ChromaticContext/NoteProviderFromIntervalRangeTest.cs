using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class NoteProviderFromIntervalRangeTest : TestBase
{
    private IntervalProviderFromNoteRange? _provider;

    [SetUp]
    public void SetUp()
    {
        Services.ServicesManager.ServicesProvider.RegisterService(() =>
            new IntervalProviderFromIndexSpan());
        _provider = new IntervalProviderFromNoteRange();
    }

    [TearDown]
    public void TearDown()
    {
        Services.ServicesManager.ResetServiceProvider();
    }

    [Theory]
    [TestCaseSource(nameof(GetTestCases))]
    public void ValidResults(
        NoteQuality quality1, int octaveOrder1, NotationSymbols modifier1,
        NoteQuality quality2, int octaveOrder2, NotationSymbols modifier2,
        Interval expectedInterval)
    {
        // Given
        var range = new NoteRange(
            new Note(quality1, octaveOrder1, modifier1),
            new Note(quality2, octaveOrder2, modifier2));

        // When
        var result = _provider!.GetIntervals(range);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedInterval);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.C, 1,
            NotationSymbols.None, new Interval(1, IntervalQuality.Perfect));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.C, 1,
            NotationSymbols.Sharp, new Interval(1, IntervalQuality.Augmented));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.Sharp, NoteQuality.C, 1,
            NotationSymbols.Sharp, new Interval(1, IntervalQuality.Perfect));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.Sharp, NoteQuality.D, 1,
            NotationSymbols.None, new Interval(2, IntervalQuality.Minor));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.Sharp, NoteQuality.D, 1,
            NotationSymbols.Sharp, new Interval(2, IntervalQuality.Major));
        yield return new TestCaseData(NoteQuality.D, 2, NotationSymbols.None, NoteQuality.E, 2,
            NotationSymbols.None, new Interval(2, IntervalQuality.Major));
        yield return new TestCaseData(NoteQuality.E, 1, NotationSymbols.None, NoteQuality.F, 1,
            NotationSymbols.None, new Interval(2, IntervalQuality.Minor));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.C, 2,
            NotationSymbols.None, new Interval(8, IntervalQuality.Perfect));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.D, 2,
            NotationSymbols.Flat, new Interval(9, IntervalQuality.Minor));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.D, 2,
            NotationSymbols.None, new Interval(9, IntervalQuality.Major));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.F, 1,
            NotationSymbols.None, new Interval(4, IntervalQuality.Perfect));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.F, 2,
            NotationSymbols.None, new Interval(11, IntervalQuality.Perfect));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.F, 2,
            NotationSymbols.Sharp, new Interval(11, IntervalQuality.Augmented));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.G, 1,
            NotationSymbols.None, new Interval(5, IntervalQuality.Perfect));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.G, 2,
            NotationSymbols.None, new Interval(12, IntervalQuality.Perfect));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.G, 2,
            NotationSymbols.Sharp, new Interval(12, IntervalQuality.Augmented));
        yield return new TestCaseData(NoteQuality.C, 1, NotationSymbols.None, NoteQuality.A, 2,
            NotationSymbols.None, new Interval(13, IntervalQuality.Major));
    }
}