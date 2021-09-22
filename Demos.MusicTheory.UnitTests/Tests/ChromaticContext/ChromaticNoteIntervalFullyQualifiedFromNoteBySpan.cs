using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext
{
    [TestFixture]
    public class ChromaticNoteIntervalFullyQualifiedFromNoteBySpan
    {
        [Theory]
        [TestCase(ChromaticNoteQuality.C, 1, NotationSymbols.None, 2, ChromaticNoteQuality.D, 1, NotationSymbols.None)]
        public void ValidResults(
            ChromaticNoteQuality noteQuality, 
            int order, 
            NotationSymbols modifier, 
            int span, 
            OneDimensionDirection direction,
            ChromaticNoteQuality expectedNoteQuality,
            int expectedOrder,
            NotationSymbols expectedModifier)
        {
            // Given
            ChromaticNoteFullyQualifiedProviderFromNoteBySpan provider = new();
            ChromaticNoteFullyQualified note = new(noteQuality, order, modifier);

            // When
            var cluster = provider.GetEnharmonicNoteCluster(note, span, direction);

            // Then
            var referentialExpectedNote = new ChromaticNoteFullyQualified(expectedNoteQuality, expectedOrder, expectedModifier);
            cluster.ChromaticContextIndex.Should().Be(referentialExpectedNote.ChromaticContextIndex);
        }
    }
}
