using System;
using Demos.MusicTheory.Commons;

namespace Demos.MusicTheory.Mappers;

public static class GeneralMapper
{
    public static Direction Map(OneDimensionalDirection directionInternal)
    {
        return directionInternal switch
        {
            OneDimensionalDirection.RIGHT => Direction.Right,
            OneDimensionalDirection.LEFT => Direction.Left,
            _ => throw new InvalidOperationException()
        };
    }
    
    public static OneDimensionalDirection Map(Direction direction)
    {
        return direction switch
        {
            Direction.Right => OneDimensionalDirection.RIGHT,
            Direction.Left => OneDimensionalDirection.LEFT,
            _ => throw new InvalidOperationException()
        };
    }
}