using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified.Providers;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.MidiAdapter;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.MidiAdapter;

[TestFixture]
internal class NoteFromMidiProviderTest : TestBase
{
    [SetUp]
    public void Setup()
    {
        RegisterService<NoteProviderFromIndex>();
    }
    
    public static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(21, new NoteInternal(NoteQualityInternal.A, 0, NotationSymbols.None));
        yield return new TestCaseData(22, new NoteInternal(NoteQualityInternal.A, 0, NotationSymbols.Sharp));
        yield return new TestCaseData(22, new NoteInternal(NoteQualityInternal.B, 0, NotationSymbols.Flat));
        yield return new TestCaseData(60, new NoteInternal(NoteQualityInternal.C, 4, NotationSymbols.None));
    }

    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void ShouldGetProperNoteFromMidiIndex(int midiIndex, NoteInternal expectedNote)
    {
        // Given
        var provider = new NoteFromMidiProvider();

        // When
        var enharmonics = provider.GetEnharmonicNotesFromMidiIndex(midiIndex);
        
        // Then
        enharmonics.Notes.Should().ContainEquivalentOf(expectedNote);
    }
}