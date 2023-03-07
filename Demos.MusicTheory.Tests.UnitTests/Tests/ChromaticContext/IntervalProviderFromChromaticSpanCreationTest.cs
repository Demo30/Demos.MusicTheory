using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class IntervalProviderFromChromaticSpanCreationTest
{
    [Theory]
    [TestCase(0, 1, IntervalQuality.Perfect)]
    [TestCase(1, 1, IntervalQuality.Augmented)]
    [TestCase(1, 2, IntervalQuality.Minor)]
    [TestCase(2, 2, IntervalQuality.Major)]
    [TestCase(2, 3, IntervalQuality.Diminished)]
    [TestCase(3, 2, IntervalQuality.Augmented)]
    [TestCase(3, 3, IntervalQuality.Minor)]
    [TestCase(4, 3, IntervalQuality.Major)]
    [TestCase(4, 4, IntervalQuality.Diminished)]
    [TestCase(5, 4, IntervalQuality.Perfect)]
    [TestCase(5, 3, IntervalQuality.Augmented)]
    [TestCase(6, 4, IntervalQuality.Augmented)]
    [TestCase(6, 5, IntervalQuality.Diminished)]
    [TestCase(7, 5, IntervalQuality.Perfect)]
    [TestCase(7, 6, IntervalQuality.Diminished)]
    [TestCase(8, 5, IntervalQuality.Augmented)]
    [TestCase(8, 6, IntervalQuality.Minor)]
    [TestCase(9, 6, IntervalQuality.Major)]
    [TestCase(9, 6, IntervalQuality.Major)]
    [TestCase(9, 7, IntervalQuality.Diminished)]
    [TestCase(10, 6, IntervalQuality.Augmented)]
    [TestCase(10, 7, IntervalQuality.Minor)]
    [TestCase(11, 7, IntervalQuality.Major)]
    [TestCase(11, 8, IntervalQuality.Diminished)]
    [TestCase(12, 8, IntervalQuality.Perfect)]
    [TestCase(12, 9, IntervalQuality.Diminished)]
    [TestCase(13, 8, IntervalQuality.Augmented)]
    [TestCase(13, 9, IntervalQuality.Minor)]
    [TestCase(19, 12, IntervalQuality.Perfect)]
    public void ValidResults(int chromaticIndexSpan, int expectedDiatonicScaleDegree, IntervalQuality expectedQuality)
    {
        // Given
        var provider = new IntervalProviderFromIndexSpan();

        // When
        var result = provider.GetIntervals(chromaticIndexSpan);

        // Then
        result.Intervals.Should().Contain(x =>
            x.DiatonicScaleDegree.Equals(expectedDiatonicScaleDegree) &&
            x.Quality.Equals(expectedQuality));
    }
}