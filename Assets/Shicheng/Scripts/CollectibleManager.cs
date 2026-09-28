using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public TMP_Text collectibleText;
    public GameObject evidencePanel;
    public TMP_Text evidenceTitleText;
    public TMP_Text evidenceRecordText;

    private CollectibleItem pendingCollectible;

    public int totalCollectibles = 5;

    private int collectedCount = 0;
    public int CollectedCount => collectedCount;
    public int TotalCollectibles => totalCollectibles;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreboard();
        if (evidencePanel != null)
            evidencePanel.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void ShowEvidence(CollectibleItem item)
    {
        pendingCollectible = item;
        evidenceTitleText.text = item.evidenceTitle + "\n[RECOVERED]";
        evidenceRecordText.text = item.evidenceRecord;
        evidencePanel.SetActive(true);
    }

    public void ArchiveEvidence()
    {
        if (pendingCollectible == null)
            return;

        // Trial 17 only becomes "known" after the player confirms / archives it.
        if (!string.IsNullOrEmpty(pendingCollectible.evidenceTitle) &&
            pendingCollectible.evidenceTitle.Contains("SYNCHRONIZATION TRIAL 17"))
        {
            TimeLoopManager.RevealLoopKnowledge();
        }

        AddCollectible();
        pendingCollectible.gameObject.SetActive(false);
        pendingCollectible = null;
        evidencePanel.SetActive(false);
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
