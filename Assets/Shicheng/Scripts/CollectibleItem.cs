using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public CollectibleManager collectibleManager;

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
            collectibleManager.AddCollectible();

        gameObject.SetActive(false);
    }
}