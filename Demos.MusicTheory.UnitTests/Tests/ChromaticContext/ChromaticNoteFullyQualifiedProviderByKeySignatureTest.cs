using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext;

[TestFixture]
public class ChromaticNoteFullyQualifiedProviderByKeySignatureTest
{
    [Theory]
    [TestCase(KeySignatures.Simple, 14, new[] { "C0", "D0", "E0", "F0", "G0", "A0", "B0", "C1", "D1", "E1", "F1", "G1", "A1", "B1" })]
    [TestCase(KeySignatures.Sharps_1, 7, new[] { "G0", "A0", "B0", "C0", "D0", "E0", "F0Sharp" })]
    public void GetNotes(KeySignatures key, int count, string[] outputs)
    {
        // Given
        ChromaticNoteDiatonicScaleKeySignatureProvider provider = new();

        // When
        var notes = provider.GetNotes(key, count);

        // Then
        notes.Should().HaveCount(count);
        foreach(var note in notes)
        {
            outputs.Should().Contain(note.ToString());
        }
    }
}