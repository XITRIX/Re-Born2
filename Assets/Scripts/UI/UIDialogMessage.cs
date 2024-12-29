using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class UIDialogMessage : MonoBehaviour
{
    public static UIDialogMessage Shared { get; private set; }

    public DialogCharacterScript dialogCharPrefab;
    public Image backstageColor;
    public Image backstageImage;
    public Image backstageSwapImage;
    public GameObject dialogCharactersHolder;
    public GameObject messageView;
    public Image avatar;
    public TextMeshProUGUI charName;
    public UIMessageTypewriterEffect textField;
    public AudioSource audioSource;
    public TextMeshProUGUI noteLabel;
    public GameObject dialogOptionsHolder;
    public GameObject dialogOptionButtonPrefab;

    private float noteLabelFadeTime = 0;
    private PlayerControlMap _controlMap;
    public Dictionary<CharacterScriptableObject, DialogCharacterScript> DialogCharacters { get; private set; } = new();

    public bool Submit => _controlMap.UI.Submit.triggered;
    public bool DialogNext => _controlMap.UI.DialogNext.triggered;

    public void Awake()
    {
        Shared = this;
        audioSource = GetComponent<AudioSource>();
        _controlMap = new PlayerControlMap();
        _controlMap.UI.Submit.Enable();
        _controlMap.UI.DialogNext.Enable();
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

    public static IEnumerator SetBackstageImage([CanBeNull] Sprite image, float seconds)
    {
        if (seconds < 0.001f)
        {
             SetBackstageImage(image);
        }
        else if (image != null)
        {
            Shared.backstageSwapImage.color = Shared.backstageImage.color;
            Shared.backstageSwapImage.sprite = Shared.backstageImage.sprite;
            Shared.backstageSwapImage.preserveAspect = Shared.backstageImage.preserveAspect;

            Shared.backstageImage.color = Color.clear;
            Shared.backstageImage.sprite = image;
            Shared.backstageImage.preserveAspect = true;

            var startTime = Time.time;
            var startOldColor = Shared.backstageSwapImage.color;
            var startColor = Color.clear;
            var targetColor = Color.white;
            while (true)
            {
                var timePass = Time.time - startTime;
                Shared.backstageImage.color = Color.Lerp(startColor, targetColor, timePass / seconds);
                Shared.backstageSwapImage.color = Color.Lerp(startOldColor, Color.clear, timePass / seconds);
                if (timePass >= seconds) break;
                yield return null;
            }

            Shared.backstageSwapImage.sprite = null;
            Shared.backstageSwapImage.color = Color.clear;
        }
        else
        {
            var startTime = Time.time;
            var startColor = Shared.backstageImage.color;
            var targetColor = Color.clear;
            while (true)
            {
                var timePass = Time.time - startTime;
                Shared.backstageImage.color = Color.Lerp(startColor, targetColor, timePass / seconds);
                if (timePass >= seconds) break;
                yield return null;
            }
            
            Shared.backstageImage.sprite = null;
        }
    }
    
    public static IEnumerator SetMessage([CanBeNull] Sprite avatar, string name, string message)
    {
        yield return SetMessage(avatar, name, Color.white, message);
    }
    
    public static IEnumerator SetMessage([CanBeNull] Sprite avatar, string name, Color nameColor, string message)
    {
        Shared.avatar.transform.parent.gameObject.SetActive(avatar != null);
        Shared.avatar.sprite = avatar;
        Shared.charName.text = name;
        Shared.charName.color = nameColor;
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

    public enum PresentationType
    {
        Fade,
        LeftSlide,
        RightSlide
    }
    
    public static IEnumerator ShowCharacter(CharacterScriptableObject character, Vector2 position, PresentationType presentationType, bool flipped, float seconds)
    {
        var prefab = Instantiate(Shared.dialogCharPrefab, Shared.dialogCharactersHolder.transform);
        Shared.DialogCharacters[character] = prefab;
        prefab.Set(character);
        
        if (flipped)
            prefab.Flip();

        if (presentationType == PresentationType.Fade)
            prefab.Position = position;
        
        var startTime = Time.time;
        while (true)
        {
            var timePass = Time.time - startTime;
            var progress = seconds > 0 ? timePass / seconds : 1;
            
            switch (presentationType)
            {
                case PresentationType.Fade:
                    prefab.Position = position;
                    prefab.Alpha = Mathf.Lerp(0, 1, progress);
                    break;
                case PresentationType.LeftSlide:
                    prefab.Position = Vector2.Lerp(Vector2.left * 1.5f, position, progress);
                    break;
                case PresentationType.RightSlide:
                    prefab.Position = Vector2.Lerp(Vector2.right * 1.5f, position, progress);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(presentationType), presentationType, null);
            }
            
            if (timePass >= seconds) break;
            yield return null;
        }
    }
    
    public static IEnumerator HideCharacter(CharacterScriptableObject character, PresentationType presentationType, bool flipped, float seconds)
    {
        var prefab = Shared.DialogCharacters[character];
        Shared.DialogCharacters.Remove(character);
        
        if (flipped)
            prefab.Flip();

        var startTime = Time.time;
        var startPosition = prefab.Position;
        var startAlpha = prefab.Alpha;
        while (true)
        {
            var timePass = Time.time - startTime;
            var progress = seconds > 0 ? timePass / seconds : 1;
            
            switch (presentationType)
            {
                case PresentationType.Fade:
                    prefab.Alpha = Mathf.Lerp(startAlpha, 0, progress);
                    break;
                case PresentationType.LeftSlide:
                    prefab.Position = Vector2.Lerp(startPosition, Vector2.left * 1.5f, progress);
                    break;
                case PresentationType.RightSlide:
                    prefab.Position = Vector2.Lerp(startPosition, Vector2.right * 1.5f, progress);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(presentationType), presentationType, null);
            }
            
            if (timePass >= seconds) break;
            yield return null;
        }
        
        Destroy(prefab.gameObject);
    }
    
    public static IEnumerator MoveCharacter(CharacterScriptableObject character, Vector2 position, bool flipped, float seconds)
    {
        var prefab = Shared.DialogCharacters[character];
        
        if (flipped)
            prefab.Flip();

        var startTime = Time.time;
        var startPosition = prefab.Position;
        while (true)
        {
            var timePass = Time.time - startTime;
            var progress = seconds > 0 ? timePass / seconds : 1;
            
            prefab.Position = Vector2.Lerp(startPosition, position, progress);
            
            if (timePass >= seconds) break;
            yield return null;
        }
    }
}