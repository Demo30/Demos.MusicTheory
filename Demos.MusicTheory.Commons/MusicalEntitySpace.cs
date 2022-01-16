using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.Commons
{
    public abstract class MusicalEntitySpace<T> : IMusicalEntitySpace<T> where T: IMusicalEntity
    {
        public IMusicalEntitySpaceValidator[] Validators => _validators.ToArray();

        public IEnumerable<T> MusicalEntities { get; private set; } = Array.Empty<T>();

        private List<IMusicalEntitySpaceValidator> _validators = new();

        public void AddMusicalEntity(T musicalEntity)
        {
            Validate(musicalEntity);

            MusicalEntities = MusicalEntities
                .Union(new T[] { musicalEntity })
                .ToArray();
        }

        private void Validate(T musicalEntity)
        {
            var failedValidator = Validators.FirstOrDefault(
                validator => validator.ValidateEntityCompatibility(musicalEntity, MusicalEntities) == false);

            if (failedValidator != null)
            {
                throw new MusicalEntityValidatorException(failedValidator);
            }
        }

        protected void AddValidator(IMusicalEntitySpaceValidator validator)
        {
            validator = validator ?? throw new ArgumentNullException();
            _validators.Add(validator);
        }
    }
}
