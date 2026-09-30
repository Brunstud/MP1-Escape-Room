using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public InputActionReference action;
    public Transform externalViewPoint;

    private Vector3 roomPosition;
    private Quaternion roomRotation;
    private bool outside = false;

    void Start()
    {
        roomPosition = transform.position;
        roomRotation = transform.rotation;

        action.action.Enable();
        action.action.performed += MovePlayer;
    }

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            TogglePosition();
        }
    }

    void MovePlayer(InputAction.CallbackContext ctx)
    {
        TogglePosition();
    }

    void TogglePosition()
    {
        if (!outside)
        {
            transform.position = externalViewPoint.position;
            transform.rotation = externalViewPoint.rotation;
            outside = true;
        }
        else
        {
            transform.position = roomPosition;
            transform.rotation = roomRotation;
            outside = false;
        }
    }
}