using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterStatus : MonoBehaviour
{
    public Image avatarImage;
    public TextMeshProUGUI charName;

    public CharacterScriptableObject characterModel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        avatarImage.sprite = characterModel.avatar;
        charName.text = characterModel.charName;
    }
}
