using System.Collections.Generic;

namespace Demos.MusicTheory.Abstractions.Commons
{
    public interface IMusicalEntitySpace<T> where T : IMusicalEntity
    {
        public IMusicalEntitySpaceValidator[] Validators { get; }

        public IEnumerable<T> MusicalEntities { get; }

        void AddMusicalEntity(T musicalEntity);
    }
}
