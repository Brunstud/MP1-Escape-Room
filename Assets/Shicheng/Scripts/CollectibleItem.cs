using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public CollectibleManager collectibleManager;
    public string evidenceTitle;

    [TextArea(5, 12)]
    public string evidenceRecord;

    private bool collected = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }


    public void Collect()
    {
        if (collected)
            return;

        collected = true;

        if (collectibleManager != null)
            collectibleManager.ShowEvidence(this);
        else
            gameObject.SetActive(false);
    }
}
