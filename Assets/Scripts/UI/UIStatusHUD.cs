using System;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusHUD : MonoBehaviour
{
    public VerticalLayoutGroup layoutGroup;

    public UICharacterStatus statusPrefab;

    private void Update()
    {
        if (layoutGroup.transform.childCount != PlayerInputScript.Shared.allCharacters.Count)
            ReloadHUD();

        RefreshHUD();
    }

    private void ReloadHUD()
    {
        foreach (Transform child in layoutGroup.transform)
            Destroy(child); 

        foreach (var character in PlayerInputScript.Shared.allCharacters)
        {
            var status = Instantiate(statusPrefab, layoutGroup.transform);
            status.characterModel = character.characterModel;
        }
    }

    private void RefreshHUD()
    {
        foreach (var character in PlayerInputScript.Shared.allCharacters)
        {
            
        }
    }
}
