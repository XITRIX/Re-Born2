using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public sealed class PasswordPuzzleInteractable : Interactable
{
    [TextArea(4, 12)]
    public string promptTitle = "SECURE ACCESS";

    public string expectedCode = "0";

    [TextArea(2, 6)]
    public string[] progressiveHints = Array.Empty<string>();

    public string successGameKey;
    public string successEventName = "PasswordPuzzleAccepted";

    public string unlockedDocumentTitle = "PYCORP INTERNAL NETWORK";

    [TextArea(4, 12)]
    public string unlockedDocumentBody = "ACCESS GRANTED\n\nCorporate archive unlocked.";

    private bool _promptIsOpen;

    [CanBeNull]
    public override Action InteractionScenario => Interact;

    private void Interact()
    {
        if (!string.IsNullOrEmpty(successGameKey) && GlobalDirector.GetGameKey(successGameKey))
        {
            PuzzleDocumentViewer.EnsureExists().Open(unlockedDocumentTitle, unlockedDocumentBody);
            return;
        }

        if (_promptIsOpen)
            return;

        _promptIsOpen = true;
        PasswordPromptController.EnsureExists().Open(
            promptTitle,
            expectedCode,
            progressiveHints,
            HandlePromptClosed);
    }

    private void HandlePromptClosed(PasswordPromptResult result)
    {
        _promptIsOpen = false;
        if (result != PasswordPromptResult.Accepted)
            return;

        if (!string.IsNullOrEmpty(successGameKey))
        {
            if (GlobalDirector.GetGameKey(successGameKey))
                return;

            GlobalDirector.SetGameKey(successGameKey, true);
        }

        if (!string.IsNullOrEmpty(successEventName))
            EventBus.Trigger(successEventName, objectId);

        if (!string.IsNullOrWhiteSpace(unlockedDocumentBody))
            PuzzleDocumentViewer.EnsureExists().Open(unlockedDocumentTitle, unlockedDocumentBody);
    }
}
