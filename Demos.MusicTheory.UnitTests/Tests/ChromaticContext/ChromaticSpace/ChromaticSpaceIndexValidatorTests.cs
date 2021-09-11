﻿using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Commons;
using Demos.MusicTheory.Contexts.ChromaticContext;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext.ChromaticSpace
{
    [TestFixture]
    public class ChromaticSpaceIndexValidatorTests
    {
        [Theory]
        public void ValidationShouldThrowException_OnConflictingIndexes()
        {
            // Given
            IChromaticNote entity = new Contexts.ChromaticContext.ChromaticNote(2);
            IEnumerable<IChromaticNote> entitites = new[]
            {
                new Contexts.ChromaticContext.ChromaticNote(1),
                new Contexts.ChromaticContext.ChromaticNote(2),
                new Contexts.ChromaticContext.ChromaticNote(3)
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
            IChromaticNote entity = new Contexts.ChromaticContext.ChromaticNote(2);
            IEnumerable<IChromaticNote> entitites = new[]
            {
                new Contexts.ChromaticContext.ChromaticNote(1),
                new Contexts.ChromaticContext.ChromaticNote(3),
                new Contexts.ChromaticContext.ChromaticNote(4)
            };

            ChromaticSpaceIndexValidator indexValidator = new ChromaticSpaceIndexValidator();

            // When
            Action act = () => { indexValidator.ValidateEntityCompatibility(entity, entitites); };

            // Then
            act.Should().NotThrow();
        }

        [Theory]
        public void ValidationShouldSucceed_WhenArrayEmpty()
        {
            // Given
            IChromaticNote entity = new Contexts.ChromaticContext.ChromaticNote(2);
            IEnumerable<IChromaticNote> entitites = Array.Empty<IChromaticNote>();

            ChromaticSpaceIndexValidator indexValidator = new ChromaticSpaceIndexValidator();

            // When
            Action act = () => { indexValidator.ValidateEntityCompatibility(entity, entitites); };

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
}
