using System;
using JetBrains.Annotations;
using UnityEngine;

public sealed class PuzzleDocumentInteractable : Interactable
{
    public string documentTitle;

    [TextArea(8, 30)]
    public string documentBody;

    public string discoveredGameKey;

    [CanBeNull]
    public override Action InteractionScenario => ShowDocument;

    private void ShowDocument()
    {
        if (!string.IsNullOrEmpty(discoveredGameKey))
            GlobalDirector.SetGameKey(discoveredGameKey, true);

        PuzzleDocumentViewer.EnsureExists().Open(documentTitle, documentBody);
    }
}
