using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[UnitTitle("Password Prompt")]
[UnitCategory("NewGame/Puzzles")]
public sealed class PasswordPromptUnit : Unit, IBranchUnit
{
    [Inspectable, Serialize]
    public List<string> ProgressiveHints { get; set; } = new();

    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput enter { get; private set; }

    [DoNotSerialize]
    public ValueInput Title { get; private set; }

    [DoNotSerialize]
    public ValueInput ExpectedCode { get; private set; }

    [DoNotSerialize]
    public ControlOutput Accepted { get; private set; }

    [DoNotSerialize]
    public ControlOutput Cancelled { get; private set; }

    [DoNotSerialize]
    public ControlOutput InvalidConfiguration { get; private set; }

    protected override void Definition()
    {
        enter = ControlInputCoroutine(nameof(enter), RunCoroutine);
        Title = ValueInput(nameof(Title), "SECURE ACCESS");
        ExpectedCode = ValueInput(nameof(ExpectedCode), string.Empty);
        Accepted = ControlOutput(nameof(Accepted));
        Cancelled = ControlOutput(nameof(Cancelled));
        InvalidConfiguration = ControlOutput(nameof(InvalidConfiguration));

        Requirement(Title, enter);
        Requirement(ExpectedCode, enter);
        Succession(enter, Accepted);
        Succession(enter, Cancelled);
        Succession(enter, InvalidConfiguration);
    }

    private IEnumerator RunCoroutine(Flow flow)
    {
        var title = flow.GetValue<string>(Title);
        var expectedCode = flow.GetValue<string>(ExpectedCode);
        var finished = false;
        var result = PasswordPromptResult.Cancelled;

        PasswordPromptController.EnsureExists().Open(
            title,
            expectedCode,
            ProgressiveHints,
            promptResult =>
            {
                result = promptResult;
                finished = true;
            });

        yield return new UnityEngine.WaitUntil(() => finished);

        yield return result switch
        {
            PasswordPromptResult.Accepted => Accepted,
            PasswordPromptResult.InvalidConfiguration => InvalidConfiguration,
            _ => Cancelled
        };
    }
}
