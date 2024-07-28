using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// Start coroutine and wait until it is over.
[UnitTitle("Run and Wait For Coroutine")]
[UnitShortTitle("Coroutine")]
public class RunAndWaitForCoroutineUnit : WaitUnit
{
    /// The coroutine to start and wait for.
    [DoNotSerialize]
    [PortLabelHidden]
    public ValueInput CoroutineEnumerator { get; private set; }
 
    [DoNotSerialize]
    public ControlOutput Continue { get; private set; }

    protected override void Definition()
    {
        base.Definition();

        Continue = ControlOutput("Continue");
        CoroutineEnumerator = ValueInput<IEnumerator>(nameof(CoroutineEnumerator));
        Requirement(CoroutineEnumerator, enter);
    }

    protected override IEnumerator Await(Flow flow)
    {
        yield return Continue;
        var coroutineEnumeratorValue = flow.GetValue<IEnumerator>(this.CoroutineEnumerator);
        yield return GlobalDirector.Shared.StartCoroutine(coroutineEnumeratorValue);
        yield return exit;
    }
}