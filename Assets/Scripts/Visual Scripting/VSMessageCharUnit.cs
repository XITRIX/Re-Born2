using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class VSMessageCharUnit : Unit
{
    [DoNotSerialize]
    public ValueInput Character { get; private set; }
    
    [DoNotSerialize]
    public ValueInput HideFace { get; private set; }
    
    [DoNotSerialize]
    public ValueInput HideName { get; private set; }
    
    [DoNotSerialize]
    public ValueInput Message { get; private set; }
    
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput Enter { get; private set; }
 
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlOutput Exit { get; private set; }
    
    protected override void Definition()
    {
        Enter = ControlInputCoroutine("Enter", RunCoroutine);
        Exit = ControlOutput("Exit");
        Character = ValueInput<CharacterScriptableObject>("Name", null);
        Message = ValueInput("Message", "");
        HideFace = ValueInput("HideFace", false);
        HideName = ValueInput("HideName", false);
    }
    
    private IEnumerator RunCoroutine(Flow flow)
    {
        var character = flow.GetValue<CharacterScriptableObject>(Character);
        var message = flow.GetValue<string>(Message);
        var hideFace = flow.GetValue<bool>(HideFace);
        var hideName = flow.GetValue<bool>(HideName);

        GlobalDirector.ShowDialog();
        UIDialogMessage.OpenMessageView();

        UIDialogMessage.Shared.DialogCharacters.TryGetValue(character, out var dialogChar);

        if (dialogChar == null)
            yield return UIDialogMessage.SetMessage(hideFace ? null : character.avatar, hideName ? null : character.charName, character.nameColor, message);
        else
        {
            // Set dark overlay is not speak
            foreach (var sharedDialogCharacter in UIDialogMessage.Shared.DialogCharacters)
            {
                if (character != sharedDialogCharacter.Key)
                    sharedDialogCharacter.Value.BlackMask = 0.4f;
                else 
                    sharedDialogCharacter.Value.BlackMask = 0;
            }
            
            yield return UIDialogMessage.SetMessage(null, hideName ? null : character.charName, character.nameColor, message);
        }

        yield return new WaitUntil(() => UIDialogMessage.Shared.DialogNext);
        yield return new WaitForSeconds(0.1f);
        
        UIDialogMessage.CloseMessageView();
        GlobalDirector.CloseDialog();
        
        // Reset dark overlay
        foreach (var sharedDialogCharacter in UIDialogMessage.Shared.DialogCharacters)
            sharedDialogCharacter.Value.BlackMask = 0;
        
        yield return Exit;
    }
}