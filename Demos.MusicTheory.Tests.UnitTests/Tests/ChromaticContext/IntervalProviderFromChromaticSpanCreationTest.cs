using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class IntervalProviderFromChromaticSpanCreationTest
{
    [Theory]
    [TestCase(0, 1, IntervalQualityInternal.Perfect)]
    [TestCase(1, 1, IntervalQualityInternal.Augmented)]
    [TestCase(1, 2, IntervalQualityInternal.Minor)]
    [TestCase(2, 2, IntervalQualityInternal.Major)]
    [TestCase(2, 3, IntervalQualityInternal.Diminished)]
    [TestCase(3, 2, IntervalQualityInternal.Augmented)]
    [TestCase(3, 3, IntervalQualityInternal.Minor)]
    [TestCase(4, 3, IntervalQualityInternal.Major)]
    [TestCase(4, 4, IntervalQualityInternal.Diminished)]
    [TestCase(5, 4, IntervalQualityInternal.Perfect)]
    [TestCase(5, 3, IntervalQualityInternal.Augmented)]
    [TestCase(6, 4, IntervalQualityInternal.Augmented)]
    [TestCase(6, 5, IntervalQualityInternal.Diminished)]
    [TestCase(7, 5, IntervalQualityInternal.Perfect)]
    [TestCase(7, 6, IntervalQualityInternal.Diminished)]
    [TestCase(8, 5, IntervalQualityInternal.Augmented)]
    [TestCase(8, 6, IntervalQualityInternal.Minor)]
    [TestCase(9, 6, IntervalQualityInternal.Major)]
    [TestCase(9, 6, IntervalQualityInternal.Major)]
    [TestCase(9, 7, IntervalQualityInternal.Diminished)]
    [TestCase(10, 6, IntervalQualityInternal.Augmented)]
    [TestCase(10, 7, IntervalQualityInternal.Minor)]
    [TestCase(11, 7, IntervalQualityInternal.Major)]
    [TestCase(11, 8, IntervalQualityInternal.Diminished)]
    [TestCase(12, 8, IntervalQualityInternal.Perfect)]
    [TestCase(12, 9, IntervalQualityInternal.Diminished)]
    [TestCase(13, 8, IntervalQualityInternal.Augmented)]
    [TestCase(13, 9, IntervalQualityInternal.Minor)]
    [TestCase(19, 12, IntervalQualityInternal.Perfect)]
    public void ValidResults(int chromaticIndexSpan, int expectedDiatonicScaleDegree, IntervalQualityInternal expectedQualityInternal)
    {
        // Given
        var provider = new IntervalProviderFromIndexSpan();

        // When
        var result = provider.GetIntervals(chromaticIndexSpan);

        // Then
        result.Intervals.Should().Contain(x =>
            x.DiatonicScaleDegree.Equals(expectedDiatonicScaleDegree) &&
            x.QualityInternal.Equals(expectedQualityInternal));
    }
}