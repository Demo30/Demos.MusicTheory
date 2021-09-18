using Demos.MusicTheory.Abstractions.ChromaticContext;
using Demos.MusicTheory.ChromaticContext;
using System;
using System.Linq;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticNoteEnharmonicCluster :
        IChromaticEntity
    {
        public int ChromaticContextIndex => Cluster.First().ChromaticContextIndex;

        public ChromaticNoteFullyQualified[] Cluster { get; }

        public ChromaticNoteEnharmonicCluster(ChromaticNoteFullyQualified[] enharmonicNotes)
        {
            enharmonicNotes = enharmonicNotes != null && enharmonicNotes.Length > 0 ?
                enharmonicNotes :
                throw new ArgumentNullException(nameof(enharmonicNotes));

            CheckEnharmonicNoteCompatibility(enharmonicNotes);
        }

        private void CheckEnharmonicNoteCompatibility(ChromaticNoteFullyQualified[] enharmonicNotes)
        {
            bool valid = enharmonicNotes
                .Select(en => en.ChromaticContextIndex)
                .Distinct()
                .Count() == 1;

            if (!valid)
            {
                throw new ArgumentException("Supplied collection of chromatic notes cannot form an enharmonic cluster.");
            }
        }
    }
}
