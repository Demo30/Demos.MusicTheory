using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
internal class NoteProviderFromNoteBySpanTest : TestBase
{
    [Test]
    [TestCase(OneDimensionalDirection.RIGHT, 2)]
    [TestCase(OneDimensionalDirection.LEFT, 2)]
    public void WhenFromNoteBySpanProviderIsUsed_FromChromaticIndexProviderShouldBeUsedWithValueOfSpannedChromaticIndex(
        OneDimensionalDirection direction, int span)
    {
        // Given
        const NoteQualityInternal calledNoteQuality = NoteQualityInternal.C;
        const int calledOrder = 1;
        const NotationSymbols calledModifier = NotationSymbols.None;

        var expectedCluster = new NoteEnharmonicsInternal(new[]
        {
            new NoteInternal(NoteQualityInternal.D, 1, NotationSymbols.Sharp),
            new NoteInternal(NoteQualityInternal.E, 1, NotationSymbols.Flat)
        });

        var mockProviderByChromaticIndex = new Mock<INoteProviderFromIndex>();
        mockProviderByChromaticIndex
            .Setup(p => p.GetEnharmonics(It.IsAny<int>()))
            .Returns(expectedCluster);

        var provider = new NoteProviderFromNoteBySpan(mockProviderByChromaticIndex.Object);
        NoteInternal noteInternal = new(calledNoteQuality, calledOrder, calledModifier);

        var expectedChromaticIndexCallArgument =
            noteInternal.ChromaticContextIndex + (direction == OneDimensionalDirection.RIGHT ? span : -1 * span);

        // When
        var cluster = provider.GetEnharmonics(noteInternal, span, direction);

        // Then
        mockProviderByChromaticIndex.Verify(p => p.GetEnharmonics(expectedChromaticIndexCallArgument),
            Times.Once);
        cluster.Should().BeSameAs(expectedCluster);
    }
}