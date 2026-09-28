using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public InputActionReference toggleAction;
    private Light pointLight;

    void Start()
    {
        pointLight = GetComponent<Light>();
        toggleAction.action.Enable();
        toggleAction.action.performed += ctx =>
        {
            SwitchLight();
        };
    }

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SwitchLight();
        }
    }

    void SwitchLight() {
        if (pointLight.color != Color.blue) {
            pointLight.color = Color.blue;
        } else {
            pointLight.color = Color.white;
        }
    }
}