using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class PressurePlateScript : Identifiable
{
    public Sprite unpressedSprite;
    public Sprite pressedSprite;
    
    public bool IsPressed => _stayCounter > 0;

    private SpriteRenderer _spriteRenderer;
    private int _stayCounter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = unpressedSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var entity = col.GetComponent<CharacterScript>();
        if (!entity || string.IsNullOrEmpty(objectId)) { return; }

        var oldValue = _stayCounter;
        _stayCounter++;
        
        if (oldValue == 0)
        {
            EventBus.Trigger("InteractionTriggerEnterUnit", (objectId, entity));
            _spriteRenderer.sprite = pressedSprite;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        var entity = col.GetComponent<CharacterScript>();
        if (!entity || string.IsNullOrEmpty(objectId)) { return; }
        
        // Debug.Log($"Exit: {entity.objectId}");
        
        _stayCounter--;
        
        if (_stayCounter == 0)
        {
            EventBus.Trigger("InteractionTriggerExitUnit", (objectId, entity));
            _spriteRenderer.sprite = unpressedSprite;
        }
    }
}
