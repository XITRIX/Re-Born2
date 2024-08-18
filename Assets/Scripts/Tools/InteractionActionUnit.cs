using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionActionUnit : EventUnit<string>
{
    // [DoNotSerialize]// No need to serialize ports.
    // public ValueOutput result { get; private set; }// The event output data to return when the event is triggered.
    
    
    [DoNotSerialize]
    public ValueInput Id { get; private set; }
    
    protected override bool register => true;

    // Adding an EventHook with the name of the event to the list of visual scripting events.
    public override EventHook GetHook(GraphReference reference)
    {
        return new EventHook("InteractionEvent");
    }
    
    protected override void Definition()
    {
        base.Definition();
        // Setting the value on our port.
        Id = ValueInput("Id", "");
    }

    protected override bool ShouldTrigger(Flow flow, string args)
    {
        // return base.ShouldTrigger(flow, args);
        var id = flow.GetValue<string>(Id);
        return id == args;
    }
}

public class InteractionTriggerEnterUnit : EventUnit<(string, CharacterScript)>
{
    [DoNotSerialize]
    public ValueInput Id { get; private set; }
    
    [DoNotSerialize]
    public ValueInput ActiveCharOnly { get; private set; }
    
    protected override bool register => true;

    // Adding an EventHook with the name of the event to the list of visual scripting events.
    public override EventHook GetHook(GraphReference reference)
    {
        return new EventHook("InteractionTriggerEnterUnit");
    }
    protected override void Definition()
    {
        base.Definition();
        // Setting the value on our port.
        Id = ValueInput("Id", "");
        ActiveCharOnly = ValueInput("ActiveCharOnly", true);
    }

    protected override bool ShouldTrigger(Flow flow, (string, CharacterScript) args)
    {
        // return base.ShouldTrigger(flow, args);
        var id = flow.GetValue<string>(Id);
        var activeCharOnly = flow.GetValue<bool>(ActiveCharOnly);

        if (activeCharOnly && PlayerInputScript.Shared.ActiveCharacter != args.Item2)
        {
            return false;
        }
        
        return id == args.Item1;
    }
}

public class InteractionTriggerExitUnit : EventUnit<(string, CharacterScript)>
{
    [DoNotSerialize]
    public ValueInput Id { get; private set; }
    
    [DoNotSerialize]
    public ValueInput ActiveCharOnly { get; private set; }
    
    protected override bool register => true;

    // Adding an EventHook with the name of the event to the list of visual scripting events.
    public override EventHook GetHook(GraphReference reference)
    {
        return new EventHook("InteractionTriggerExitUnit");
    }
    protected override void Definition()
    {
        base.Definition();
        // Setting the value on our port.
        Id = ValueInput("Id", "");
        ActiveCharOnly = ValueInput("ActiveCharOnly", true);
    }

    protected override bool ShouldTrigger(Flow flow, (string, CharacterScript) args)
    {
        // return base.ShouldTrigger(flow, args);
        var id = flow.GetValue<string>(Id);
        var activeCharOnly = flow.GetValue<bool>(ActiveCharOnly);

        if (activeCharOnly && PlayerInputScript.Shared.ActiveCharacter != args.Item2)
        {
            return false;
        }
        
        return id == args.Item1;
    }
}