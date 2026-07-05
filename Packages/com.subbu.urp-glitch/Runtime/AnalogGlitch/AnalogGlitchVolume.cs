using System;
using UnityEngine.Rendering;

namespace URPGlitch
{
    [Serializable]
    [VolumeComponentMenu("Analog Glitch")]
    public class AnalogGlitchVolume : VolumeComponent
    {
        internal const float ActiveEpsilon = 0.0001f;

        public ClampedFloatParameter scanLineJitter = new(0f, 0f, 1f);
        public ClampedFloatParameter verticalJump = new(0f, 0f, 1f);
        public ClampedFloatParameter horizontalShake = new(0f, 0f, 1f);
        public ClampedFloatParameter colorDrift = new(0f, 0f, 1f);

        public bool IsActive => active &&
                                (scanLineJitter.value > ActiveEpsilon ||
                                 verticalJump.value > ActiveEpsilon ||
                                 horizontalShake.value > ActiveEpsilon ||
                                 colorDrift.value > ActiveEpsilon);
    }
}
