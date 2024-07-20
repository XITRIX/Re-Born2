using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MarqueeTextEffect : MonoBehaviour
{
    public float speed = 10;
    public float padding = 10;
    
    private TextMeshProUGUI _textObject;
    private TextMeshProUGUI _textObjectClone;

    private RectTransform _textRectTransform;
    private RectTransform _textCloneRectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textObject = GetComponent<TextMeshProUGUI>();
        _textRectTransform = _textObject.GetComponent<RectTransform>();
        
        _textObjectClone = Instantiate(_textObject, _textRectTransform);
        _textObjectClone.GetComponent<MarqueeTextEffect>().enabled = false;
        
        _textCloneRectTransform = _textObjectClone.GetComponent<RectTransform>();
        // cloneRectTransform.SetParent(_textRectTransform);
        // cloneRectTransform.anchorMin = new Vector2(1, 0.5f);
        // cloneRectTransform.localScale = Vector3.one;

        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        var pos = _textCloneRectTransform.localPosition;
        pos.x = _textRectTransform.rect.width + padding;
        _textCloneRectTransform.localPosition = pos;

        pos = _textRectTransform.position;
        pos.x -= speed * Time.deltaTime;

        if (pos.x <= -_textRectTransform.rect.width)
        {
            pos.x += _textRectTransform.rect.width + padding;
        }
        
        _textRectTransform.position = pos;
    }
}
