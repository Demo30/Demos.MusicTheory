using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedFromNoteBySpanTest : TestBase
{
    private ChromaticNoteFullyQualifiedProviderFromNoteBySpan? _provider;
    
    [SetUp]
    public void SetUp()
    {
        Services.ServicesManager.ServicesProvider.RegisterService(() => new ChromaticNoteFullyQualifiedProviderFromChromaticIndex());
        _provider = new ChromaticNoteFullyQualifiedProviderFromNoteBySpan();
    }
    
    [TearDown]
    public void TearDown()
    {
        Services.ServicesManager.ResetServiceProvider();
    }
    
    [Theory]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 1, NotationSymbols.Flat)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 1, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 2,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 1, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 0, NotationSymbols.Flat)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 1,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 0,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 12,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 1, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 0,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 2,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 2,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.E, 0, NotationSymbols.Flat)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 2,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.F, 0, NotationSymbols.DoubleFlat)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 3,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.D, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 3,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.E, 0, NotationSymbols.Flat)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 5,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.F, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 6,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.F, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 6,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.G, 0, NotationSymbols.Flat)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 7,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.G, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 19,  OneDimensionDirection.RIGHT, ChromaticNoteQuality.G, 1, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.D, 1, NotationSymbols.Flat, 1, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 1, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, 1, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 1, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.D, 1, NotationSymbols.None, 2, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 1, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.D, 0, NotationSymbols.Flat, 1, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 1, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.None, 0, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 12, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 0, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.D, 0, NotationSymbols.Sharp, 2, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.E, 0, NotationSymbols.Flat, 2, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.F, 0, NotationSymbols.DoubleFlat, 2, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.Sharp)]
    [TestCase(ChromaticNoteQuality.D, 0, NotationSymbols.Sharp, 3, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.E, 0, NotationSymbols.Flat, 3, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.F, 0, NotationSymbols.None, 5, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.F, 0, NotationSymbols.Sharp, 6, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.G, 0, NotationSymbols.Flat, 6, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.G, 0, NotationSymbols.None, 7, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
    [TestCase(ChromaticNoteQuality.G, 1, NotationSymbols.None, 19, OneDimensionDirection.LEFT, ChromaticNoteQuality.C, 0, NotationSymbols.None)]
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
        var cluster = _provider!.GetEnharmonicNoteCluster(note, span, direction);

        // Then
        var expectedNote = new ChromaticNoteFullyQualified(expectedNoteQuality, expectedOrder, expectedModifier);
        cluster.Cluster.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }
}