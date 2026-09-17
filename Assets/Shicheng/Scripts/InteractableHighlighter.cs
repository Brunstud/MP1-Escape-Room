using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableHighlighter : MonoBehaviour
{
    public enum HighlightType
    {
        MachinePart,
        Fuel,
        Information,
        Miscellaneous
    }

    public HighlightType highlightType;

    public Renderer[] renderers;

    public float emissionIntensity = 3f;

    private Color highlightColor;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable =
            GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        SetColorByType();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        if (interactable == null)
            return;

        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);

        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        if (interactable == null)
            return;

        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);

        interactable.selectEntered.RemoveListener(OnSelectEntered);
        interactable.selectExited.RemoveListener(OnSelectExited);
    }

    private void SetColorByType()
    {
        switch (highlightType)
        {
            case HighlightType.MachinePart:
                highlightColor = new Color(1.0f, 0.83f, 0.28f);
                // #FFD447 - machine part candidates and matching machine sockets
                break;

            case HighlightType.Fuel:
                highlightColor = new Color(1.0f, 0.48f, 0.20f);
                // #FF7A33 - fuel/start-up materials and their matching slots
                break;

            case HighlightType.Information:
                highlightColor = new Color(0.44f, 0.91f, 1.0f);
                // #6FE8FF - interactable objects that reveal information or give score
                break;

            case HighlightType.Miscellaneous:
                highlightColor = new Color(0.26f, 0.90f, 0.69f);
                // #43E6B1 - extra interactable objects that do not give score
                break;
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        SetHighlight(true);
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        SetHighlight(false);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        SetHighlight(true);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        SetHighlight(false);
    }

    private void SetHighlight(bool enabled)
    {
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                if (enabled)
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor",
                        highlightColor * emissionIntensity);
                }
                else
                {
                    mat.SetColor("_EmissionColor", Color.black);
                }
            }
        }
    }
}