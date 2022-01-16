using Demos.MusicTheory.Commons;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Demos.MusicTheory.ChromaticContext;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext.ChromaticSpace;

[TestFixture]
public class ChromaticSpaceIndexValidatorTests
{
    [Theory]
    public void ValidationShouldThrowException_OnConflictingIndexes()
    {
        // Given
        IChromaticEntity entity = new ChromaticEntity(2);
        IEnumerable<IChromaticEntity> entitites = new[]
        {
            new ChromaticEntity(1),
            new ChromaticEntity(2),
            new ChromaticEntity(3)
        };

        ChromaticSpaceIndexValidator indexValidator = new ChromaticSpaceIndexValidator();

        // When
        Action act = () => { indexValidator.ValidateEntityCompatibility(entity, entitites); };

        // Then
        act.Should().Throw<MusicalEntityValidatorException>();
    }

    [Theory]
    public void ValidationShouldSucceed_WhenNoConflictingNotes()
    {
        // Given
        IChromaticEntity entity = new ChromaticEntity(2);
        IEnumerable<IChromaticEntity> entitites = new[]
        {
            new ChromaticEntity(1),
            new ChromaticEntity(3),
            new ChromaticEntity(4)
        };

        ChromaticSpaceIndexValidator indexValidator = new ChromaticSpaceIndexValidator();

        // When
        Action act = () => { indexValidator.ValidateEntityCompatibility(entity, entitites); };

        // Then
        act.Should().NotThrow();
    }

    [Theory]
    [Test]
    public void ValidationShouldSucceed_WhenArrayEmpty()
    {
        // Given
        IChromaticEntity entity = new ChromaticEntity(2);
        IEnumerable<IChromaticEntity> entities = Array.Empty<IChromaticEntity>();

        var indexValidator = new ChromaticSpaceIndexValidator();

        // When
        var act = () => { indexValidator.ValidateEntityCompatibility(entity, entities); };

        // Then
        act.Should().NotThrow();
    }

    [Theory]
    public void ThrowOnIncompatibleEntityAdded()
    {
        // Given
        Contexts.PhysicalContext.Tone entity = new Contexts.PhysicalContext.Tone(440);
        IEnumerable<Contexts.PhysicalContext.Tone> entitites = Array.Empty<Contexts.PhysicalContext.Tone>();

        ChromaticSpaceIndexValidator indexValidator = new ChromaticSpaceIndexValidator();

        // When
        Action act = () => { indexValidator.ValidateEntityCompatibility(entity, entitites); };

        // Then
        act.Should().Throw<ArgumentException>();
    }
}