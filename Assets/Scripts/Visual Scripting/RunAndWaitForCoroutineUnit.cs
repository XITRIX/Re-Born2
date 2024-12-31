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
    
    
    /// The coroutine to start and wait for.
    [DoNotSerialize]
    [PortLabelHidden]
    public ValueOutput Coroutine { get; private set; }
    
    private Coroutine _coroutine;

    protected override void Definition()
    {
        base.Definition();

        CoroutineEnumerator = ValueInput<IEnumerator>(nameof(CoroutineEnumerator));
        Coroutine = ValueOutput(nameof(Coroutine), _ => _coroutine);
        Requirement(CoroutineEnumerator, enter);
    }

    protected override IEnumerator Await(Flow flow)
    {
        var coroutineEnumeratorValue = flow.GetValue<IEnumerator>(this.CoroutineEnumerator);
        _coroutine = GlobalDirector.Shared.StartCoroutine(coroutineEnumeratorValue);
        yield return _coroutine;
        yield return exit;
    }
}