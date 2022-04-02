using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedFromNoteByIntervalTest
{
    private ChromaticNoteFullyQualifiedProviderFromNoteByInterval _provider;
    
    [SetUp]
    public void SetUp()
    {
        _provider = new ChromaticNoteFullyQualifiedProviderFromNoteByInterval();
    }
    
    [Theory]
    [TestCaseSource(nameof(GetTestCases))]
    public void ValidResults(
        ChromaticNoteQuality noteQuality, 
        int order, 
        NotationSymbols modifier, 
        ChromaticNoteIntervalFullyQualified interval, 
        OneDimensionDirection direction,
        ChromaticNoteQuality expectedNoteQuality,
        int expectedOrder,
        NotationSymbols expectedModifier)
    {
        // Given
        ChromaticNoteFullyQualified note = new(noteQuality, order, modifier);

        // When
        var cluster = _provider.GetEnharmonicNoteCluster(note, interval, direction);

        // Then
        var expectedNote = new ChromaticNoteFullyQualified(expectedNoteQuality, expectedOrder, expectedModifier);
        cluster.Cluster.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        // TODO: add more test cases
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(1, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.RIGHT, ChromaticNoteQuality.C, 1, NotationSymbols.None);
    }
}