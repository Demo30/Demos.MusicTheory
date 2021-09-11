using System;

namespace Demos.MusicTheory.Contexts.PhysicalContext
{
    public abstract class ToneProvider<T> : ToneProviderBase
    {
        public override Type Type => typeof(T);

        public T BuildParameters { get; }

        public ToneProvider(T parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException();
            }

            BuildParameters = parameters;
        }
    }
}
