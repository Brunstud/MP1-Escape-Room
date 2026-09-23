using UnityEngine;

public class RevealPanel : MonoBehaviour
{
    public GameObject panel;

    public void Reveal()
    {
        panel.SetActive(false);
    }
}