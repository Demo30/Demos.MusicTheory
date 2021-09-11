using Demos.MusicTheory.Abstractions.Commons;
using System;

namespace Demos.MusicTheory.Commons
{
    public class MusicalEntityValidatorException : Exception
    {
        public IMusicalEntitySpaceValidator Validator { get; set; }

        public MusicalEntityValidatorException(IMusicalEntitySpaceValidator validator) : base()
        {
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
    }
}
