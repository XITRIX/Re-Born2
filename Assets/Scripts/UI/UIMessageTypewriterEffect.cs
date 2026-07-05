using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;

[RequireComponent(typeof(TMP_Text))]
public class UIMessageTypewriterEffect : MonoBehaviour
{
    private TextMeshProUGUI _textBox;
    private bool startSkipping = false;
    private PlayerControlMap _controlMap;
    private float _lastTime; 
    
    private bool IsSkipping => Math.Abs(_controlMap.UI.Submit.ReadValue<float>() - 1) < 0.1f;
    private bool IsGoNext => Math.Abs(_controlMap.UI.DialogNext.ReadValue<float>() - 1) < 0.1f;
    private bool IsGoFast => Math.Abs(_controlMap.UI.DialogFast.ReadValue<float>() - 1) < 0.1f;

    private void Awake()
    {
        _textBox = GetComponent<TextMeshProUGUI>();
        _controlMap = new PlayerControlMap();
        _controlMap.UI.Submit.Enable();
        _controlMap.UI.DialogNext.Enable();
        _controlMap.UI.DialogFast.Enable();
    }

    public IEnumerator SetText(string text)
    {
        yield return StartCoroutine(RevealText(text));
    }
    
    IEnumerator RevealText(string originalString)
    {
        // Store the time of the latest call to this method
        var callTime = Time.time;
        _lastTime = callTime;
        
        startSkipping = false;

        var trimmedString = originalString.Trim();
        _textBox.text = trimmedString;
        _textBox.maxVisibleCharacters = 0;
        
        var numCharsRevealed = 0;
        
        UIDialogMessage.Shared.audioSource.Play();

        _textBox.ForceMeshUpdate();
        while (numCharsRevealed < _textBox.textInfo.characterCount)
        {
            startSkipping |= !IsGoFast;
            
            while (numCharsRevealed < originalString.Length && originalString[numCharsRevealed] == ' ')
                ++numCharsRevealed;
            
            ++numCharsRevealed;
            _textBox.maxVisibleCharacters = numCharsRevealed;

            // If this call's start time no longer matches the latest start time, drop it
            if (_lastTime - callTime > 0.001)
                yield break;

            var globalDirector = GlobalDirector.Shared;
            yield return new WaitForSeconds(startSkipping && IsGoFast ? globalDirector.typeWriterSpeedFast : globalDirector.typeWriterSpeedNormal);
        }
        
        UIDialogMessage.Shared.audioSource.Stop();
    } 
}