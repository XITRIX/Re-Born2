using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum PasswordPromptResult
{
    Accepted,
    Cancelled,
    InvalidConfiguration
}

public sealed class PasswordPromptController : MonoBehaviour
{
    private sealed class DigitSlot
    {
        public RectTransform root;
        public Button selectButton;
        public TextMeshProUGUI digitLabel;
        public Image background;
    }

    public static PasswordPromptController Shared { get; private set; }

    private static readonly Color PanelColor = new(0.025f, 0.055f, 0.08f, 0.985f);
    private static readonly Color SlotColor = new(0.055f, 0.12f, 0.16f, 1f);
    private static readonly Color SelectedSlotColor = new(0.04f, 0.52f, 0.72f, 1f);
    private static readonly Color AccentColor = new(0.15f, 0.78f, 0.96f, 1f);
    private static readonly Color ErrorColor = new(1f, 0.28f, 0.25f, 1f);

    private readonly List<DigitSlot> _slots = new();
    private readonly List<int> _digits = new();
    private readonly List<string> _progressiveHints = new();

    private Canvas _canvas;
    private GameObject _root;
    private RectTransform _content;
    private ScrollRect _scrollRect;
    private TextMeshProUGUI _titleLabel;
    private TextMeshProUGUI _statusLabel;
    private TextMeshProUGUI _hintLabel;
    private Button _unlockButton;
    private Button _cancelButton;
    private PlayerControlMap _controlMap;
    private GameObject _previousSelectedObject;
    private Action<PasswordPromptResult> _onClosed;
    private string _expectedCode;
    private int _selectedIndex;
    private int _failedAttempts;
    private float _nextNavigationRepeatTime;
    private float _acceptInputAfter;
    private Vector2 _previousNavigation;
    private bool _isOpen;
    private bool _restorePlayerInput;
    private bool _scrollSelectionAtEndOfFrame;

    public bool IsOpen => _isOpen;
    public PasswordPromptResult LastResult { get; private set; } = PasswordPromptResult.Cancelled;

    public static PasswordPromptController EnsureExists()
    {
        if (Shared != null)
            return Shared;

        var controllerObject = new GameObject("[Password Prompt]");
        DontDestroyOnLoad(controllerObject);
        return controllerObject.AddComponent<PasswordPromptController>();
    }

    private void Awake()
    {
        if (Shared != null && Shared != this)
        {
            Destroy(gameObject);
            return;
        }

        Shared = this;
        DontDestroyOnLoad(gameObject);

        _controlMap = new PlayerControlMap();
        BuildInterface();
        _root.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Shared == this)
            Shared = null;

        _controlMap?.Dispose();
    }

    public void Open(
        string title,
        string expectedCode,
        IReadOnlyList<string> progressiveHints,
        Action<PasswordPromptResult> onClosed)
    {
        if (_isOpen)
        {
            Debug.LogWarning("A password prompt is already open.");
            onClosed?.Invoke(PasswordPromptResult.Cancelled);
            return;
        }

        if (!IsValidExpectedCode(expectedCode))
        {
            Debug.LogError("Password prompt expected code must contain at least one numeric digit.");
            LastResult = PasswordPromptResult.InvalidConfiguration;
            onClosed?.Invoke(LastResult);
            return;
        }

        _expectedCode = expectedCode;
        _onClosed = onClosed;
        _progressiveHints.Clear();
        if (progressiveHints != null)
            _progressiveHints.AddRange(progressiveHints.Where(hint => !string.IsNullOrWhiteSpace(hint)));

        _failedAttempts = 0;
        _selectedIndex = 0;
        _previousNavigation = Vector2.zero;
        _nextNavigationRepeatTime = 0f;
        _acceptInputAfter = Time.unscaledTime + 0.12f;
        LastResult = PasswordPromptResult.Cancelled;

        RebuildDigitSlots(expectedCode.Length);
        _titleLabel.text = string.IsNullOrWhiteSpace(title) ? "SECURE ACCESS" : title;
        _statusLabel.text = "Enter access code";
        _statusLabel.color = Color.white;
        _hintLabel.text = string.Empty;

        _previousSelectedObject = EventSystem.current != null
            ? EventSystem.current.currentSelectedGameObject
            : null;

        _restorePlayerInput = PlayerInputScript.Shared != null &&
                              PlayerInputScript.Shared.IsPlayerInputEnabled;
        PlayerInputScript.Shared?.DisablePlayerInput();

        _root.SetActive(true);
        _isOpen = true;
        EnableUIActions();
        SelectDigit(0);
    }

    public static bool IsValidExpectedCode(string expectedCode)
    {
        return !string.IsNullOrEmpty(expectedCode) &&
               expectedCode.All(digit => digit is >= '0' and <= '9');
    }

    private void Update()
    {
        if (!_isOpen)
            return;

        HandleNavigation();
        HandleKeyboardDigits();

        if (Time.unscaledTime < _acceptInputAfter)
            return;

        if (_controlMap.UI.Submit.WasPressedThisFrame())
            ValidateCurrentCode();

        if (_controlMap.UI.Cancel.WasPressedThisFrame())
            Close(PasswordPromptResult.Cancelled);
    }

    private void LateUpdate()
    {
        if (!_isOpen || !_scrollSelectionAtEndOfFrame)
            return;

        _scrollSelectionAtEndOfFrame = false;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        Canvas.ForceUpdateCanvases();
        ScrollSelectedDigitIntoView();
    }

    private void HandleNavigation()
    {
        var navigation = _controlMap.UI.Navigate.ReadValue<Vector2>();
        var horizontal = Mathf.Abs(navigation.x) >= 0.55f ? Mathf.Sign(navigation.x) : 0f;
        var vertical = Mathf.Abs(navigation.y) >= 0.55f ? Mathf.Sign(navigation.y) : 0f;
        var normalized = new Vector2(horizontal, vertical);

        if (normalized == Vector2.zero)
        {
            _previousNavigation = Vector2.zero;
            return;
        }

        var isInitialPress = _previousNavigation == Vector2.zero || normalized != _previousNavigation;
        if (!isInitialPress && Time.unscaledTime < _nextNavigationRepeatTime)
            return;

        if (Mathf.Abs(normalized.x) >= Mathf.Abs(normalized.y))
            SelectDigit((_selectedIndex + (normalized.x > 0 ? 1 : -1) + _digits.Count) % _digits.Count);
        else
            ChangeDigit(_selectedIndex, normalized.y > 0 ? 1 : -1);

        _previousNavigation = normalized;
        _nextNavigationRepeatTime = Time.unscaledTime + (isInitialPress ? 0.38f : 0.12f);
    }

    private void HandleKeyboardDigits()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        for (var digit = 0; digit <= 9; digit++)
        {
            var mainKey = digit switch
            {
                0 => keyboard.digit0Key,
                1 => keyboard.digit1Key,
                2 => keyboard.digit2Key,
                3 => keyboard.digit3Key,
                4 => keyboard.digit4Key,
                5 => keyboard.digit5Key,
                6 => keyboard.digit6Key,
                7 => keyboard.digit7Key,
                8 => keyboard.digit8Key,
                9 => keyboard.digit9Key,
                _ => null
            };

            var numpadKey = digit switch
            {
                0 => keyboard.numpad0Key,
                1 => keyboard.numpad1Key,
                2 => keyboard.numpad2Key,
                3 => keyboard.numpad3Key,
                4 => keyboard.numpad4Key,
                5 => keyboard.numpad5Key,
                6 => keyboard.numpad6Key,
                7 => keyboard.numpad7Key,
                8 => keyboard.numpad8Key,
                9 => keyboard.numpad9Key,
                _ => null
            };

            if ((mainKey?.wasPressedThisFrame ?? false) || (numpadKey?.wasPressedThisFrame ?? false))
            {
                SetDigit(_selectedIndex, digit);
                SelectDigit((_selectedIndex + 1) % _digits.Count);
                break;
            }
        }
    }

    private void ValidateCurrentCode()
    {
        var currentCode = string.Concat(_digits);
        if (string.Equals(currentCode, _expectedCode, StringComparison.Ordinal))
        {
            _statusLabel.text = "ACCESS GRANTED";
            _statusLabel.color = AccentColor;
            Close(PasswordPromptResult.Accepted);
            return;
        }

        _failedAttempts++;
        _statusLabel.text = "ACCESS DENIED";
        _statusLabel.color = ErrorColor;

        var hintIndex = _failedAttempts / 2 - 1;
        if (hintIndex >= 0 && hintIndex < _progressiveHints.Count)
            _hintLabel.text = _progressiveHints[hintIndex];
    }

    private void Close(PasswordPromptResult result)
    {
        if (!_isOpen)
            return;

        LastResult = result;
        _isOpen = false;
        DisableUIActions();
        _root.SetActive(false);

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (_previousSelectedObject != null && _previousSelectedObject.activeInHierarchy)
                EventSystem.current.SetSelectedGameObject(_previousSelectedObject);
        }

        if (_restorePlayerInput)
            PlayerInputScript.Shared?.EnablePlayerInput();

        var callback = _onClosed;
        _onClosed = null;
        callback?.Invoke(result);
    }

    private void EnableUIActions()
    {
        _controlMap.UI.Navigate.Enable();
        _controlMap.UI.Submit.Enable();
        _controlMap.UI.Cancel.Enable();
    }

    private void DisableUIActions()
    {
        _controlMap.UI.Navigate.Disable();
        _controlMap.UI.Submit.Disable();
        _controlMap.UI.Cancel.Disable();
    }

    private void RebuildDigitSlots(int count)
    {
        foreach (Transform child in _content)
            Destroy(child.gameObject);

        _slots.Clear();
        _digits.Clear();

        for (var index = 0; index < count; index++)
        {
            var capturedIndex = index;
            _digits.Add(0);

            var slotObject = CreateUIObject($"Digit {index + 1}", _content);
            var slotRect = slotObject.GetComponent<RectTransform>();
            slotRect.sizeDelta = new Vector2(92f, 164f);
            var layoutElement = slotObject.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 92f;
            layoutElement.preferredHeight = 164f;
            layoutElement.minWidth = 92f;

            var upButton = CreateButton("+", slotRect, new Vector2(0f, 58f), new Vector2(76f, 38f));
            upButton.onClick.AddListener(() =>
            {
                SelectDigit(capturedIndex);
                ChangeDigit(capturedIndex, 1);
            });

            var digitButton = CreateButton("0", slotRect, Vector2.zero, new Vector2(76f, 72f), 46f);
            digitButton.onClick.AddListener(() => SelectDigit(capturedIndex));

            var downButton = CreateButton("-", slotRect, new Vector2(0f, -58f), new Vector2(76f, 38f));
            downButton.onClick.AddListener(() =>
            {
                SelectDigit(capturedIndex);
                ChangeDigit(capturedIndex, -1);
            });

            _slots.Add(new DigitSlot
            {
                root = slotRect,
                selectButton = digitButton,
                digitLabel = digitButton.GetComponentInChildren<TextMeshProUGUI>(),
                background = digitButton.GetComponent<Image>()
            });
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
        _scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void SelectDigit(int index)
    {
        if (_slots.Count == 0)
            return;

        _selectedIndex = Mathf.Clamp(index, 0, _slots.Count - 1);

        for (var slotIndex = 0; slotIndex < _slots.Count; slotIndex++)
            _slots[slotIndex].background.color = slotIndex == _selectedIndex ? SelectedSlotColor : SlotColor;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_slots[_selectedIndex].selectButton.gameObject);
        }

        _scrollSelectionAtEndOfFrame = true;
    }

    private void ChangeDigit(int index, int delta)
    {
        SetDigit(index, (_digits[index] + delta + 10) % 10);
    }

    private void SetDigit(int index, int value)
    {
        _digits[index] = Mathf.Clamp(value, 0, 9);
        _slots[index].digitLabel.text = _digits[index].ToString();
        _statusLabel.text = "Enter access code";
        _statusLabel.color = Color.white;
    }

    private void ScrollSelectedDigitIntoView()
    {
        if (_slots.Count <= 1)
        {
            _scrollRect.horizontalNormalizedPosition = 0f;
            return;
        }

        Canvas.ForceUpdateCanvases();
        var contentWidth = _content.rect.width;
        var viewportWidth = ((RectTransform)_scrollRect.viewport).rect.width;
        if (contentWidth <= viewportWidth)
        {
            _scrollRect.horizontalNormalizedPosition = 0.5f;
            return;
        }

        var selectedCenter = _slots[_selectedIndex].root.anchoredPosition.x + _slots[_selectedIndex].root.rect.width * 0.5f;
        var target = Mathf.Clamp01((selectedCenter - viewportWidth * 0.5f) / (contentWidth - viewportWidth));
        _scrollRect.horizontalNormalizedPosition = target;
    }

    private void BuildInterface()
    {
        _canvas = gameObject.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.sortingOrder = 5000;

        var scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();

        _root = CreateUIObject("Password Prompt Root", transform);
        StretchToParent(_root.GetComponent<RectTransform>());
        var blocker = _root.AddComponent<Image>();
        blocker.color = new Color(0f, 0f, 0f, 0.82f);

        var panel = CreateUIObject("Panel", _root.transform);
        var panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.sizeDelta = new Vector2(1120f, 720f);
        panelRect.anchoredPosition = Vector2.zero;
        var panelImage = panel.AddComponent<Image>();
        panelImage.color = PanelColor;
        var outline = panel.AddComponent<Outline>();
        outline.effectColor = new Color(0.08f, 0.65f, 0.88f, 0.85f);
        outline.effectDistance = new Vector2(2f, -2f);

        _titleLabel = CreateText("Title", panel.transform, new Vector2(0f, 250f), new Vector2(980f, 150f), 34f);
        _titleLabel.alignment = TextAlignmentOptions.Center;
        _titleLabel.color = AccentColor;

        var viewportObject = CreateUIObject("Digit Viewport", panel.transform);
        var viewportRect = viewportObject.GetComponent<RectTransform>();
        viewportRect.anchorMin = viewportRect.anchorMax = new Vector2(0.5f, 0.5f);
        viewportRect.sizeDelta = new Vector2(920f, 184f);
        viewportRect.anchoredPosition = new Vector2(0f, 65f);
        viewportObject.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0.22f);
        viewportObject.AddComponent<RectMask2D>();

        var contentObject = CreateUIObject("Digits", viewportObject.transform);
        _content = contentObject.GetComponent<RectTransform>();
        _content.anchorMin = new Vector2(0f, 0.5f);
        _content.anchorMax = new Vector2(0f, 0.5f);
        _content.pivot = new Vector2(0f, 0.5f);
        _content.anchoredPosition = Vector2.zero;
        _content.sizeDelta = new Vector2(0f, 164f);
        var horizontalLayout = contentObject.AddComponent<HorizontalLayoutGroup>();
        horizontalLayout.spacing = 14f;
        horizontalLayout.padding = new RectOffset(16, 16, 10, 10);
        horizontalLayout.childAlignment = TextAnchor.MiddleCenter;
        horizontalLayout.childControlWidth = false;
        horizontalLayout.childControlHeight = false;
        var fitter = contentObject.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

        _scrollRect = viewportObject.AddComponent<ScrollRect>();
        _scrollRect.viewport = viewportRect;
        _scrollRect.content = _content;
        _scrollRect.horizontal = true;
        _scrollRect.vertical = false;
        _scrollRect.movementType = ScrollRect.MovementType.Clamped;
        _scrollRect.inertia = false;
        _scrollRect.scrollSensitivity = 35f;

        _statusLabel = CreateText("Status", panel.transform, new Vector2(0f, -78f), new Vector2(900f, 48f), 30f);
        _statusLabel.alignment = TextAlignmentOptions.Center;

        _hintLabel = CreateText("Hint", panel.transform, new Vector2(0f, -145f), new Vector2(920f, 80f), 24f);
        _hintLabel.alignment = TextAlignmentOptions.Center;
        _hintLabel.color = new Color(0.82f, 0.9f, 0.94f, 1f);

        _unlockButton = CreateButton("UNLOCK", panelRect, new Vector2(-125f, -245f), new Vector2(210f, 62f), 26f);
        _unlockButton.onClick.AddListener(ValidateCurrentCode);
        _cancelButton = CreateButton("BACK", panelRect, new Vector2(125f, -245f), new Vector2(210f, 62f), 26f);
        _cancelButton.onClick.AddListener(() => Close(PasswordPromptResult.Cancelled));

        var controls = CreateText(
            "Controls",
            panel.transform,
            new Vector2(0f, -315f),
            new Vector2(960f, 40f),
            20f);
        controls.text = "Left/Right Select    Up/Down Change    Submit Unlock    Cancel Back";
        controls.alignment = TextAlignmentOptions.Center;
        controls.color = new Color(0.58f, 0.7f, 0.76f, 1f);
    }

    private static GameObject CreateUIObject(string objectName, Transform parent)
    {
        var result = new GameObject(objectName, typeof(RectTransform));
        result.transform.SetParent(parent, false);
        return result;
    }

    private static TextMeshProUGUI CreateText(
        string objectName,
        Transform parent,
        Vector2 position,
        Vector2 size,
        float fontSize)
    {
        var textObject = CreateUIObject(objectName, parent);
        var rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        var label = textObject.AddComponent<TextMeshProUGUI>();
        label.font = TMP_Settings.defaultFontAsset;
        label.fontSize = fontSize;
        label.color = Color.white;
        label.enableWordWrapping = true;
        label.raycastTarget = false;
        return label;
    }

    private static Button CreateButton(
        string label,
        RectTransform parent,
        Vector2 position,
        Vector2 size,
        float fontSize = 22f)
    {
        var buttonObject = CreateUIObject($"{label} Button", parent);
        var rect = buttonObject.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        var image = buttonObject.AddComponent<Image>();
        image.color = SlotColor;
        var button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        var colors = button.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = new Color(1.18f, 1.18f, 1.18f, 1f);
        colors.pressedColor = new Color(0.72f, 0.88f, 0.94f, 1f);
        colors.selectedColor = new Color(1.12f, 1.12f, 1.12f, 1f);
        button.colors = colors;

        var text = CreateText("Label", buttonObject.transform, Vector2.zero, size, fontSize);
        text.text = label;
        text.alignment = TextAlignmentOptions.Center;
        return button;
    }

    private static void StretchToParent(RectTransform rectTransform)
    {
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
