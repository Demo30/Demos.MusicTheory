using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Demos.MusicTheory.PhysicalContext;
using ITone = Demos.MusicTheory.PhysicalContext.ITone;
using IToneSpaceFrequencyValidator = Demos.MusicTheory.PhysicalContext.IToneSpaceFrequencyValidator;

namespace Demos.MusicTheory.UnitTests.Tests.PhysicalContext
{
    [TestFixture]
    public class ToneSpaceTests
    {
        [Theory]
        public void TonesAdded()
        {
            // Given
            IEnumerable<ITone> notes = new[]
            {
                new Contexts.PhysicalContext.Tone(200),
                new Contexts.PhysicalContext.Tone(400),
                new Contexts.PhysicalContext.Tone(450),
                new Contexts.PhysicalContext.Tone(470),
            };
            int numberOfNotes = notes.Count();

            Mock<IToneSpaceFrequencyValidator> frequencyValidator = new Mock<IToneSpaceFrequencyValidator>();
            frequencyValidator
                .Setup(mock => mock.ValidateEntityCompatibility(
                    It.IsAny<ITone>(),
                    It.IsAny<ITone[]>()))
                .Returns(true);

            ToneSpace toneSpace = new ToneSpace(frequencyValidator.Object);

            // When
            notes.ToList().ForEach(tone => toneSpace.AddMusicalEntity(tone));

            // Then
            frequencyValidator.Verify(mock =>
                mock.ValidateEntityCompatibility(
                    It.IsAny<ITone>(),
                    It.IsAny<ITone[]>()),
                Times.Exactly(numberOfNotes));
            toneSpace.MusicalEntities.Should().HaveCount(numberOfNotes);
        }
    }
}
