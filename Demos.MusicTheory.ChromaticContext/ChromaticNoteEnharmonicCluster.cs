using Demos.MusicTheory.ChromaticContext;
using System;
using System.Linq;

namespace Demos.MusicTheory.Contexts.ChromaticContext
{
    public class ChromaticNoteEnharmonicCluster : ChromaticEntity
    {
        public ChromaticNoteFullyQualified[] Cluster { get; }

        public ChromaticNoteEnharmonicCluster(ChromaticNoteFullyQualified[] enharmonicNotes) : base(GetChromaticContextIndex(enharmonicNotes))
        {
            enharmonicNotes = enharmonicNotes != null && enharmonicNotes.Length > 0 ?
                enharmonicNotes :
                throw new ArgumentNullException(nameof(enharmonicNotes));

            CheckEnharmonicNoteCompatibility(enharmonicNotes);

            Cluster = enharmonicNotes;
        }

        private static int GetChromaticContextIndex(ChromaticNoteFullyQualified[] enharmonicNotes) =>
            (enharmonicNotes != null && enharmonicNotes.Length > 0) ? enharmonicNotes.First().ChromaticContextIndex : -1;

        private void CheckEnharmonicNoteCompatibility(ChromaticNoteFullyQualified[] enharmonicNotes)
        {
            bool valid = enharmonicNotes
                .Select(en => en.ChromaticContextIndex)
                .Distinct()
                .Count() == 1;

            if (!valid)
            {
                throw new ArgumentException("Supplied collection of chromatic notes cannot form an enharmonic cluster due to unmatching chromatic context index.");
            }
        }
    }
}
