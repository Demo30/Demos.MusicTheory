using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class NoteProviderFromNoteBySpanTest : TestBase
{
    [Test]
    [TestCase(OneDimensionalDirection.RIGHT, 2)]
    [TestCase(OneDimensionalDirection.LEFT, 2)]
    public void WhenFromNoteBySpanProviderIsUsed_FromChromaticIndexProviderShouldBeUsedWithValueOfSpannedChromaticIndex(
        OneDimensionalDirection direction, int span)
    {
        // Given
        const NoteQuality calledNoteQuality = NoteQuality.C;
        const int calledOrder = 1;
        const NotationSymbols calledModifier = NotationSymbols.None;

        var expectedCluster = new NoteEnharmonics(new[]
        {
            new Note(NoteQuality.D, 1, NotationSymbols.Sharp),
            new Note(NoteQuality.E, 1, NotationSymbols.Flat)
        });

        var mockProviderByChromaticIndex = new Mock<INoteProviderFromIndex>();
        mockProviderByChromaticIndex
            .Setup(p => p.GetEnharmonics(It.IsAny<int>()))
            .Returns(expectedCluster);

        var provider = new NoteProviderFromNoteBySpan(mockProviderByChromaticIndex.Object);
        Note note = new(calledNoteQuality, calledOrder, calledModifier);

        var expectedChromaticIndexCallArgument =
            note.ChromaticContextIndex + (direction == OneDimensionalDirection.RIGHT ? span : -1 * span);

        // When
        var cluster = provider.GetEnharmonics(note, span, direction);

        // Then
        mockProviderByChromaticIndex.Verify(p => p.GetEnharmonics(expectedChromaticIndexCallArgument),
            Times.Once);
        cluster.Should().BeSameAs(expectedCluster);
    }
}