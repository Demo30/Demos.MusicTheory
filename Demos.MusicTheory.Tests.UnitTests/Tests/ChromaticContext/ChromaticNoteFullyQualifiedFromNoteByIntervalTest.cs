using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedFromNoteByIntervalTest : TestBase
{
    [Test]
    [TestCase(OneDimensionDirection.RIGHT)]
    [TestCase(OneDimensionDirection.LEFT)]
    public void ValidResults(OneDimensionDirection direction)
    {
        // Given
        const ChromaticNoteQuality calledQuality = ChromaticNoteQuality.D;
        const int calledOrder = 5;
        const NotationSymbols calledModifier = NotationSymbols.None;
        var calledInterval = new ChromaticNoteIntervalFullyQualified(3, ChromaticNoteIntervalQuality.Augmented);

        var expectedResult = new ChromaticNoteEnharmonicCluster(new[]
        {
            new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp),
            new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 0, NotationSymbols.Flat)
        });
        
        ChromaticNoteFullyQualified note = new(calledQuality, calledOrder, calledModifier);
        var mockProviderFromNoteBySpan = new Mock<IChromaticNoteFullyQualifiedProviderFromChromaticIndex>();
        mockProviderFromNoteBySpan
            .Setup(p => p.GetEnharmonicNoteCluster(It.IsAny<int>()))
            .Returns(expectedResult);
        
        var expectedChromaticIndexArgument = note.ChromaticContextIndex + (direction == OneDimensionDirection.RIGHT ? calledInterval.SemitoneCount : -calledInterval.SemitoneCount);

        // When
        var provider = new ChromaticNoteFullyQualifiedProviderFromNoteByInterval(mockProviderFromNoteBySpan.Object);
        var cluster = provider.GetEnharmonicNoteCluster(note, calledInterval, direction);

        // Then
        mockProviderFromNoteBySpan.Verify(c => c.GetEnharmonicNoteCluster(expectedChromaticIndexArgument), Times.Once);
        cluster.Should().BeSameAs(expectedResult);
    }
}