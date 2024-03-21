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
    
    private bool IsSkipping => Math.Abs(_controlMap.UI.Submit.ReadValue<float>() - 1) < 0.1f;

    private void Awake()
    {
        _textBox = GetComponent<TextMeshProUGUI>();
        _controlMap = new PlayerControlMap();
        _controlMap.UI.Submit.Enable();
    }

    public IEnumerator SetText(string text)
    {
        yield return StartCoroutine(RevealText(text));
    }
    
    IEnumerator RevealText(string originalString)
    {
        startSkipping = false;

        var trimmedString = originalString.Trim();
        _textBox.text = trimmedString;
        _textBox.maxVisibleCharacters = 0;
        
        var numCharsRevealed = 0;
        
        // TODO: Uncomment for dialog audio
        // VNCanvasController.Shared.audioSource.Play();

        _textBox.ForceMeshUpdate();
        while (numCharsRevealed < _textBox.textInfo.characterCount)
        {
            startSkipping |= !IsSkipping;
            
            while (numCharsRevealed < originalString.Length && originalString[numCharsRevealed] == ' ')
                ++numCharsRevealed;
            
            ++numCharsRevealed;
            _textBox.maxVisibleCharacters = numCharsRevealed;

            yield return new WaitForSeconds(startSkipping && IsSkipping ? 0.01f : 0.07f);
        }
        
        // TODO: Uncomment for dialog audio
        // VNCanvasController.Shared.audioSource.Stop();
    } 
}