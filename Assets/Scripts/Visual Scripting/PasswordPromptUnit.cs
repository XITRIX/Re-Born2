using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[UnitTitle("Password Prompt")]
[UnitCategory("NewGame/Puzzles")]
public sealed class PasswordPromptUnit : Unit, IBranchUnit
{
    // Kept serialized for graphs created before ProgressiveHintsText was added.
    // New graphs should author hints in the multiline input instead.
    [Serialize]
    public List<string> ProgressiveHints { get; set; } = new();

    [DoNotSerialize]
    [PortLabelHidden]
    public ControlInput enter { get; private set; }

    [DoNotSerialize]
    public ValueInput Title { get; private set; }

    [DoNotSerialize]
    public ValueInput ExpectedCode { get; private set; }

    [DoNotSerialize]
    public ValueInput ProgressiveHintsText { get; private set; }

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
        ProgressiveHintsText = ValueInput(nameof(ProgressiveHintsText), string.Empty);
        Accepted = ControlOutput(nameof(Accepted));
        Cancelled = ControlOutput(nameof(Cancelled));
        InvalidConfiguration = ControlOutput(nameof(InvalidConfiguration));

        Requirement(Title, enter);
        Requirement(ExpectedCode, enter);
        Requirement(ProgressiveHintsText, enter);
        Succession(enter, Accepted);
        Succession(enter, Cancelled);
        Succession(enter, InvalidConfiguration);
    }

    private IEnumerator RunCoroutine(Flow flow)
    {
        var title = flow.GetValue<string>(Title);
        var expectedCode = flow.GetValue<string>(ExpectedCode);
        var progressiveHintsText = flow.GetValue<string>(ProgressiveHintsText);
        var finished = false;
        var result = PasswordPromptResult.Cancelled;

        PasswordPromptController.EnsureExists().Open(
            title,
            expectedCode,
            ParseProgressiveHints(progressiveHintsText),
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

    private IReadOnlyList<string> ParseProgressiveHints(string hintsText)
    {
        if (string.IsNullOrWhiteSpace(hintsText))
            return ProgressiveHints;

        var hints = new List<string>();
        var lines = hintsText.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');

        foreach (var line in lines)
        {
            var hint = line.Trim();
            if (!string.IsNullOrEmpty(hint))
                hints.Add(hint);
        }

        return hints;
    }
}
