using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.Commons;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.ChromaticContext;

namespace Demos.MusicTheory.UnitTests.Tests.ChromaticContext.ChromaticSpace
{
    [TestFixture]
    public class ChromaticSpaceTests
    {
        [Theory]
        public void ValidatorsRunOnEntityAdd()
        {
            // Given
            var chromaticNote = new ChromaticEntity(1);

            Mock<IChromaticSpaceIndexValidator> indexValidator = new Mock<IChromaticSpaceIndexValidator>();
            indexValidator
                .Setup(mock => mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()))
                .Returns(true);

            MusicTheory.ChromaticContext.ChromaticSpace chromaticSpace = new MusicTheory.ChromaticContext.ChromaticSpace(indexValidator.Object);

            // When
            chromaticSpace.AddMusicalEntity(chromaticNote);

            // Then
            indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()),
                Times.Once);
        }

        [Theory]
        public void EvaluatesValidatorOnAdd()
        {
            // Given
            var chromaticNote = new ChromaticEntity(1);

            Mock<IChromaticSpaceIndexValidator> indexValidator = new Mock<IChromaticSpaceIndexValidator>();
            indexValidator
                .Setup(mock => mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()))
                .Returns(false);

            MusicTheory.ChromaticContext.ChromaticSpace chromaticSpace = new MusicTheory.ChromaticContext.ChromaticSpace(indexValidator.Object);

            // When
            Action act = () => { chromaticSpace.AddMusicalEntity(chromaticNote); };

            // Then
            act.Should().Throw<MusicalEntityValidatorException>();
            indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()),
                Times.Once);
        }

        [Theory]
        public void ChromaticNotesAdded()
        {
            // Given
            IEnumerable<IChromaticEntity> notes = new[]
            {
                new ChromaticEntity(1),
                new ChromaticEntity(2),
                new ChromaticEntity(3),
                new ChromaticEntity(5)
            };
            int numberOfNotes = notes.Count();

            Mock<IChromaticSpaceIndexValidator> indexValidator = new Mock<IChromaticSpaceIndexValidator>();
            indexValidator
                .Setup(mock => mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()))
                .Returns(true);

            MusicTheory.ChromaticContext.ChromaticSpace chromaticSpace = new MusicTheory.ChromaticContext.ChromaticSpace(indexValidator.Object);

            // When
            notes.ToList().ForEach(note => chromaticSpace.AddMusicalEntity(note));

            // Then
            indexValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<IChromaticEntity>(),
                    It.IsAny<IChromaticEntity[]>()),
                Times.Exactly(numberOfNotes));
            chromaticSpace.MusicalEntities.Should().HaveCount(numberOfNotes);
        }
    }
}
