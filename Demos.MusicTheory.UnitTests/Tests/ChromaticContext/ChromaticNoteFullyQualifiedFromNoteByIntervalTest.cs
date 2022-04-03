using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedFromNoteByIntervalTest : TestBase
{
    [Test]
    public void ValidResults()
    {
        // Given
        const ChromaticNoteQuality calledQuality = ChromaticNoteQuality.D;
        const int calledOrder = 5;
        const NotationSymbols calledModifier = NotationSymbols.None;
        var calledInterval = new ChromaticNoteIntervalFullyQualified(3, ChromaticNoteIntervalQuality.Augmented);
        const OneDimensionDirection calledDirection = OneDimensionDirection.RIGHT;

        var expectedResult = new ChromaticNoteEnharmonicCluster(new[]
        {
            new ChromaticNoteFullyQualified(ChromaticNoteQuality.C, 0, NotationSymbols.Sharp),
            new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 0, NotationSymbols.Flat)
        });
        
        ChromaticNoteFullyQualified note = new(calledQuality, calledOrder, calledModifier);
        var mockProviderFromNoteBySpan = new Mock<IChromaticNoteFullyQualifiedProviderFromNoteBySpan>();
        mockProviderFromNoteBySpan
            .Setup(p => p.GetEnharmonicNoteCluster(
                It.IsAny<ChromaticNoteFullyQualified>(), 
                It.IsAny<int>(),
                It.IsAny<OneDimensionDirection>()))
            .Returns(expectedResult);

        // When
        var provider = new ChromaticNoteFullyQualifiedProviderFromNoteByInterval(mockProviderFromNoteBySpan.Object);
        var cluster = provider.GetEnharmonicNoteCluster(note, calledInterval, calledDirection);

        // Then
        mockProviderFromNoteBySpan.Verify(c => c.GetEnharmonicNoteCluster(note, calledInterval.SemitoneCount, calledDirection), Times.Once);
        cluster.Should().BeSameAs(expectedResult);
    }
}