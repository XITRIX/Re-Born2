using UnityEngine;

public class Act4Script : MonoBehaviour
{
    public static Act4Script Shared { get; private set; }
    public Act4ShakeEffect crazyOverlay;

    [Range(0, 1)]
    public float crazyLevel = 0;

    private const float CrazyRange = 70;
    private const float CrazySpeed = 90;

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
    }
}
