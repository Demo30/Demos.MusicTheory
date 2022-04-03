using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedFromNoteBySpanTest : TestBase
{
    private ChromaticNoteFullyQualifiedProviderFromNoteBySpan _provider;
    
    [SetUp]
    public void SetUp()
    {
        _provider = new ChromaticNoteFullyQualifiedProviderFromNoteBySpan();
    }
    
    // TODO: add more test, try edge-cases
    [Theory]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 1, NotationSymbols.Flat)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 1, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 2,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 1, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 0, NotationSymbols.Flat)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 0,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 12,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 1, NotationSymbols.None)]
    public void ValidResults(
        ChromaticNoteQuality noteQuality, 
        int order, 
        NotationSymbols modifier, 
        int span, 
        OneDimensionDirection direction,
        ChromaticNoteQuality expectedNoteQuality,
        int expectedOrder,
        NotationSymbols expectedModifier)
    {
        // Given
        ChromaticNoteFullyQualified note = new(noteQuality, order, modifier);

        // When
        var cluster = _provider.GetEnharmonicNoteCluster(note, span, direction);

        // Then
        var expectedNote = new ChromaticNoteFullyQualified(expectedNoteQuality, expectedOrder, expectedModifier);
        cluster.Cluster.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }
}