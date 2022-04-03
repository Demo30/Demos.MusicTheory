using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteIntervalFullyQualifiedFromIntervalRangeTest : TestBase
{
    private ChromaticIntervalFullyQualifiedProviderFromRange? _provider;
    
    [SetUp]
    public void SetUp()
    {
        Services.ServicesManager.ServicesProvider.RegisterService(() => new ChromaticIntervalFullyQualifiedProviderFromChromaticIndexSpan());
        _provider = new ChromaticIntervalFullyQualifiedProviderFromRange();
    }
    
    [TearDown]
    public void TearDown()
    {
        Services.ServicesManager.ResetServiceProvider();
    }
    
    [Theory]
    [TestCaseSource(nameof(GetTestCases))]
    public void ValidResults(
        ChromaticNoteQuality quality1, int octaveOrder1, NotationSymbols modifier1, 
        ChromaticNoteQuality quality2, int octaveOrder2, NotationSymbols modifier2, 
        ChromaticNoteIntervalFullyQualified expectedInterval)
    {
        // Given
        var range = new ChromaticNoteFullyQualifiedRange(
            new ChromaticNoteFullyQualified(quality1, octaveOrder1, modifier1),
            new ChromaticNoteFullyQualified(quality2, octaveOrder2, modifier2));

        // When
        var result = _provider!.GetIntervals(range);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedInterval);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.C, 1, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(1, ChromaticNoteIntervalQuality.Perfect));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, new ChromaticNoteIntervalFullyQualified(1, ChromaticNoteIntervalQuality.Augmented));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, new ChromaticNoteIntervalFullyQualified(1, ChromaticNoteIntervalQuality.Perfect));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, ChromaticNoteQuality.D, 1, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.Sharp, ChromaticNoteQuality.D, 1, NotationSymbols.Sharp, new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Major));
        yield return new TestCaseData(ChromaticNoteQuality.D, 2, NotationSymbols.None, ChromaticNoteQuality.E, 2, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Major));
        yield return new TestCaseData(ChromaticNoteQuality.E, 1, NotationSymbols.None, ChromaticNoteQuality.F, 1, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(2, ChromaticNoteIntervalQuality.Minor));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.C, 2, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(8, ChromaticNoteIntervalQuality.Perfect));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.D, 2, NotationSymbols.Flat, new ChromaticNoteIntervalFullyQualified(9, ChromaticNoteIntervalQuality.Minor));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.D, 2, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(9, ChromaticNoteIntervalQuality.Major));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.F, 1, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(4, ChromaticNoteIntervalQuality.Perfect));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.F, 2, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(11, ChromaticNoteIntervalQuality.Perfect));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.F, 2, NotationSymbols.Sharp, new ChromaticNoteIntervalFullyQualified(11, ChromaticNoteIntervalQuality.Augmented));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.G, 1, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(5, ChromaticNoteIntervalQuality.Perfect));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.G, 2, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(12, ChromaticNoteIntervalQuality.Perfect));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.G, 2, NotationSymbols.Sharp, new ChromaticNoteIntervalFullyQualified(12, ChromaticNoteIntervalQuality.Augmented));
        yield return new TestCaseData(ChromaticNoteQuality.C, 1, NotationSymbols.None, ChromaticNoteQuality.A, 2, NotationSymbols.None, new ChromaticNoteIntervalFullyQualified(13, ChromaticNoteIntervalQuality.Major));
    }
}