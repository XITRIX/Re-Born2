using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
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
    public TextMeshProUGUI noteLabel;

    private float noteLabelFadeTime = 0;
    private PlayerControlMap _controlMap;

    public bool Submit => _controlMap.UI.Submit.triggered;

    public void Awake()
    {
        Shared = this;
        audioSource = GetComponent<AudioSource>();
        _controlMap = new PlayerControlMap();
        _controlMap.UI.Submit.Enable();
    }
    void Update()
    {
        if (noteLabel.alpha != 0 && Time.time > noteLabelFadeTime)
        {
            noteLabel.alpha = 1 - (Time.time - noteLabelFadeTime) / 0.5f;
        }
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
    
    public static void ShowNote(string text, float seconds = 2)
    {
        Shared.noteLabel.text = "<mark=#00000077>" + text + "</mark>";
        Shared.noteLabel.alpha = 1;
        Shared.noteLabelFadeTime = Time.time + seconds;
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