using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Act4ShakeEffect : MonoBehaviour
{
    public Vector2 origin;
    public float shakeRange = 0.2f;
    public float shakeSpeed = 1;
    public float alpha = 1;
    
    private Vector2 _nextTarget;
    private RectTransform _rectTransform;
    private Image _image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        origin = _rectTransform.anchoredPosition;
        SetNextTarget();
    }

    // Update is called once per frame
    void Update()
    {
        var color = _image.color;
        color.a = alpha;
        _image.color = color;
        
        if (Vector2.Distance(_rectTransform.anchoredPosition, _nextTarget) < 0.1)
            SetNextTarget();
        
        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _nextTarget, shakeSpeed * Time.deltaTime);
        // Debug.Log($"{_rectTransform.anchoredPosition} to ");
    }

    private void SetNextTarget()
    {
        var x = Random.Range(-shakeRange, shakeRange);
        var y = Random.Range(-shakeRange, shakeRange);
        _nextTarget = new Vector2(origin.x + x, origin.y + y);
    }
}
