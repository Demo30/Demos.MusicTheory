using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified
{
    public class ChromaticNoteIntervalFullyQualifiedCluster : IChromaticIndexSpan
    {
        public int ChromaticIndexSpan => Cluster.First().ChromaticIndexSpan;

        public ChromaticNoteIntervalFullyQualified[] Cluster { get; }

        public ChromaticNoteIntervalFullyQualifiedCluster(IEnumerable<ChromaticNoteIntervalFullyQualified> cluster)
        {
            Validate(cluster);
            Cluster = cluster.ToArray();
        }

        private void Validate(IEnumerable<ChromaticNoteIntervalFullyQualified> cluster)
        {
            if (cluster == null || cluster.Count() == 0)
            {
                throw new ArgumentNullException("Cluster cannot be empty.");
            }

            bool sharedIndexSpan = cluster
                .Select(interval => interval.ChromaticIndexSpan)
                .Distinct()
                .Count() == 1;
            if (!sharedIndexSpan)
            {
                throw new ArgumentException("All intervals within a cluster need to share the same chromatic index span.");
            }
        }
    }
}
