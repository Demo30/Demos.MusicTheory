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
public class NoteProviderFromNoteByIntervalTest : TestBase
{
    [Test]
    [TestCase(OneDimensionalDirection.RIGHT)]
    [TestCase(OneDimensionalDirection.LEFT)]
    public void ValidResults(OneDimensionalDirection direction)
    {
        // Given
        const NoteQuality calledQuality = NoteQuality.D;
        const int calledOrder = 5;
        const NotationSymbols calledModifier = NotationSymbols.None;
        var calledInterval = new Interval(3, IntervalQuality.Augmented);

        var expectedResult = new NoteEnharmonics(new[]
        {
            new Note(NoteQuality.C, 0, NotationSymbols.Sharp),
            new Note(NoteQuality.D, 0, NotationSymbols.Flat)
        });

        Note note = new(calledQuality, calledOrder, calledModifier);
        var mockProviderFromNoteBySpan = new Mock<INoteProviderFromIndex>();
        mockProviderFromNoteBySpan
            .Setup(p => p.GetEnharmonics(It.IsAny<int>()))
            .Returns(expectedResult);

        var expectedChromaticIndexArgument = note.ChromaticContextIndex + (direction == OneDimensionalDirection.RIGHT
            ? calledInterval.SemitoneCount
            : -calledInterval.SemitoneCount);

        // When
        var provider = new NoteProviderFromNoteByInterval(mockProviderFromNoteBySpan.Object);
        var cluster = provider.GetEnharmonicNoteCluster(note, calledInterval, direction);

        // Then
        mockProviderFromNoteBySpan.Verify(c => c.GetEnharmonics(expectedChromaticIndexArgument), Times.Once);
        cluster.Should().BeSameAs(expectedResult);
    }
}