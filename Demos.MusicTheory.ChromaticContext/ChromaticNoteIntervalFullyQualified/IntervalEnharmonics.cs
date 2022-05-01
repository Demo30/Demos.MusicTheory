using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

public class IntervalEnharmonics : IChromaticIndexSpan
{
    public int ChromaticIndexSpan => Intervals.First().ChromaticIndexSpan;

    public int SemitoneCount => Intervals.First().SemitoneCount;

    public Interval[] Intervals
    {
        get => _intervals;
        private init
        {
            Validate(value);
            _intervals = value;
        }
    }

    private readonly Interval[] _intervals;

    public IntervalEnharmonics(Interval[] intervals)
    {
        Intervals = intervals;
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