using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusHUD : MonoBehaviour
{
    public VerticalLayoutGroup layoutGroup;

    public UICharacterStatus statusPrefab;

    private List<UICharacterStatus> characterStatusItems = new();
    private void Update()
    {
        if (layoutGroup.transform.childCount != PlayerInputScript.Shared.AllCharacters.Count)
            ReloadHUD();

        RefreshHUD();
    }

    private void ReloadHUD()
    {
        foreach (var child in characterStatusItems)
            Destroy(child); 
        characterStatusItems.Clear();

        foreach (var character in PlayerInputScript.Shared.AllCharacters)
        {
            statusPrefab.characterModel = character.characterModel;
            var status = Instantiate(statusPrefab, layoutGroup.transform);
            characterStatusItems.Add(status);
        }
    }

    private void RefreshHUD()
    {
        for (var i = 0; i < characterStatusItems.Count; i++)
        {
            characterStatusItems[i].IsSelected = i == PlayerInputScript.Shared.activeCharacterIndex;
        }
    }
}
