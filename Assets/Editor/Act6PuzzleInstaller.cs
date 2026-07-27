using UnityEditor;
using UnityEngine;

public static class Act6PuzzleInstaller
{
    private const string Act6PrefabPath = "Assets/Assets/Prefabs/Maps/Act6/Act6Map.prefab";
    private const string PuzzleRootName = "PC Puzzle - Four Approvals";
    private static readonly string[] PreviewHints =
    {
        "The terminal says R6. That archived R5 sheet is obsolete.",
        "Only approved entries belong in the chain. The rejected names are noise.",
        "Oldest approved to newest, then take the final badge digit."
    };

    [MenuItem("Tools/Act 6/Install Four Approvals Puzzle")]
    public static void Install()
    {
        var prefabRoot = PrefabUtility.LoadPrefabContents(Act6PrefabPath);
        try
        {
            var existingRoot = prefabRoot.transform.Find(PuzzleRootName);
            if (existingRoot != null)
                Object.DestroyImmediate(existingRoot.gameObject);

            var puzzleRoot = new GameObject(PuzzleRootName);
            puzzleRoot.transform.SetParent(prefabRoot.transform, false);

            CreatePasswordTerminal(puzzleRoot.transform);
            CreateR6SignOff(puzzleRoot.transform);
            CreateR5Archive(puzzleRoot.transform);
            CreateStaffDirectory(puzzleRoot.transform);
            CreateITNote(puzzleRoot.transform);

            PrefabUtility.SaveAsPrefabAsset(prefabRoot, Act6PrefabPath);
            AssetDatabase.SaveAssets();
            Debug.Log("Installed Act 6 Four Approvals puzzle.");
        }
        finally
        {
            PrefabUtility.UnloadPrefabContents(prefabRoot);
        }
    }

    [MenuItem("Tools/Act 6/Install Four Approvals Puzzle", true)]
    private static bool ValidateInstall()
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>(Act6PrefabPath) != null;
    }

    [MenuItem("Tools/Act 6/Preview Password Prompt/Act 6 Code")]
    private static void PreviewAct6PasswordPrompt()
    {
        OpenPasswordPreview("8365");
    }

    [MenuItem("Tools/Act 6/Preview Password Prompt/Single Digit")]
    private static void PreviewSingleDigitPasswordPrompt()
    {
        OpenPasswordPreview("7");
    }

    [MenuItem("Tools/Act 6/Preview Password Prompt/Leading Zero")]
    private static void PreviewLeadingZeroPasswordPrompt()
    {
        OpenPasswordPreview("0609");
    }

    [MenuItem("Tools/Act 6/Preview Password Prompt/Long 12-Digit Code")]
    private static void PreviewLongPasswordPrompt()
    {
        OpenPasswordPreview("060912345678");
    }

    [MenuItem("Tools/Act 6/Preview Password Prompt/Act 6 Code", true)]
    [MenuItem("Tools/Act 6/Preview Password Prompt/Single Digit", true)]
    [MenuItem("Tools/Act 6/Preview Password Prompt/Leading Zero", true)]
    [MenuItem("Tools/Act 6/Preview Password Prompt/Long 12-Digit Code", true)]
    private static bool ValidatePasswordPreview()
    {
        return Application.isPlaying;
    }

    private static void OpenPasswordPreview(string expectedCode)
    {
        PasswordPromptController.EnsureExists().Open(
            $"PYCORP PASSWORD UI PREVIEW\nExpected: {expectedCode}",
            expectedCode,
            PreviewHints,
            result => Debug.Log($"Password UI preview result: {result}"));
    }

    private static void CreatePasswordTerminal(Transform parent)
    {
        var terminal = CreateHotspot<PasswordPuzzleInteractable>(
            parent,
            "PC Terminal",
            "act6_pc",
            new Vector3(-4.2f, 25.25f, -0.1f),
            new Vector3(2.2f, 2.1f, 4f));

        terminal.promptTitle =
            "PYCORP SECURE BOOT\n" +
            "Credentials expired.\n" +
            "Recovery challenge: HELIOS R6 — Final Approval Chain\n" +
            "Enter badge tails, oldest first.";
        terminal.expectedCode = "8365";
        terminal.progressiveHints = new[]
        {
            "The terminal says R6. That archived R5 sheet is obsolete.",
            "Only approved entries belong in the chain. The rejected names are noise.",
            "Oldest approved to newest, then take the final badge digit."
        };
        terminal.successGameKey = "act6_pc_unlocked";
        terminal.successEventName = "PasswordPuzzleAccepted";
        terminal.unlockedDocumentTitle = "PYCORP INTERNAL NETWORK";
        terminal.unlockedDocumentBody =
            "<align=center><b>ACCESS GRANTED</b></align>\n\n" +
            "Corporate archive unlocked.\n\n" +
            "<i>The contents of this workstation continue in the Act 6 story graph.</i>";
    }

    private static void CreateR6SignOff(Transform parent)
    {
        var document = CreateHotspot<PuzzleDocumentInteractable>(
            parent,
            "HELIOS R6 Sign-Off Sheet",
            "act6_helios_r6_signoff",
            new Vector3(-1.25f, 25.35f, -0.2f),
            new Vector3(1.35f, 1.35f, 4f));

        document.documentTitle = "PROJECT HELIOS — RELEASE 6";
        document.documentBody =
            "<align=center><b>FINAL SIGN-OFF AUDIT</b></align>\n\n" +
            "<b>07:55   A. Mercer</b>\n" +
            "<color=#A32020>REJECTED — expired clearance</color>\n\n" +
            "<b>08:40   R. Kessler</b>\n" +
            "<color=#146B3A>APPROVED</color>\n\n" +
            "<b>09:30   J. Shaw</b>\n" +
            "<color=#A32020>REJECTED — checksum mismatch</color>\n\n" +
            "<b>10:15   M. Vale</b>\n" +
            "<color=#146B3A>APPROVED</color>\n\n" +
            "<b>12:10   P. Novak</b>\n" +
            "<color=#A32020>REJECTED — missing witness</color>\n\n" +
            "<b>14:05   L. Orlov</b>\n" +
            "<color=#146B3A>APPROVED</color>\n\n" +
            "<b>16:45   D. Crane</b>\n" +
            "<color=#A32020>REJECTED — revoked by Security</color>\n\n" +
            "<b>18:30   E. Hale</b>\n" +
            "<color=#146B3A>APPROVED</color>";
        document.discoveredGameKey = "act6_clue_helios_r6";

        AddPaperVisual(
            document.transform,
            new Vector3(0f, 2.85f, -0.22f),
            new Vector2(0.9f, 0.62f),
            new Color(0.88f, 0.91f, 0.86f, 1f));
    }

    private static void CreateR5Archive(Transform parent)
    {
        var document = CreateHotspot<PuzzleDocumentInteractable>(
            parent,
            "HELIOS R5 Archived Sheet",
            "act6_helios_r5_archive",
            new Vector3(1.25f, 25.35f, -0.2f),
            new Vector3(1.35f, 1.35f, 4f));

        document.documentTitle = "HELIOS R5 — ARCHIVED";
        document.documentBody =
            "<align=center><b>SUPERSEDED — DO NOT USE</b></align>\n\n" +
            "<s>08:00   D. Crane     APPROVED</s>\n\n" +
            "<s>11:20   P. Novak     APPROVED</s>\n\n" +
            "<s>15:40   J. Shaw      APPROVED</s>\n\n" +
            "<s>17:10   A. Mercer    APPROVED</s>\n\n" +
            "<align=center><color=#A32020><b>ARCHIVED / SUPERSEDED BY R6</b></color></align>";
        document.discoveredGameKey = "act6_clue_helios_r5";

        AddPaperVisual(
            document.transform,
            new Vector3(0f, 2.85f, -0.22f),
            new Vector2(0.9f, 0.62f),
            new Color(0.55f, 0.56f, 0.54f, 1f));
    }

    private static void CreateStaffDirectory(Transform parent)
    {
        var document = CreateHotspot<PuzzleDocumentInteractable>(
            parent,
            "PyCorp Staff Directory",
            "act6_staff_directory",
            new Vector3(5.9f, 25.3f, -0.2f),
            new Vector3(1.75f, 2.3f, 4f));

        document.documentTitle = "PYCORP STAFF DIRECTORY";
        document.documentBody =
            "<align=center><i>Alphabetical by surname</i></align>\n\n" +
            "<b>D. Crane</b>       Q-2301\n\n" +
            "<b>E. Hale</b>        X-9915\n\n" +
            "<b>R. Kessler</b>     C-1048\n\n" +
            "<b>A. Mercer</b>      A-6427\n\n" +
            "<b>P. Novak</b>       R-8814\n\n" +
            "<b>L. Orlov</b>       S-3206\n\n" +
            "<b>J. Shaw</b>        T-5509\n\n" +
            "<b>M. Vale</b>        B-7713";
        document.discoveredGameKey = "act6_clue_staff_directory";

        AddPaperVisual(
            document.transform,
            new Vector3(-1.25f, 3.05f, -0.22f),
            new Vector2(0.78f, 1.08f),
            new Color(0.75f, 0.86f, 0.9f, 1f));
    }

    private static void CreateITNote(Transform parent)
    {
        var document = CreateHotspot<PuzzleDocumentInteractable>(
            parent,
            "IT Badge Tail Note",
            "act6_badge_tail_note",
            new Vector3(-5.75f, 25.4f, -0.2f),
            new Vector3(0.8f, 2f, 4f));

        document.documentTitle = "IT RECOVERY REMINDER";
        document.documentBody =
            "<align=center><b>“BADGE TAIL” = FINAL NUMBER ONLY</b></align>\n\n" +
            "Use the audit order, not directory order.\n\n" +
            "— IT Security";
        document.discoveredGameKey = "act6_clue_badge_tail_note";

        AddPaperVisual(
            document.transform,
            new Vector3(1.6f, 2.95f, -0.22f),
            new Vector2(0.64f, 0.58f),
            new Color(1f, 0.84f, 0.22f, 1f));
    }

    private static T CreateHotspot<T>(
        Transform parent,
        string objectName,
        string objectId,
        Vector3 localPosition,
        Vector3 triggerSize)
        where T : Interactable
    {
        var hotspot = new GameObject(objectName);
        hotspot.transform.SetParent(parent, false);
        hotspot.transform.localPosition = localPosition;
        hotspot.transform.localRotation = Quaternion.identity;
        hotspot.transform.localScale = Vector3.one;

        var interactable = hotspot.AddComponent<T>();
        interactable.objectId = objectId;

        var trigger = hotspot.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size = triggerSize;
        return interactable;
    }

    private static void AddPaperVisual(
        Transform parent,
        Vector3 localPosition,
        Vector2 paperSize,
        Color paperColor)
    {
        var visual = new GameObject("Paper Visual");
        visual.transform.SetParent(parent, false);
        visual.transform.localPosition = localPosition;
        visual.transform.localRotation = Quaternion.identity;

        var renderer = visual.AddComponent<SpriteRenderer>();
        renderer.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        renderer.drawMode = SpriteDrawMode.Simple;
        renderer.color = paperColor;
        renderer.sortingOrder = 12;

        var spriteSize = renderer.sprite.bounds.size;
        visual.transform.localScale = new Vector3(
            paperSize.x / spriteSize.x,
            paperSize.y / spriteSize.y,
            1f);
    }
}
