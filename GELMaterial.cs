using System;
using System.Numerics;

namespace Fusyon.GEL
{
    public class GELMaterial
    {
        public Action<string, Vector4> SetVector4 { get; internal set; }

        internal object Source { get; }

        public GELMaterial(object material)
        {
            Source = material;
            SetVector4 = null;
        }
    }
}