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
        Message = ValueInput<string>("Message", "");
    }
    
    private IEnumerator RunCoroutine(Flow flow)
    {
        var avatar = flow.GetValue<Sprite>(Avatar);
        var name = flow.GetValue<string>(Name);
        var message = flow.GetValue<string>(Message);

        GlobalDirector.ShowDialog();
        UIDialogMessage.OpenMessageView();
        yield return UIDialogMessage.SetMessage(avatar, name, message);
        
        yield return new WaitUntil(() => UIDialogMessage.Shared.Submit);
        yield return new WaitForSeconds(0.1f);
        
        UIDialogMessage.CloseMessageView();
        GlobalDirector.CloseDialog();
        
        yield return Exit;
    }
}