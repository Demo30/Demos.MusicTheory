using System;
using System.Collections.Generic;

namespace Demos.MusicTheory.IntervalChromaticFullyQualifiedResolvers
{
    public interface IIntervalChromaticFullyQualifiedResolver<TArgument>
    {
        IntervalChromaticFullyQualified Resolve(TArgument argument);
    }
}
