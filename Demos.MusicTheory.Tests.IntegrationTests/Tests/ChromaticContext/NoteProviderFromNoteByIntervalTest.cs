using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class NoteProviderFromNoteByIntervalTest : TestBase
{
    private NoteProviderFromNoteByInterval? _provider;

    [SetUp]
    public void SetUp()
    {
        RegisterService<NoteProviderFromIndex>();
        _provider = new NoteProviderFromNoteByInterval();
    }

    [Theory]
    [TestCaseSource(nameof(GetTestCases))]
    public void ValidResults(
        NoteQuality noteQuality,
        int order,
        NotationSymbols modifier,
        Interval interval,
        OneDimensionalDirection direction,
        NoteQuality expectedNoteQuality,
        int expectedOrder,
        NotationSymbols expectedModifier)
    {
        // Given
        Note note = new(noteQuality, order, modifier);

        // When
        var cluster = _provider!.GetEnharmonics(note, interval, direction);

        // Then
        var expectedNote = new Note(expectedNoteQuality, expectedOrder, expectedModifier);
        cluster.Notes.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData
        (
            NoteQuality.C, 1, NotationSymbols.None,
            new Interval(2, IntervalQuality.Minor), OneDimensionalDirection.RIGHT,
            NoteQuality.D, 1, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 1, NotationSymbols.None,
            new Interval(2, IntervalQuality.Minor), OneDimensionalDirection.RIGHT,
            NoteQuality.C, 1, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 1, NotationSymbols.None,
            new Interval(2, IntervalQuality.Major), OneDimensionalDirection.RIGHT,
            NoteQuality.D, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(2, IntervalQuality.Minor), OneDimensionalDirection.RIGHT,
            NoteQuality.D, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(2, IntervalQuality.Minor), OneDimensionalDirection.RIGHT,
            NoteQuality.C, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(1, IntervalQuality.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQuality.C, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(8, IntervalQuality.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQuality.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.Sharp,
            new Interval(1, IntervalQuality.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQuality.C, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.Sharp,
            new Interval(2, IntervalQuality.Major), OneDimensionalDirection.RIGHT,
            NoteQuality.D, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.Sharp,
            new Interval(2, IntervalQuality.Major), OneDimensionalDirection.RIGHT,
            NoteQuality.E, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.Sharp,
            new Interval(2, IntervalQuality.Major), OneDimensionalDirection.RIGHT,
            NoteQuality.F, 0, NotationSymbols.DoubleFlat
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(3, IntervalQuality.Minor), OneDimensionalDirection.RIGHT,
            NoteQuality.D, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(2, IntervalQuality.Augmented),
            OneDimensionalDirection.RIGHT,
            NoteQuality.D, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData(
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(3, IntervalQuality.Minor), OneDimensionalDirection.RIGHT,
            NoteQuality.E, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(4, IntervalQuality.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQuality.F, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(4, IntervalQuality.Augmented),
            OneDimensionalDirection.RIGHT,
            NoteQuality.F, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(4, IntervalQuality.Augmented),
            OneDimensionalDirection.RIGHT,
            NoteQuality.G, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(5, IntervalQuality.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQuality.G, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 0, NotationSymbols.None,
            new Interval(12, IntervalQuality.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQuality.G, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.D, 1, NotationSymbols.Flat,
            new Interval(2, IntervalQuality.Minor), OneDimensionalDirection.LEFT,
            NoteQuality.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 1, NotationSymbols.Sharp,
            new Interval(2, IntervalQuality.Minor), OneDimensionalDirection.LEFT,
            NoteQuality.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.D, 1, NotationSymbols.None,
            new Interval(2, IntervalQuality.Major), OneDimensionalDirection.LEFT,
            NoteQuality.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.D, 0, NotationSymbols.Flat,
            new Interval(2, IntervalQuality.Minor), OneDimensionalDirection.LEFT,
            NoteQuality.C, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.G, 0, NotationSymbols.None,
            new Interval(5, IntervalQuality.Perfect),
            OneDimensionalDirection.LEFT,
            NoteQuality.C, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQuality.C, 1, NotationSymbols.None,
            new Interval(8, IntervalQuality.Perfect),
            OneDimensionalDirection.LEFT,
            NoteQuality.C, 0, NotationSymbols.None
        );
    }
}