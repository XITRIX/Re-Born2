using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VSMessageCharUnit : Unit
{
    [DoNotSerialize]
    public ValueInput Character { get; private set; }
    
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
    }
    
    private IEnumerator RunCoroutine(Flow flow)
    {
        var character = flow.GetValue<CharacterScriptableObject>(Character);
        var message = flow.GetValue<string>(Message);

        GlobalDirector.ShowDialog();
        UIDialogMessage.OpenMessageView();
        yield return UIDialogMessage.SetMessage(character.avatar, character.charName, message);
        
        yield return new WaitUntil(() => UIDialogMessage.Shared.Submit);
        yield return new WaitForSeconds(0.1f);
        
        UIDialogMessage.CloseMessageView();
        GlobalDirector.CloseDialog();
        
        yield return Exit;
    }
}