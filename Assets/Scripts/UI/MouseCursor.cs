using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursor : MonoBehaviour
{
    [Header("Input Reference Data")]
    [SerializeField] private InputActionReference pointerPosInput;

    [Header("Scene Camera")]
    [SerializeField] private Camera sceneCamera;

    private Vector2 mousePosition;

    private void Start()
    {
        /* Makes the system mouse cursor invisible when game is playing. */
        Cursor.visible = false;
    }

    private void Update()
    {
        /* Converts to the mouse position to a world point position in the scene. */
        mousePosition = sceneCamera.ScreenToWorldPoint(pointerPosInput.action.ReadValue<Vector2>());
        transform.position = mousePosition;
    }
}
