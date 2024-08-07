using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogMessage : MonoBehaviour
{
    public static UIDialogMessage Shared { get; private set; }

    public Image backstageColor;
    public Image backstageImage;
    public GameObject messageView;
    public Image avatar;
    public TextMeshProUGUI charName;
    public UIMessageTypewriterEffect textField;
    public AudioSource audioSource;
    
    private PlayerControlMap _controlMap;

    public bool Submit => _controlMap.UI.Submit.triggered;

    public void Awake()
    {
        Shared = this;
        audioSource = GetComponent<AudioSource>();
        _controlMap = new PlayerControlMap();
        _controlMap.UI.Submit.Enable();
    }

    public static void SetBackstageColor(Color targetColor)
    {
        Shared.backstageColor.color = targetColor;
    }
    
    public static IEnumerator SetBackstageColor(Color targetColor, float seconds)
    {
        var startTime = Time.time;
        var startColor = Shared.backstageColor.color;
        while (true)
        {
            var timePass = Time.time - startTime;
            Shared.backstageColor.color = Color.Lerp(startColor, targetColor, timePass / seconds); 
            if (timePass >= seconds) break;
            yield return null;
        }
    }
    
    public static void SetBackstageImage([CanBeNull] Sprite image)
    {
        Shared.backstageImage.color = image == null ? Color.clear : Color.white;
        Shared.backstageImage.sprite = image;
        Shared.backstageImage.preserveAspect = true;
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