using System.Collections;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
public class DiatonicScalesProviderFromNoteClusterTest : TestBase
{
    private DiatonicScalesProviderFromNoteCluster _provider = null!;
    
    [SetUp]
    protected void SetUp()
    {
        RegisterService<ElementaryNoteFromDiatonicScaleKeySignatureProvider>();
        _provider = new DiatonicScalesProviderFromNoteCluster();
    }

    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void ShouldReturnExpectedDiatonicScalesBasedOnSuppliedElementaryNotes(IEnumerable<ElementaryNote> elementaryNotes, IEnumerable<DiatonicScale> expectedScales)
    {
        // Given

        // When
        var scales = _provider.GetDiatonicScales(elementaryNotes)?.ToArray();

        // Then
        var expectedScalesArray = expectedScales.ToArray();

        scales.Should().NotBeNull();
        scales.Should().HaveCount(expectedScalesArray.Length);
        foreach (var resultScale in scales!)
        {
            expectedScalesArray.Should().Contain(resultScale);
        }
    }
    
    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void ShouldReturnExpectedDiatonicScalesBasedOnSuppliedFullyQualifiedNotes(IEnumerable<ElementaryNote> elementaryNotes, IEnumerable<DiatonicScale> expectedScales)
    {
        // Given
        var fullyQualifiedNotes = elementaryNotes
            .Select(x => new Note(x.Quality, 3, x.Modifier))
            .ToArray();

        // When
        var scales = _provider.GetDiatonicScales(fullyQualifiedNotes)?.ToArray();

        // Then
        var expectedScalesArray = expectedScales.ToArray();
        
        scales.Should().NotBeNull();
        scales.Should().HaveCount(expectedScalesArray.Length);
        foreach (var resultScale in scales!)
        {
            expectedScalesArray.Should().Contain(resultScale);
        }
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(Array.Empty<ElementaryNote>(), Array.Empty<DiatonicScale>())
            .SetName("Empty set of notes should return empty set of fitting diatonic scales.");

        yield return new TestCaseData(
            new[]
            {
                new ElementaryNote(NoteQuality.C, NotationSymbols.None),
                new ElementaryNote(NoteQuality.D, NotationSymbols.None),
                new ElementaryNote(NoteQuality.E, NotationSymbols.None),
                new ElementaryNote(NoteQuality.F, NotationSymbols.None),
                new ElementaryNote(NoteQuality.G, NotationSymbols.None),
                new ElementaryNote(NoteQuality.A, NotationSymbols.None),
                new ElementaryNote(NoteQuality.B, NotationSymbols.None),
            },
            new[]
            {
                new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.A, NotationSymbols.None, DiatonicScaleType.Minor)
            }
        ).SetName("All basic notes without accidents should be valid for C-major and A-minor scale");

        yield return new TestCaseData(
            new[]
            {
                new ElementaryNote(NoteQuality.C, NotationSymbols.None),
            },
            new[]
            {
                new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.G, NotationSymbols.None, DiatonicScaleType.Major),
                // See C#/Db possible confusion - but this should be correct, Db-Mj has C, but C#-Mj does not
                new DiatonicScale(NoteQuality.D, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.A, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.E, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.B, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.F, NotationSymbols.None, DiatonicScaleType.Major),
                
                new DiatonicScale(NoteQuality.A, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQuality.E, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQuality.B, NotationSymbols.Flat, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQuality.F, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQuality.C, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQuality.G, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQuality.D, NotationSymbols.None, DiatonicScaleType.Minor),
            }
        ).SetName("Natural C note should match many scales.");
        
        yield return new TestCaseData(
            new[]
            {
                new ElementaryNote(NoteQuality.C, NotationSymbols.None),
                new ElementaryNote(NoteQuality.C, NotationSymbols.Sharp),
            },
            Array.Empty<DiatonicScale>()
        ).SetName("Self-conflicting notes should not match any diatonic scale.");

        yield return new TestCaseData(
            new[]
            {
                new ElementaryNote(NoteQuality.C, NotationSymbols.None),
                new ElementaryNote(NoteQuality.B, NotationSymbols.Flat),
                new ElementaryNote(NoteQuality.D, NotationSymbols.Flat),
            },
            new[]
            {
                new DiatonicScale(NoteQuality.A, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.F, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQuality.D, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQuality.B, NotationSymbols.Flat, DiatonicScaleType.Minor),
            }
        );
    }
}