using UnityEngine;

public class RainbowLighting : MonoBehaviour
{
    public Light targetLight;

    private bool isCyan = false;

    public void ToggleLight()
    {
        isCyan = !isCyan;

        if (isCyan)
        {
            targetLight.color = Color.cyan;
        }
        else
        {
            targetLight.color = Color.white;
        }
    }
}