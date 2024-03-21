using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogMessage : MonoBehaviour
{
    public static UIDialogMessage Shared { get; private set; }
    
    public GameObject messageView;
    public Image avatar;
    public TextMeshProUGUI charName;
    public UIMessageTypewriterEffect textField;
    
    private PlayerControlMap _controlMap;

    public bool Submit => _controlMap.UI.Submit.triggered;

    public void Awake()
    {
        Shared = this;
        _controlMap = new PlayerControlMap();
        _controlMap.UI.Submit.Enable();
    }

    public static IEnumerator SetMessage([CanBeNull] Sprite avatar, string name, string message)
    {
        Shared.avatar.transform.parent.gameObject.SetActive(avatar != null);
        Shared.avatar.sprite = avatar;
        Shared.charName.text = name;
        yield return Shared.textField.SetText(message);
    }

    public static void OpenMessageView()
    {
        Shared.messageView.SetActive(true);
    }

    public static void CloseMessageView()
    {
        Shared.messageView.SetActive(false);
    }
}