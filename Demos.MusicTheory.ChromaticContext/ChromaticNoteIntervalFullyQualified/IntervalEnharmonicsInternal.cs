using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos.MusicTheory.ChromaticContext.ChromaticNoteIntervalFullyQualified;

internal class IntervalEnharmonicsInternal : IChromaticIndexSpan
{
    public int ChromaticIndexSpan => Intervals.First().ChromaticIndexSpan;

    public int SemitoneCount => Intervals.First().SemitoneCount;

    public IntervalInternal[] Intervals
    {
        get => _intervals;
        private init
        {
            Validate(value);
            _intervals = value;
        }
    }

    private readonly IntervalInternal[] _intervals;

    public IntervalEnharmonicsInternal(IntervalInternal[] intervals)
    {
        Intervals = intervals;
    }

    private static void Validate(IntervalInternal[] cluster)
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