using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext;
using Demos.MusicTheory.ChromaticContext.ChromaticNoteFullyQualified;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext;
using Demos.MusicTheory.ChromaticContext.DiatonicSubcontext.Providers;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;

namespace Demos.MusicTheory.Tests.IntegrationTests.Tests.ChromaticContext;

[TestFixture]
internal class DiatonicScalesProviderFromNoteClusterTest : TestBase
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
    public void ShouldReturnExpectedDiatonicScalesBasedOnSuppliedElementaryNotes(IEnumerable<ElementaryNoteInternal> elementaryNotes, IEnumerable<DiatonicScale> expectedScales)
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
    public void ShouldReturnExpectedDiatonicScalesBasedOnSuppliedFullyQualifiedNotes(IEnumerable<ElementaryNoteInternal> elementaryNotes, IEnumerable<DiatonicScale> expectedScales)
    {
        // Given
        var fullyQualifiedNotes = elementaryNotes
            .Select(x => new NoteInternal(x.QualityInternal, 3, x.Modifier))
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
        yield return new TestCaseData(Array.Empty<ElementaryNoteInternal>(), Array.Empty<DiatonicScale>())
            .SetName("Empty set of notes should return empty set of fitting diatonic scales.");

        yield return new TestCaseData(
            new[]
            {
                new ElementaryNoteInternal(NoteQualityInternal.C, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.D, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.E, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.F, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.G, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.A, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.B, NotationSymbols.None),
            },
            new[]
            {
                new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.A, NotationSymbols.None, DiatonicScaleType.Minor)
            }
        ).SetName("All basic notes without accidents should be valid for C-major and A-minor scale");

        yield return new TestCaseData(
            new[]
            {
                new ElementaryNoteInternal(NoteQualityInternal.C, NotationSymbols.None),
            },
            new[]
            {
                new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.G, NotationSymbols.None, DiatonicScaleType.Major),
                // See C#/Db possible confusion - but this should be correct, Db-Mj has C, but C#-Mj does not
                new DiatonicScale(NoteQualityInternal.D, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.A, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.E, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.B, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.F, NotationSymbols.None, DiatonicScaleType.Major),
                
                new DiatonicScale(NoteQualityInternal.A, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQualityInternal.E, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQualityInternal.B, NotationSymbols.Flat, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQualityInternal.F, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQualityInternal.C, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQualityInternal.G, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQualityInternal.D, NotationSymbols.None, DiatonicScaleType.Minor),
            }
        ).SetName("Natural C note should match many scales.");
        
        yield return new TestCaseData(
            new[]
            {
                new ElementaryNoteInternal(NoteQualityInternal.C, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.C, NotationSymbols.Sharp),
            },
            Array.Empty<DiatonicScale>()
        ).SetName("Self-conflicting notes should not match any diatonic scale.");

        yield return new TestCaseData(
            new[]
            {
                new ElementaryNoteInternal(NoteQualityInternal.C, NotationSymbols.None),
                new ElementaryNoteInternal(NoteQualityInternal.B, NotationSymbols.Flat),
                new ElementaryNoteInternal(NoteQualityInternal.D, NotationSymbols.Flat),
            },
            new[]
            {
                new DiatonicScale(NoteQualityInternal.A, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.F, NotationSymbols.None, DiatonicScaleType.Minor),
                new DiatonicScale(NoteQualityInternal.D, NotationSymbols.Flat, DiatonicScaleType.Major),
                new DiatonicScale(NoteQualityInternal.B, NotationSymbols.Flat, DiatonicScaleType.Minor),
            }
        );
    }
}