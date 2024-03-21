using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterStatus : MonoBehaviour
{
    private Image _selfImage;
    public Image avatarImage;
    public TextMeshProUGUI charName;
    public Color selectedColor;
    public Color unselectedColor;

    public CharacterScriptableObject characterModel;

    private bool _isSelected;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _selfImage = GetComponent<Image>();
        avatarImage.sprite = characterModel.avatar;
        charName.text = characterModel.charName;
    }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            _selfImage.color = value ? selectedColor : unselectedColor;
        }
    }
}
