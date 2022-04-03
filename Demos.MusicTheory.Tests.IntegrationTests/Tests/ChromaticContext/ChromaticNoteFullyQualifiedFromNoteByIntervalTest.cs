using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedFromNoteByIntervalTest : TestBase
{
    private ChromaticNoteFullyQualifiedProviderFromNoteByInterval? _provider;

    [SetUp]
    public void SetUp()
    {
        Services.ServicesManager.ServicesProvider.RegisterService(() => new ChromaticNoteFullyQualifiedProviderFromChromaticIndex());
        _provider = new ChromaticNoteFullyQualifiedProviderFromNoteByInterval();
    }
    
    [TearDown]
    public void TearDown()
    {
        Services.ServicesManager.ResetServiceProvider();
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
        var cluster = _provider!.GetEnharmonicNoteCluster(note, interval, direction);

        // Then
        var expectedNote = new ChromaticNoteFullyQualified(expectedNoteQuality, expectedOrder, expectedModifier);
        cluster.Cluster.Should().ContainSingle(n => n.IsEqualByContent(expectedNote));
        cluster.ChromaticContextIndex.Should().Be(expectedNote.ChromaticContextIndex);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 1, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.D, 1, NotationSymbols.Flat
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 1, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.C, 1, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 1, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Major), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.D, 1, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.D, 0, NotationSymbols.Flat
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.C, 0, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(1, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.C, 0, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(8, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.C, 1, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.Sharp,
            new ChromaticNoteIntervalFullyQualified(1, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.C, 0, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.Sharp, 
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Major), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.D, 0, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.Sharp,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Major), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.E, 0, NotationSymbols.Flat
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.Sharp,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Major), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.F, 0, NotationSymbols.DoubleFlat
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(3, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.D, 0, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Augmented), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.D, 0, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData(
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(3, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.E, 0, NotationSymbols.Flat
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(4, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.F, 0, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None, 
            new ChromaticNoteIntervalFullyQualified(4, ChromaticNoteIntervalQuality.Augmented), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.F, 0, NotationSymbols.Sharp
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(4, ChromaticNoteIntervalQuality.Augmented), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.G, 0, NotationSymbols.Flat
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(5, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.G, 0, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(12, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.RIGHT,
            ChromaticNoteQuality.G, 1, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.D, 1, NotationSymbols.Flat,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.LEFT,
            ChromaticNoteQuality.C, 1, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 1, NotationSymbols.Sharp,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.LEFT,
            ChromaticNoteQuality.C, 1, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.D, 1, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Major), OneDimensionDirection.LEFT,
            ChromaticNoteQuality.C, 1, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.D, 0, NotationSymbols.Flat,
            new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor), OneDimensionDirection.LEFT,
            ChromaticNoteQuality.C, 0, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.G, 0, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(5, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.LEFT,
            ChromaticNoteQuality.C, 0, NotationSymbols.None
        );
        
        yield return new TestCaseData
        (
            ChromaticNoteQuality.C, 1, NotationSymbols.None,
            new ChromaticNoteIntervalFullyQualified(8, ChromaticNoteIntervalQuality.Perfect), OneDimensionDirection.LEFT,
            ChromaticNoteQuality.C, 0, NotationSymbols.None
        );
    }
}