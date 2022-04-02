using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteIntervalFullyQualifiedFromIntervalRangeTest
{
    [Theory]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.C, 1, NotationSymbols.None, 0)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, 1)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, 0)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, ChromaticNoteQuality.D, 1, NotationSymbols.None, 1)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, ChromaticNoteQuality.D, 1, NotationSymbols.Sharp, 2)]
    [TestCase(ChromaticNoteQuality.D, 2, NotationSymbols.None, ChromaticNoteQuality.E, 2, NotationSymbols.None, 2)]
    [TestCase(ChromaticNoteQuality.E, 1, NotationSymbols.None, ChromaticNoteQuality.F, 1, NotationSymbols.None, 1)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.C, 2, NotationSymbols.None, 12)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.D, 2, NotationSymbols.Flat, 13)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.D, 2, NotationSymbols.None, 14)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.F, 2, NotationSymbols.None, 17)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.F, 2, NotationSymbols.Sharp, 18)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.G, 2, NotationSymbols.None, 19)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.G, 2, NotationSymbols.Sharp, 20)]
    [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.A, 2, NotationSymbols.None, 21)]
    public void ValidResults(
        ChromaticNoteQuality quality1, int octaveOrder1, NotationSymbols modifier1, 
        ChromaticNoteQuality quality2, int octaveOrder2, NotationSymbols modifier2, 
        int expectedChromaticIndexSpan)
    {
        // Given
        var range = new ChromaticNoteFullyQualifiedRange(
            new ChromaticNoteFullyQualified(quality1, octaveOrder1, modifier1),
            new ChromaticNoteFullyQualified(quality2, octaveOrder2, modifier2));
        ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan providerBySpan = new();
        ChromaticIntervalFullyQualifiedProviderFromRange provider = new(providerBySpan);

        // When
        var result = provider.GetIntervals(range);

        // Then
        var distinctChromaticIndexSpans = result.Cluster.Select(x => x.ChromaticIndexSpan).Distinct().ToList();

        distinctChromaticIndexSpans.Should().HaveCount(1);
        distinctChromaticIndexSpans.First().Should().Be(expectedChromaticIndexSpan);
    }
}