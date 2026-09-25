using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public TMP_Text collectibleText;

    public int totalCollectibles = 5;

    private int collectedCount = 0;
    public int CollectedCount => collectedCount;
    public int TotalCollectibles => totalCollectibles;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreboard();
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void AddCollectible()
    {
        collectedCount++;

        if (collectedCount > totalCollectibles)
            collectedCount = totalCollectibles;

        UpdateScoreboard();
    }


    private void UpdateScoreboard()
    {
        collectibleText.text =
            "EVIDENCE\n" +
            collectedCount + " / " + totalCollectibles;
    }
}
