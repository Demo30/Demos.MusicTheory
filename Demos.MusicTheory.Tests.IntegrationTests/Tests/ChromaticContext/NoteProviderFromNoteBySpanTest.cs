using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteProviderFromNoteBySpanTest : TestBase
{
    private NoteProviderFromNoteBySpan? _provider;

    [SetUp]
    public void SetUp()
    {
        RegisterService<NoteProviderFromIndex>();
        _provider = new NoteProviderFromNoteBySpan();
    }

    [Theory]
    [TestCase(NoteQualityInternal.C, 1, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQualityInternal.D,
        1, NotationSymbols.Flat)]
    [TestCase(NoteQualityInternal.C, 1, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQualityInternal.C,
        1, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.C, 1, NotationSymbols.None, 2, OneDimensionalDirection.RIGHT, NoteQualityInternal.D,
        1, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQualityInternal.D,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQualityInternal.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 0, OneDimensionalDirection.RIGHT, NoteQualityInternal.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 12, OneDimensionalDirection.RIGHT, NoteQualityInternal.C,
        1, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.Sharp, 0, OneDimensionalDirection.RIGHT, NoteQualityInternal.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.RIGHT, NoteQualityInternal.D,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.RIGHT, NoteQualityInternal.E,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.RIGHT, NoteQualityInternal.F,
        0, NotationSymbols.DoubleFlat)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 3, OneDimensionalDirection.RIGHT, NoteQualityInternal.D,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 3, OneDimensionalDirection.RIGHT, NoteQualityInternal.E,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 5, OneDimensionalDirection.RIGHT, NoteQualityInternal.F,
        0, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 6, OneDimensionalDirection.RIGHT, NoteQualityInternal.F,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 6, OneDimensionalDirection.RIGHT, NoteQualityInternal.G,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 7, OneDimensionalDirection.RIGHT, NoteQualityInternal.G,
        0, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 19, OneDimensionalDirection.RIGHT, NoteQualityInternal.G,
        1, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.D, 1, NotationSymbols.Flat, 1, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 1,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 1, NotationSymbols.Sharp, 1, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        1, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.D, 1, NotationSymbols.None, 2, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 1,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.D, 0, NotationSymbols.Flat, 1, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.Sharp, 1, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.None, 0, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 1, NotationSymbols.None, 12, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.C, 0, NotationSymbols.Sharp, 0, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.D, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.E, 0, NotationSymbols.Flat, 2, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 0,
        NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.F, 0, NotationSymbols.DoubleFlat, 2, OneDimensionalDirection.LEFT,
        NoteQualityInternal.C, 0, NotationSymbols.Sharp)]
    [TestCase(NoteQualityInternal.D, 0, NotationSymbols.Sharp, 3, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.E, 0, NotationSymbols.Flat, 3, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.F, 0, NotationSymbols.None, 5, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.F, 0, NotationSymbols.Sharp, 6, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQualityInternal.G, 0, NotationSymbols.Flat, 6, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.G, 0, NotationSymbols.None, 7, OneDimensionalDirection.LEFT, NoteQualityInternal.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQualityInternal.G, 1, NotationSymbols.None, 19, OneDimensionalDirection.LEFT, NoteQualityInternal.C,
        0, NotationSymbols.None)]
    public void ValidResults(
        NoteQualityInternal noteQualityInternal,
        int order,
        NotationSymbols modifier,
        int span,
        OneDimensionalDirection direction,
        NoteQualityInternal expectedNoteQualityInternal,
        int expectedOrder,
        NotationSymbols expectedModifier)
    {
        // Given
        NoteInternal noteInternal = new(noteQualityInternal, order, modifier);

        // When
        var cluster = _provider!.GetEnharmonics(noteInternal, span, direction);

        // Then
        var expectedNote = new NoteInternal(expectedNoteQualityInternal, expectedOrder, expectedModifier);
        cluster.Notes.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }
}