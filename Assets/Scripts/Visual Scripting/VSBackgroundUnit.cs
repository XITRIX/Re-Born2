using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VSBackgroundImageUnit : Unit
{
    [DoNotSerialize]
    public ValueInput Image { get; private set; }
    
    [DoNotSerialize]
    public ValueInput Seconds { get; private set; }
    
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
        Image = ValueInput<Sprite>("Image", null);
        Seconds = ValueInput<float>("Seconds", 0);
    }
    
    private IEnumerator RunCoroutine(Flow flow)
    {
        var image = flow.GetValue<Sprite>(Image);
        var seconds = flow.GetValue<float>(Seconds);

        yield return UIDialogMessage.SetBackstageImage(image, seconds);
        yield return Exit;
    }
}