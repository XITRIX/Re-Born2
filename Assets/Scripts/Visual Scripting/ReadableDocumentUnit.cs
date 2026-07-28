using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Readable Document")]
[UnitCategory("NewGame/Puzzles")]
public sealed class ReadableDocumentUnit : Unit
{
    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput Enter { get; private set; }

    [DoNotSerialize]
    public ValueInput Title { get; private set; }

    [DoNotSerialize]
    public ValueInput Body { get; private set; }

    [DoNotSerialize]
    public ValueInput DiscoveredGameKey { get; private set; }

    [DoNotSerialize]
    [PortLabelHidden]
    public ControlOutput Closed { get; private set; }

    protected override void Definition()
    {
        Enter = ControlInputCoroutine(nameof(Enter), ShowDocument);
        Title = ValueInput(nameof(Title), "DOCUMENT");
        Body = ValueInput(nameof(Body), string.Empty);
        DiscoveredGameKey = ValueInput(nameof(DiscoveredGameKey), string.Empty);
        Closed = ControlOutput(nameof(Closed));

        Requirement(Title, Enter);
        Requirement(Body, Enter);
        Requirement(DiscoveredGameKey, Enter);
        Succession(Enter, Closed);
    }

    private IEnumerator ShowDocument(Flow flow)
    {
        var title = flow.GetValue<string>(Title);
        var body = flow.GetValue<string>(Body);
        var discoveredGameKey = flow.GetValue<string>(DiscoveredGameKey);
        var viewer = PuzzleDocumentViewer.EnsureExists();

        // Do not replace a document that is already being read.
        if (viewer.IsOpen)
            yield return new WaitUntil(() => !viewer.IsOpen);

        if (!string.IsNullOrWhiteSpace(discoveredGameKey))
            GlobalDirector.SetGameKey(discoveredGameKey, true);

        viewer.Open(title, body);
        yield return new WaitUntil(() => !viewer.IsOpen);
        yield return Closed;
    }
}
