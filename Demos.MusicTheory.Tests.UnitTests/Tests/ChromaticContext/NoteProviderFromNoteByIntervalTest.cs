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
internal class NoteProviderFromNoteByIntervalTest : TestBase
{
    [Test]
    [TestCase(OneDimensionalDirection.RIGHT)]
    [TestCase(OneDimensionalDirection.LEFT)]
    public void ValidResults(OneDimensionalDirection direction)
    {
        // Given
        const NoteQualityInternal calledQuality = NoteQualityInternal.D;
        const int calledOrder = 5;
        const NotationSymbols calledModifier = NotationSymbols.None;
        var calledInterval = new IntervalInternal(3, IntervalQualityInternal.Augmented);

        var expectedResult = new NoteEnharmonicsInternal(new[]
        {
            new NoteInternal(NoteQualityInternal.C, 0, NotationSymbols.Sharp),
            new NoteInternal(NoteQualityInternal.D, 0, NotationSymbols.Flat)
        });

        NoteInternal noteInternal = new(calledQuality, calledOrder, calledModifier);
        var mockProviderFromNoteBySpan = new Mock<INoteProviderFromIndex>();
        mockProviderFromNoteBySpan
            .Setup(p => p.GetEnharmonics(It.IsAny<int>()))
            .Returns(expectedResult);

        var expectedChromaticIndexArgument = noteInternal.ChromaticContextIndex + (direction == OneDimensionalDirection.RIGHT
            ? calledInterval.SemitoneCount
            : -calledInterval.SemitoneCount);

        // When
        var provider = new NoteProviderFromNoteByInterval(mockProviderFromNoteBySpan.Object);
        var cluster = provider.GetEnharmonics(noteInternal, calledInterval, direction);

        // Then
        mockProviderFromNoteBySpan.Verify(c => c.GetEnharmonics(expectedChromaticIndexArgument), Times.Once);
        cluster.Should().BeSameAs(expectedResult);
    }
}