using UnityEngine;
using URPGlitch;

public class Act4Script : MonoBehaviour
{
    public static Act4Script Shared { get; private set; }
    public Act4ShakeEffect crazyOverlay;

    [Range(0, 1)]
    public float crazyLevel = 0;

    // public float glitchEffectValue = 0;
    
    public float CrazyRange = 70;
    public float CrazySpeed = 90;

    private AnalogGlitchVolume _dreamGlitchEffect;
    private bool _firstTime = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Shared = this;
    }

    // Update is called once per frame
    void Update()
    {
        crazyOverlay.alpha = crazyLevel;
        crazyOverlay.shakeRange = CrazyRange * crazyLevel;
        crazyOverlay.shakeSpeed = CrazySpeed * crazyLevel;
    }

    public static void SetCrazyLevel(float crazyLevel)
    {
        Shared.crazyLevel = crazyLevel;
        Shared.SetDreamGlitchEffectWeight(crazyLevel);
    }
    
    private void SetDreamGlitchEffectWeight(float weight)
    {
        if (_firstTime)
        {
            _firstTime = false;
            var postEffectVolume = GlobalDirector.Shared.postEffectVolume;
            var profile = postEffectVolume.profile;
            if (profile == null && postEffectVolume.sharedProfile != null)
            {
                postEffectVolume.profile = Instantiate(postEffectVolume.sharedProfile);
                profile = postEffectVolume.profile;
            }
        
            if (!profile.TryGet<AnalogGlitchVolume>(out _dreamGlitchEffect)) return;
        
            _dreamGlitchEffect.scanLineJitter.overrideState = true;
            _dreamGlitchEffect.horizontalShake.overrideState = true;
        }

        _dreamGlitchEffect.scanLineJitter.value = weight * 0.25f;
        _dreamGlitchEffect.horizontalShake.value = weight * 0.05f;
    }
}
