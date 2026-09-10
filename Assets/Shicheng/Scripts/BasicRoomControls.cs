using UnityEngine;
using UnityEngine.InputSystem;

public class BasicRoomControls : MonoBehaviour
{
    // The controller buttons
    public InputActionReference lightAction;
    public InputActionReference teleportAction;
    public InputActionReference quitAction;

    // The light in the room
    public Light roomLight;

    // The player's XR Origin
    public Transform xrOrigin;

    // Two places the player can stand
    public Transform insidePoint;
    public Transform outsidePoint;

    bool isOutside = false;
    int colorIndex = 0;

    Color[] colors =
    {
        Color.white,
        Color.red,
        Color.blue,
        Color.green
    };


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightAction.action.Enable();
        teleportAction.action.Enable();
        quitAction.action.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        // Check light button
        if (lightAction.action.WasPressedThisFrame())
        {
            ChangeLight();
        }

        // Check teleport button
        if (teleportAction.action.WasPressedThisFrame())
        {
            ChangeView();
        }

        // Check quit button
        if (quitAction.action.WasPressedThisFrame())
        {
            QuitGame();
        }
    }


    void ChangeLight()
    {
        // Color Loop
        colorIndex = (colorIndex + 1) % colors.Length;
        roomLight.color = colors[colorIndex];
    }


    void ChangeView()
    {
        if (isOutside == false)
        {
            // Move outside
            xrOrigin.position = outsidePoint.position;
            xrOrigin.rotation = outsidePoint.rotation;

            isOutside = true;
        }
        else
        {
            // Move back inside
            xrOrigin.position = insidePoint.position;
            xrOrigin.rotation = insidePoint.rotation;

            isOutside = false;
        }
    }


    void QuitGame()
    {
#if UNITY_EDITOR
        // Stop Play Mode when testing inside Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the real built game
        Application.Quit();
#endif
    }
}