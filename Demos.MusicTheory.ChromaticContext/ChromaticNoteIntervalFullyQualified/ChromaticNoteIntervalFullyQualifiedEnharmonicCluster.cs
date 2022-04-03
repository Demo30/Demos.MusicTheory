using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

public class ChromaticNoteIntervalFullyQualifiedEnharmonicCluster : IChromaticIndexSpan
{
    public int ChromaticIndexSpan => Cluster.First().ChromaticIndexSpan;
    
    public int SemitoneCount => Cluster.First().SemitoneCount;

    public ChromaticNoteIntervalFullyQualified[] Cluster
    {
        get => _cluster;
        private init
        {
            Validate(value);
            _cluster = value;
        }
    }

    private readonly ChromaticNoteIntervalFullyQualified[] _cluster;

    public ChromaticNoteIntervalFullyQualifiedEnharmonicCluster(ChromaticNoteIntervalFullyQualified[] cluster)
    {
        Cluster = cluster;
    }

    private static void Validate(ChromaticNoteIntervalFullyQualified[] cluster)
    {
        if (cluster == null || !cluster.Any())
            throw new ArgumentNullException(nameof(cluster));

        var sharedIndexSpan = cluster
            .Select(interval => interval.ChromaticIndexSpan)
            .Distinct()
            .Count() == 1;
        
        if (!sharedIndexSpan)
            throw new ArgumentException("All intervals within a cluster need to share the same chromatic index span.");
    }
}