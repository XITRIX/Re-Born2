using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VSMessageArrayUnit : Unit
{
    [DoNotSerialize]
    public ValueInput Avatar { get; private set; }
    
    [DoNotSerialize]
    public ValueInput Name { get; private set; }
    
    [DoNotSerialize]
    public ValueInput Messages { get; private set; }
    
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
        Name = ValueInput("Name", "");
        Messages = ValueInput("Messages", new List<string>());
    }
    
    private IEnumerator RunCoroutine(Flow flow)
    {
        var avatar = flow.GetValue<Sprite>(Avatar);
        var name = flow.GetValue<string>(Name);
        var messages = flow.GetValue<List<string>>(Messages);

        foreach (var message in messages)
        {
            GlobalDirector.ShowDialog();
            UIDialogMessage.OpenMessageView();
            yield return UIDialogMessage.SetMessage(avatar, name, message);
            
            yield return new WaitUntil(() => UIDialogMessage.Shared.Submit);
            yield return new WaitForSeconds(0.1f);
            
            UIDialogMessage.CloseMessageView();
            GlobalDirector.CloseDialog();
        }
        
        yield return Exit;
    }
}