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
public class ChromaticNoteFullyQualifiedFromNoteBySpanTest : TestBase
{
    [Test]
    [TestCase(OneDimensionDirection.RIGHT, 2)]
    [TestCase(OneDimensionDirection.LEFT, 2)]
    public void WhenFromNoteBySpanProviderIsUsed_FromChromaticIndexProviderShouldBeUsedWithValueOfSpannedChromaticIndex(OneDimensionDirection direction, int span)
    {
        // Given
        const ChromaticNoteQuality calledNoteQuality = ChromaticNoteQuality.C;
        const int calledOrder = 1;
        const NotationSymbols calledModifier = NotationSymbols.None;

        var expectedCluster = new ChromaticNoteFullyQualifiedEnharmonicCluster(new[]
        {
            new ChromaticNoteFullyQualified(ChromaticNoteQuality.D, 1, NotationSymbols.Sharp),
            new ChromaticNoteFullyQualified(ChromaticNoteQuality.E, 1, NotationSymbols.Flat)
        });
        
        var mockProviderByChromaticIndex = new Mock<IChromaticNoteFullyQualifiedProviderFromChromaticIndex>();
        mockProviderByChromaticIndex
            .Setup(p => p.GetEnharmonicNoteCluster(It.IsAny<int>()))
            .Returns(expectedCluster);
        
        var provider = new ChromaticNoteFullyQualifiedProviderFromNoteBySpan(mockProviderByChromaticIndex.Object);
        ChromaticNoteFullyQualified note = new(calledNoteQuality, calledOrder, calledModifier);

        var expectedChromaticIndexCallArgument =
            note.ChromaticContextIndex + (direction == OneDimensionDirection.RIGHT ? span : (-1 * span));
        
        // When
        var cluster = provider.GetEnharmonicNoteCluster(note, span, direction);

        // Then
        mockProviderByChromaticIndex.Verify(p => p.GetEnharmonicNoteCluster(expectedChromaticIndexCallArgument), Times.Once);
        cluster.Should().BeSameAs(expectedCluster);
    }
}