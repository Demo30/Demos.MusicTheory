using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class NoteProviderFromNoteBySpanTest : TestBase
{
    private NoteProviderFromNoteBySpan? _provider;

    [SetUp]
    public void SetUp()
    {
        Services.ServicesManager.ServicesProvider.RegisterService(() =>
            new NoteProviderFromIndex());
        _provider = new NoteProviderFromNoteBySpan();
    }

    [TearDown]
    public void TearDown()
    {
        Services.ServicesManager.ResetServiceProvider();
    }

    [Theory]
    [TestCase(NoteQuality.C, 1, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQuality.D,
        1, NotationSymbols.Flat)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQuality.C,
        1, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.None, 2, OneDimensionalDirection.RIGHT, NoteQuality.D,
        1, NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQuality.D,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 1, OneDimensionalDirection.RIGHT, NoteQuality.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 0, OneDimensionalDirection.RIGHT, NoteQuality.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 12, OneDimensionalDirection.RIGHT, NoteQuality.C,
        1, NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 0, OneDimensionalDirection.RIGHT, NoteQuality.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.RIGHT, NoteQuality.D,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.RIGHT, NoteQuality.E,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.RIGHT, NoteQuality.F,
        0, NotationSymbols.DoubleFlat)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 3, OneDimensionalDirection.RIGHT, NoteQuality.D,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 3, OneDimensionalDirection.RIGHT, NoteQuality.E,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 5, OneDimensionalDirection.RIGHT, NoteQuality.F,
        0, NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 6, OneDimensionalDirection.RIGHT, NoteQuality.F,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 6, OneDimensionalDirection.RIGHT, NoteQuality.G,
        0, NotationSymbols.Flat)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 7, OneDimensionalDirection.RIGHT, NoteQuality.G,
        0, NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 19, OneDimensionalDirection.RIGHT, NoteQuality.G,
        1, NotationSymbols.None)]
    [TestCase(NoteQuality.D, 1, NotationSymbols.Flat, 1, OneDimensionalDirection.LEFT, NoteQuality.C, 1,
        NotationSymbols.None)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.Sharp, 1, OneDimensionalDirection.LEFT, NoteQuality.C,
        1, NotationSymbols.None)]
    [TestCase(NoteQuality.D, 1, NotationSymbols.None, 2, OneDimensionalDirection.LEFT, NoteQuality.C, 1,
        NotationSymbols.None)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.Flat, 1, OneDimensionalDirection.LEFT, NoteQuality.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 1, OneDimensionalDirection.LEFT, NoteQuality.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.None, 0, OneDimensionalDirection.LEFT, NoteQuality.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQuality.C, 1, NotationSymbols.None, 12, OneDimensionalDirection.LEFT, NoteQuality.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQuality.C, 0, NotationSymbols.Sharp, 0, OneDimensionalDirection.LEFT, NoteQuality.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.Sharp, 2, OneDimensionalDirection.LEFT, NoteQuality.C,
        0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.E, 0, NotationSymbols.Flat, 2, OneDimensionalDirection.LEFT, NoteQuality.C, 0,
        NotationSymbols.Sharp)]
    [TestCase(NoteQuality.F, 0, NotationSymbols.DoubleFlat, 2, OneDimensionalDirection.LEFT,
        NoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(NoteQuality.D, 0, NotationSymbols.Sharp, 3, OneDimensionalDirection.LEFT, NoteQuality.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQuality.E, 0, NotationSymbols.Flat, 3, OneDimensionalDirection.LEFT, NoteQuality.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQuality.F, 0, NotationSymbols.None, 5, OneDimensionalDirection.LEFT, NoteQuality.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQuality.F, 0, NotationSymbols.Sharp, 6, OneDimensionalDirection.LEFT, NoteQuality.C,
        0, NotationSymbols.None)]
    [TestCase(NoteQuality.G, 0, NotationSymbols.Flat, 6, OneDimensionalDirection.LEFT, NoteQuality.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQuality.G, 0, NotationSymbols.None, 7, OneDimensionalDirection.LEFT, NoteQuality.C, 0,
        NotationSymbols.None)]
    [TestCase(NoteQuality.G, 1, NotationSymbols.None, 19, OneDimensionalDirection.LEFT, NoteQuality.C,
        0, NotationSymbols.None)]
    public void ValidResults(
        NoteQuality noteQuality,
        int order,
        NotationSymbols modifier,
        int span,
        OneDimensionalDirection direction,
        NoteQuality expectedNoteQuality,
        int expectedOrder,
        NotationSymbols expectedModifier)
    {
        // Given
        Note note = new(noteQuality, order, modifier);

        // When
        var cluster = _provider!.GetEnharmonicNoteCluster(note, span, direction);

        // Then
        var expectedNote = new Note(expectedNoteQuality, expectedOrder, expectedModifier);
        cluster.Cluster.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }
}