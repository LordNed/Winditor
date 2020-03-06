using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Enums
{
    public enum GXAttenuationFn
    {
        // Specular Computation
        Spec = 0,
        // Spot Light Attenuation
        Spot = 1,
        // No attenuation
        None = 2
    }
}
