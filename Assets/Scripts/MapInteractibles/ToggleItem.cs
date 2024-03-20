using System;
using UnityEngine;

public class ToggleItem : Interactable
{
    public Sprite disabledSprite;
    public Sprite enabledSprite;

    public bool isEnabled;

    private SpriteRenderer _spriteRenderer;
    private int _stayCounter;

    public override Action InteractionScenario => () =>
    {
        isEnabled = !isEnabled;
        UpdateSprite();
        base.InteractionScenario();
    };

    public override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        _spriteRenderer.sprite = isEnabled ? enabledSprite : disabledSprite;
    }
}
