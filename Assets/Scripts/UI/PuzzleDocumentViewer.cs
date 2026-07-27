using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class PuzzleDocumentViewer : MonoBehaviour
{
    public static PuzzleDocumentViewer Shared { get; private set; }

    private Canvas _canvas;
    private GameObject _root;
    private TextMeshProUGUI _titleLabel;
    private TextMeshProUGUI _bodyLabel;
    private ScrollRect _scrollRect;
    private PlayerControlMap _controlMap;
    private GameObject _previousSelectedObject;
    private Button _closeButton;
    private bool _isOpen;
    private bool _restorePlayerInput;
    private float _acceptInputAfter;

    public bool IsOpen => _isOpen;

    public static PuzzleDocumentViewer EnsureExists()
    {
        if (Shared != null)
            return Shared;

        var viewerObject = new GameObject("[Puzzle Document Viewer]");
        DontDestroyOnLoad(viewerObject);
        return viewerObject.AddComponent<PuzzleDocumentViewer>();
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

    public void Open(string title, string body)
    {
        if (_isOpen)
            return;

        _titleLabel.text = string.IsNullOrWhiteSpace(title) ? "DOCUMENT" : title;
        _bodyLabel.text = body ?? string.Empty;
        _scrollRect.verticalNormalizedPosition = 1f;

        _previousSelectedObject = EventSystem.current != null
            ? EventSystem.current.currentSelectedGameObject
            : null;

        _restorePlayerInput = PlayerInputScript.Shared != null &&
                              PlayerInputScript.Shared.IsPlayerInputEnabled;
        PlayerInputScript.Shared?.DisablePlayerInput();
        _root.SetActive(true);
        _isOpen = true;
        _acceptInputAfter = Time.unscaledTime + 0.12f;
        _controlMap.UI.Navigate.Enable();
        _controlMap.UI.Submit.Enable();
        _controlMap.UI.Cancel.Enable();
        Canvas.ForceUpdateCanvases();
        _scrollRect.verticalNormalizedPosition = 1f;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_closeButton.gameObject);
        }
    }

    private void Update()
    {
        if (!_isOpen)
            return;

        var navigation = _controlMap.UI.Navigate.ReadValue<Vector2>();
        if (Mathf.Abs(navigation.y) > 0.15f)
        {
            _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(
                _scrollRect.verticalNormalizedPosition + navigation.y * Time.unscaledDeltaTime * 0.8f);
        }

        if (Time.unscaledTime >= _acceptInputAfter &&
            (_controlMap.UI.Submit.WasPressedThisFrame() || _controlMap.UI.Cancel.WasPressedThisFrame()))
            Close();
    }

    private void Close()
    {
        if (!_isOpen)
            return;

        _isOpen = false;
        _controlMap.UI.Navigate.Disable();
        _controlMap.UI.Submit.Disable();
        _controlMap.UI.Cancel.Disable();
        _root.SetActive(false);

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (_previousSelectedObject != null && _previousSelectedObject.activeInHierarchy)
                EventSystem.current.SetSelectedGameObject(_previousSelectedObject);
        }

        if (_restorePlayerInput)
            PlayerInputScript.Shared?.EnablePlayerInput();
    }

    private void BuildInterface()
    {
        _canvas = gameObject.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _canvas.sortingOrder = 5001;

        var scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        gameObject.AddComponent<GraphicRaycaster>();

        _root = CreateUIObject("Document Root", transform);
        StretchToParent(_root.GetComponent<RectTransform>());
        _root.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0.82f);

        var panel = CreateUIObject("Paper", _root.transform);
        var panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = Vector2.zero;
        panelRect.sizeDelta = new Vector2(1180f, 820f);
        panel.AddComponent<Image>().color = new Color(0.91f, 0.9f, 0.82f, 1f);

        _titleLabel = CreateText("Title", panel.transform, new Vector2(0f, 345f), new Vector2(1040f, 70f), 38f);
        _titleLabel.color = new Color(0.035f, 0.13f, 0.18f, 1f);
        _titleLabel.alignment = TextAlignmentOptions.Center;
        _titleLabel.fontStyle = FontStyles.Bold;

        var viewportObject = CreateUIObject("Viewport", panel.transform);
        var viewportRect = viewportObject.GetComponent<RectTransform>();
        viewportRect.anchorMin = viewportRect.anchorMax = new Vector2(0.5f, 0.5f);
        viewportRect.anchoredPosition = new Vector2(0f, 5f);
        viewportRect.sizeDelta = new Vector2(1040f, 590f);
        viewportObject.AddComponent<RectMask2D>();

        var bodyObject = CreateUIObject("Body", viewportObject.transform);
        var bodyRect = bodyObject.GetComponent<RectTransform>();
        bodyRect.anchorMin = new Vector2(0f, 1f);
        bodyRect.anchorMax = new Vector2(1f, 1f);
        bodyRect.pivot = new Vector2(0.5f, 1f);
        bodyRect.anchoredPosition = Vector2.zero;
        bodyRect.sizeDelta = new Vector2(0f, 590f);

        _bodyLabel = bodyObject.AddComponent<TextMeshProUGUI>();
        _bodyLabel.font = TMP_Settings.defaultFontAsset;
        _bodyLabel.fontSize = 27f;
        _bodyLabel.color = new Color(0.08f, 0.09f, 0.085f, 1f);
        _bodyLabel.alignment = TextAlignmentOptions.TopLeft;
        _bodyLabel.enableWordWrapping = true;
        _bodyLabel.overflowMode = TextOverflowModes.Overflow;
        _bodyLabel.margin = new Vector4(24f, 18f, 24f, 18f);

        var fitter = bodyObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        _scrollRect = viewportObject.AddComponent<ScrollRect>();
        _scrollRect.viewport = viewportRect;
        _scrollRect.content = bodyRect;
        _scrollRect.horizontal = false;
        _scrollRect.vertical = true;
        _scrollRect.movementType = ScrollRect.MovementType.Clamped;
        _scrollRect.inertia = true;
        _scrollRect.scrollSensitivity = 35f;

        _closeButton = CreateButton("CLOSE", panelRect, new Vector2(0f, -360f), new Vector2(220f, 58f));
        _closeButton.onClick.AddListener(Close);
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
        label.enableWordWrapping = true;
        label.raycastTarget = false;
        return label;
    }

    private static Button CreateButton(string label, RectTransform parent, Vector2 position, Vector2 size)
    {
        var buttonObject = CreateUIObject($"{label} Button", parent);
        var rect = buttonObject.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        var image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.035f, 0.2f, 0.27f, 1f);
        var button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;

        var text = CreateText("Label", buttonObject.transform, Vector2.zero, size, 24f);
        text.text = label;
        text.color = Color.white;
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
