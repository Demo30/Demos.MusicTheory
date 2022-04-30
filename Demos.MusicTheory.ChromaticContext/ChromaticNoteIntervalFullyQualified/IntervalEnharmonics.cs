using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

public class IntervalEnharmonics : IChromaticIndexSpan
{
    public int ChromaticIndexSpan => Cluster.First().ChromaticIndexSpan;

    public int SemitoneCount => Cluster.First().SemitoneCount;

    public Interval[] Cluster
    {
        get => _cluster;
        private init
        {
            Validate(value);
            _cluster = value;
        }
    }

    private readonly Interval[] _cluster;

    public IntervalEnharmonics(Interval[] cluster)
    {
        Cluster = cluster;
    }

    private static void Validate(Interval[] cluster)
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