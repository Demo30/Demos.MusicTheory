using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteIntervalFullyQualifiedFromChromaticSpanCreationTest
{
    [Theory]
    [TestCase(0, 1, ChromaticNoteIntervalQuality.Perfect)]
    [TestCase(1, 1, ChromaticNoteIntervalQuality.Augmented)]
    [TestCase(1, 2, ChromaticNoteIntervalQuality.Minor)]
    [TestCase(2, 2, ChromaticNoteIntervalQuality.Major)]
    [TestCase(2, 3, ChromaticNoteIntervalQuality.Diminished)]
    [TestCase(3, 2, ChromaticNoteIntervalQuality.Augmented)]
    [TestCase(3, 3, ChromaticNoteIntervalQuality.Minor)]
    [TestCase(4, 3, ChromaticNoteIntervalQuality.Major)]
    [TestCase(4, 4, ChromaticNoteIntervalQuality.Diminished)]
    [TestCase(5, 4, ChromaticNoteIntervalQuality.Perfect)]
    [TestCase(5, 3, ChromaticNoteIntervalQuality.Augmented)]
    [TestCase(6, 4, ChromaticNoteIntervalQuality.Augmented)]
    [TestCase(6, 5, ChromaticNoteIntervalQuality.Diminished)]
    [TestCase(7, 5, ChromaticNoteIntervalQuality.Perfect)]
    [TestCase(7, 6, ChromaticNoteIntervalQuality.Diminished)]
    [TestCase(8, 5, ChromaticNoteIntervalQuality.Augmented)]
    [TestCase(8, 6, ChromaticNoteIntervalQuality.Minor)]
    [TestCase(9, 6, ChromaticNoteIntervalQuality.Major)]
    [TestCase(9, 6, ChromaticNoteIntervalQuality.Major)]
    [TestCase(9, 7, ChromaticNoteIntervalQuality.Diminished)]
    [TestCase(10, 6, ChromaticNoteIntervalQuality.Augmented)]
    [TestCase(10, 7, ChromaticNoteIntervalQuality.Minor)]
    [TestCase(11, 7, ChromaticNoteIntervalQuality.Major)]
    [TestCase(11, 8, ChromaticNoteIntervalQuality.Diminished)]
    [TestCase(12, 8, ChromaticNoteIntervalQuality.Perfect)]
    [TestCase(12, 9, ChromaticNoteIntervalQuality.Diminished)]
    [TestCase(13, 8, ChromaticNoteIntervalQuality.Augmented)]
    [TestCase(13, 9, ChromaticNoteIntervalQuality.Minor)]
    [TestCase(19, 12, ChromaticNoteIntervalQuality.Perfect)]
    public void ValidResults(int chromaticIndexSpan, int expectedDiatonicScaleDegree, ChromaticNoteIntervalQuality expectedQuality)
    {
        // Given
        ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan provider = new();

        // When
        var result = provider.GetIntervals(chromaticIndexSpan);

        // Then
        result.Cluster.Should().Contain(x => 
            x.DiatonicScaleDegree.Equals(expectedDiatonicScaleDegree) &&
            x.Quality.Equals(expectedQuality));
    }
}