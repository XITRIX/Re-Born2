using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VSMessageUnit : Unit
{
    [DoNotSerialize]
    public ValueInput Avatar { get; private set; }
    
    [DoNotSerialize]
    public ValueInput Name { get; private set; }
    
    [DoNotSerialize]
    public ValueInput NameColor { get; private set; }
    
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
        Avatar = ValueInput<Sprite>("Avatar", null);
        Name = ValueInput<string>("Name", "");
        NameColor = ValueInput<Color>("NameColor", Color.white);
        Message = ValueInput<string>("Message", "");
    }
    
    private IEnumerator RunCoroutine(Flow flow)
    {
        // Reset dark overlay
        foreach (var sharedDialogCharacter in UIDialogMessage.Shared.DialogCharacters)
            sharedDialogCharacter.Value.BlackMask = 0;
        
        var avatar = flow.GetValue<Sprite>(Avatar);
        var name = flow.GetValue<string>(Name);
        var nameColor = flow.GetValue<Color>(NameColor);
        var message = flow.GetValue<string>(Message);

        GlobalDirector.ShowDialog();
        UIDialogMessage.OpenMessageView();
        yield return UIDialogMessage.SetMessage(avatar, name, nameColor, message);
        
        yield return new WaitUntil(() => UIDialogMessage.Shared.Submit);
        yield return new WaitForSeconds(0.1f);
        
        UIDialogMessage.CloseMessageView();
        GlobalDirector.CloseDialog();
        
        yield return Exit;
    }
}