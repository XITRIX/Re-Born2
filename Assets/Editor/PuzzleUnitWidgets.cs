using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Widget(typeof(ReadableDocumentUnit))]
public sealed class ReadableDocumentUnitWidget : UnitWidget<ReadableDocumentUnit>
{
    public ReadableDocumentUnitWidget(FlowCanvas canvas, ReadableDocumentUnit unit)
        : base(canvas, unit)
    {
    }

    public override Inspector GetPortInspector(IUnitPort port, Metadata metadata)
    {
        if (port.key == nameof(ReadableDocumentUnit.Title))
            return new StackedStringPortInspector(metadata, "Title");

        if (port.key == nameof(ReadableDocumentUnit.Body))
            return new StackedStringPortInspector(metadata, "Body", 8, 20);

        if (port.key == nameof(ReadableDocumentUnit.DiscoveredGameKey))
            return new StackedStringPortInspector(metadata, "Discovered Game Key");

        return base.GetPortInspector(port, metadata);
    }
}

[Descriptor(typeof(ReadableDocumentUnit))]
public sealed class ReadableDocumentUnitDescriptor : UnitDescriptor<ReadableDocumentUnit>
{
    public ReadableDocumentUnitDescriptor(ReadableDocumentUnit unit)
        : base(unit)
    {
    }

    protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
    {
        base.DefinedPort(port, description);

        if (port.key == nameof(ReadableDocumentUnit.Title) ||
            port.key == nameof(ReadableDocumentUnit.Body) ||
            port.key == nameof(ReadableDocumentUnit.DiscoveredGameKey))
        {
            description.showLabel = port is ValueInput valueInput && valueInput.hasValidConnection;
        }
    }
}

[Widget(typeof(PasswordPromptUnit))]
public sealed class PasswordPromptUnitWidget : UnitWidget<PasswordPromptUnit>
{
    public PasswordPromptUnitWidget(FlowCanvas canvas, PasswordPromptUnit unit)
        : base(canvas, unit)
    {
    }

    public override Inspector GetPortInspector(IUnitPort port, Metadata metadata)
    {
        if (port.key == nameof(PasswordPromptUnit.Title))
            return new StackedStringPortInspector(metadata, "Title", 5, 14);

        if (port.key == nameof(PasswordPromptUnit.ExpectedCode))
            return new StackedStringPortInspector(metadata, "Expected Code");

        if (port.key == nameof(PasswordPromptUnit.ProgressiveHintsText))
            return new StackedStringPortInspector(metadata, "Progressive Hints", 5, 14);

        return base.GetPortInspector(port, metadata);
    }
}

[Descriptor(typeof(PasswordPromptUnit))]
public sealed class PasswordPromptUnitDescriptor : UnitDescriptor<PasswordPromptUnit>
{
    public PasswordPromptUnitDescriptor(PasswordPromptUnit unit)
        : base(unit)
    {
    }

    protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
    {
        base.DefinedPort(port, description);

        if (port.key == nameof(PasswordPromptUnit.Title) ||
            port.key == nameof(PasswordPromptUnit.ExpectedCode) ||
            port.key == nameof(PasswordPromptUnit.ProgressiveHintsText))
        {
            description.showLabel = port is ValueInput valueInput && valueInput.hasValidConnection;
        }
    }
}

internal sealed class StackedStringPortInspector : Inspector
{
    private const float LabelSpacing = 2f;

    private readonly GUIContent _label;
    private readonly int _minimumLines;
    private readonly int _maximumLines;

    public StackedStringPortInspector(
        Metadata metadata,
        string label,
        int minimumLines = 1,
        int maximumLines = 1)
        : base(metadata)
    {
        _label = new GUIContent(label);
        _minimumLines = minimumLines;
        _maximumLines = maximumLines;
    }

    private bool IsMultiline => _maximumLines > 1;

    protected override bool cacheHeight => !IsMultiline;

    protected override float GetHeight(float width, GUIContent label)
    {
        var contentHeight = EditorGUIUtility.singleLineHeight +
                            LabelSpacing +
                            GetTextFieldHeight(width);

        return HeightWithLabel(metadata, width, contentHeight, label);
    }

    protected override void OnGUI(Rect position, GUIContent label)
    {
        position = BeginLabeledBlock(metadata, position, label);

        var labelPosition = position.VerticalSection(
            ref y,
            EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(labelPosition, _label);

        y += LabelSpacing;

        var textFieldPosition = position.VerticalSection(
            ref y,
            GetTextFieldHeight(position.width));

        var currentValue = metadata.value as string ?? string.Empty;
        var newValue = IsMultiline
            ? EditorGUI.TextArea(
                textFieldPosition,
                currentValue,
                LudiqStyles.textAreaWordWrapped)
            : EditorGUI.TextField(textFieldPosition, currentValue);

        if (EndBlock(metadata))
        {
            metadata.RecordUndo();
            metadata.value = newValue;
        }
    }

    public override float GetAdaptiveWidth()
    {
        return 200f;
    }

    private float GetTextFieldHeight(float width)
    {
        if (!IsMultiline)
            return EditorGUIUtility.singleLineHeight;

        var currentValue = metadata.value as string ?? string.Empty;
        var calculatedHeight = LudiqStyles.textAreaWordWrapped.CalcHeight(
            new GUIContent(currentValue),
            width);
        var lineHeight = EditorStyles.textArea.lineHeight;
        var verticalPadding = EditorStyles.textArea.padding.top + EditorStyles.textArea.padding.bottom;
        var minimumHeight = lineHeight * _minimumLines + verticalPadding;
        var maximumHeight = lineHeight * _maximumLines + verticalPadding;

        return Mathf.Clamp(calculatedHeight, minimumHeight, maximumHeight);
    }
}
