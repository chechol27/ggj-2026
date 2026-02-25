using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ManualPopupUI : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text body;

    [Header("Close Settings")]
    [SerializeField] private bool closeOnLeftClickAnywhere = true;
    [SerializeField] private bool closeOnEscape = true;

    [SerializeField] private float minOpenTime = 0.15f;

    public event Action OnClosed;

    private float openedAtUnscaled;

    private void OnEnable()
    {
        openedAtUnscaled = Time.unscaledTime;
    }

    public void Setup(GameStageType stage, string text)
    {
        if (title != null) title.text = stage.ToString();
        if (body != null) body.text = text;
    }

    private void Update()
    {
        if (Time.unscaledTime - openedAtUnscaled < minOpenTime)
            return;

        if (closeOnEscape && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Close();
            return;
        }

        if (closeOnLeftClickAnywhere && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Close();
            return;
        }
    }

    private void Close()
    {
        OnClosed?.Invoke();
        Destroy(gameObject);
    }
}