using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteProviderFromNoteByIntervalTest : TestBase
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
        NoteQualityInternal noteQualityInternal,
        int order,
        NotationSymbols modifier,
        IntervalInternal intervalInternal,
        OneDimensionalDirection direction,
        NoteQualityInternal expectedNoteQualityInternal,
        int expectedOrder,
        NotationSymbols expectedModifier)
    {
        // Given
        NoteInternal noteInternal = new(noteQualityInternal, order, modifier);

        // When
        var cluster = _provider!.GetEnharmonics(noteInternal, intervalInternal, direction);

        // Then
        var expectedNote = new NoteInternal(expectedNoteQualityInternal, expectedOrder, expectedModifier);
        cluster.Notes.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData
        (
            NoteQualityInternal.C, 1, NotationSymbols.None,
            new IntervalInternal(2, IntervalQualityInternal.Minor), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.D, 1, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 1, NotationSymbols.None,
            new IntervalInternal(2, IntervalQualityInternal.Minor), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.C, 1, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 1, NotationSymbols.None,
            new IntervalInternal(2, IntervalQualityInternal.Major), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.D, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(2, IntervalQualityInternal.Minor), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.D, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(2, IntervalQualityInternal.Minor), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.C, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(1, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.C, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(8, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.Sharp,
            new IntervalInternal(1, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.C, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.Sharp,
            new IntervalInternal(2, IntervalQualityInternal.Major), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.D, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.Sharp,
            new IntervalInternal(2, IntervalQualityInternal.Major), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.E, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.Sharp,
            new IntervalInternal(2, IntervalQualityInternal.Major), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.F, 0, NotationSymbols.DoubleFlat
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(3, IntervalQualityInternal.Minor), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.D, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(2, IntervalQualityInternal.Augmented),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.D, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData(
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(3, IntervalQualityInternal.Minor), OneDimensionalDirection.RIGHT,
            NoteQualityInternal.E, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(4, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.F, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(4, IntervalQualityInternal.Augmented),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.F, 0, NotationSymbols.Sharp
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(4, IntervalQualityInternal.Augmented),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.G, 0, NotationSymbols.Flat
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(5, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.G, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 0, NotationSymbols.None,
            new IntervalInternal(12, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.G, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.D, 1, NotationSymbols.Flat,
            new IntervalInternal(2, IntervalQualityInternal.Minor), OneDimensionalDirection.LEFT,
            NoteQualityInternal.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 1, NotationSymbols.Sharp,
            new IntervalInternal(2, IntervalQualityInternal.Minor), OneDimensionalDirection.LEFT,
            NoteQualityInternal.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.D, 1, NotationSymbols.None,
            new IntervalInternal(2, IntervalQualityInternal.Major), OneDimensionalDirection.LEFT,
            NoteQualityInternal.C, 1, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.D, 0, NotationSymbols.Flat,
            new IntervalInternal(2, IntervalQualityInternal.Minor), OneDimensionalDirection.LEFT,
            NoteQualityInternal.C, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.G, 0, NotationSymbols.None,
            new IntervalInternal(5, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.LEFT,
            NoteQualityInternal.C, 0, NotationSymbols.None
        );

        yield return new TestCaseData
        (
            NoteQualityInternal.C, 1, NotationSymbols.None,
            new IntervalInternal(8, IntervalQualityInternal.Perfect),
            OneDimensionalDirection.LEFT,
            NoteQualityInternal.C, 0, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            NoteQualityInternal.G, 1, NotationSymbols.None,
            new IntervalInternal(3, IntervalQualityInternal.Minor),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.C, 2, NotationSymbols.DoubleFlat
        );
        
        yield return new TestCaseData
        (
            NoteQualityInternal.G, 1, NotationSymbols.None,
            new IntervalInternal(3, IntervalQualityInternal.Minor),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.B, 1, NotationSymbols.Flat
        );
        
        yield return new TestCaseData
        (
            NoteQualityInternal.G, 1, NotationSymbols.None,
            new IntervalInternal(3, IntervalQualityInternal.Minor),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.A, 1, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData
        (
            NoteQualityInternal.F, 1, NotationSymbols.DoubleSharp,
            new IntervalInternal(3, IntervalQualityInternal.Minor),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.C, 2, NotationSymbols.DoubleFlat
        );
        
        yield return new TestCaseData
        (
            NoteQualityInternal.F, 1, NotationSymbols.DoubleSharp,
            new IntervalInternal(3, IntervalQualityInternal.Minor),
            OneDimensionalDirection.RIGHT,
            NoteQualityInternal.A, 1, NotationSymbols.Sharp
        );
    }
}