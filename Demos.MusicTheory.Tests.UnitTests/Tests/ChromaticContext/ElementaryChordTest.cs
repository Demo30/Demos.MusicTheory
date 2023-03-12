using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class ElementaryChordTest
{
    [Test]
    public void WhenNullChordStructureIsSupplied_CtorThrowsError()
    {
        // Given
        ChordStructure chordStructure = null;
        
        // When
        void Init() => new ElementaryChord(chordStructure);

        // Then
        Assert.Throws<ArgumentNullException>(Init);
    }
    
    [Test]
    [TestCaseSource(nameof(GetDataForTestInversions))]
    public void WhenValidChordIsConstructed_InversionsInProperOrderShouldBeReturned(ElementaryChord chord, IList<ChordStructure> expectedInversions)
    {
        // When
        var inversions = chord.GetChordStructureInversions().ToArray();
        
        // Then
        for (var i = 0; i < inversions.Length; i++)
        {
            expectedInversions[i].Should().BeEquivalentTo(inversions[i]);   
        }
    }

    private static IEnumerable<TestCaseData> GetDataForTestInversions()
    {
        yield return new TestCaseData(
            new ElementaryChord(new ChordStructure(new[] {new IntervalInternal(2, IntervalQualityInternal.Major)})),
            new[]
            {
                new ChordStructure(new[] { new IntervalInternal(2, IntervalQualityInternal.Major) })
            });

        yield return new TestCaseData(
            new ElementaryChord(new ChordStructure(new[] { new IntervalInternal(2, IntervalQualityInternal.Major), new IntervalInternal(3, IntervalQualityInternal.Minor) })),
            new[]
            {
                new ChordStructure(new[] { new IntervalInternal(2, IntervalQualityInternal.Major), new IntervalInternal(3, IntervalQualityInternal.Minor) }),
                new ChordStructure(new[] { new IntervalInternal(3, IntervalQualityInternal.Minor), new IntervalInternal(2, IntervalQualityInternal.Major) })
            });
        
        yield return new TestCaseData(
            new ElementaryChord(new ChordStructure(new[] { new IntervalInternal(2, IntervalQualityInternal.Major), new IntervalInternal(3, IntervalQualityInternal.Minor), new IntervalInternal(2, IntervalQualityInternal.Minor) })),
            new[]
            {
                new ChordStructure(new[] { new IntervalInternal(2, IntervalQualityInternal.Major), new IntervalInternal(3, IntervalQualityInternal.Minor), new IntervalInternal(2, IntervalQualityInternal.Minor) }),
                new ChordStructure(new[] { new IntervalInternal(2, IntervalQualityInternal.Minor), new IntervalInternal(2, IntervalQualityInternal.Major), new IntervalInternal(3, IntervalQualityInternal.Minor) }),
                new ChordStructure(new[] { new IntervalInternal(3, IntervalQualityInternal.Minor), new IntervalInternal(2, IntervalQualityInternal.Minor), new IntervalInternal(2, IntervalQualityInternal.Major) }),
            });
    }
    
}