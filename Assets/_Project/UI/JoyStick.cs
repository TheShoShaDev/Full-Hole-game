using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System;

public class UIToolkitJoystick : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private float handleRange = 1f;

    private VisualElement joystickContainer;
    private VisualElement joystickBackground;
    private VisualElement joystickHandle;

    private Vector2 joystickCenter;
    private bool isDragging = false;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        // �������� �������� �� UXML
        var root = uiDocument.rootVisualElement;
        joystickContainer = root.Q<VisualElement>("joystick-container");
        joystickBackground = root.Q<VisualElement>("joystick-background");
        joystickHandle = root.Q<VisualElement>("joystick-handle");

        // ��������� ������� ���������
        joystickCenter = joystickBackground.worldBound.center;

        // ������������ �������
        joystickBackground.RegisterCallback<PointerDownEvent>(OnPointerDown);
        joystickBackground.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        joystickBackground.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        isDragging = true;
        UpdateJoystickPosition(evt.position);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!isDragging) return;
        UpdateJoystickPosition(evt.position);
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        isDragging = false;
        Direction = Vector2.zero;
        joystickHandle.style.translate = new StyleTranslate(new Translate(0, 0, 0));
    }

    private void UpdateJoystickPosition(Vector2 touchPos)
    {
        // ��������� ������� ������� � ��������� ����������
        Vector2 localPos = touchPos - joystickCenter;
        Vector2 clampedPos = Vector2.ClampMagnitude(localPos, joystickBackground.worldBound.width * 0.5f * handleRange);

        // ��������� ������� �����
        joystickHandle.style.translate = new StyleTranslate(new Translate(clampedPos.x, clampedPos.y, 0));

        // ��������������� ����������� ([-1, 1] �� X � Y)
        Direction = clampedPos / (joystickBackground.worldBound.width * 0.5f);
    }
}