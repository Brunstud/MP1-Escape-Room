using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TimeMachineProgress : MonoBehaviour
{
    // UI
    public TMP_Text statusText;

    // NEW: Two scoreboard mirrors
    public TMP_Text progressScoreboardText;
    public TMP_Text puzzleScoreboardText;


    // Real machine parts
    public GameObject part1Object;
    public GameObject part2Object;
    public GameObject part3Object;


    // Ghost models
    public GameObject part1Ghost;
    public GameObject part2Ghost;
    public GameObject part3Ghost;

    public GameObject powerSourceGhost;
    public GameObject resonantMetalGhost;


    // Ghost fade
    public float ghostFadeTime = 0.6f;
    public float ghostAlpha = 0.35f;


    // Stage 2 sockets
    public GameObject powerSourceSocket;
    public GameObject resonantMetalSocket;


    // Progress
    public bool part1Installed = false;
    public bool part2Installed = false;
    public bool part3Installed = false;

    public bool powerSourceInstalled = false;
    public bool resonantMetalInstalled = false;


    // -------------------------
    // NEW: Key discovery
    // -------------------------

    public bool part1Discovered = false;
    public bool part2Discovered = false;
    public bool part3Discovered = false;

    public bool powerSourceDiscovered = false;
    public bool resonantMetalDiscovered = false;


    // -------------------------
    // NEW: Puzzle clues
    // -------------------------

    public int puzzle1TotalClues = 3;
    public int puzzle2TotalClues = 2;

    public int puzzle1FoundClues = 0;
    public int puzzle2FoundClues = 0;


    // Becomes true after all three machine parts are installed
    private bool fuelStageStarted = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!AllPartsInstalled())
        {
            // Stage 2 should not be available at the beginning
            powerSourceSocket.SetActive(false);
            resonantMetalSocket.SetActive(false);
        }

        UpdateProgressText();

        // NEW
        UpdateProgressScoreboard();
        UpdatePuzzleScoreboard();
    }


    // Update is called once per frame
    void Update()
    {

    }


    // -------------------------
    // NEW: Discover Keys
    // -------------------------

    public void Part1Discovered()
    {
        part1Discovered = true;
        UpdateProgressScoreboard();
    }

    public void Part2Discovered()
    {
        part2Discovered = true;
        UpdateProgressScoreboard();
    }

    public void Part3Discovered()
    {
        part3Discovered = true;
        UpdateProgressScoreboard();
    }

    public void PowerSourceDiscovered()
    {
        powerSourceDiscovered = true;
        UpdateProgressScoreboard();
    }

    public void ResonantMetalDiscovered()
    {
        resonantMetalDiscovered = true;
        UpdateProgressScoreboard();
    }


    // -------------------------
    // Part 1
    // -------------------------

    public void Part1Inserted()
    {
        part1Installed = true;

        // NEW
        part1Discovered = true;

        if (part1Ghost != null)
            StartCoroutine(FadeOutGhost(part1Ghost));

        CheckProgress();
    }


    public void Part1Removed()
    {
        if (fuelStageStarted)
            return;

        part1Installed = false;

        if (part1Ghost != null)
            ShowGhost(part1Ghost);

        CheckProgress();
    }


    // -------------------------
    // Part 2
    // -------------------------

    public void Part2Inserted()
    {
        part2Installed = true;

        // NEW
        part2Discovered = true;

        if (part2Ghost != null)
            StartCoroutine(FadeOutGhost(part2Ghost));

        CheckProgress();
    }


    public void Part2Removed()
    {
        if (fuelStageStarted)
            return;

        part2Installed = false;

        if (part2Ghost != null)
            ShowGhost(part2Ghost);

        CheckProgress();
    }


    // -------------------------
    // Part 3
    // -------------------------

    public void Part3Inserted()
    {
        part3Installed = true;

        // NEW
        part3Discovered = true;

        if (part3Ghost != null)
            StartCoroutine(FadeOutGhost(part3Ghost));

        CheckProgress();
    }


    public void Part3Removed()
    {
        if (fuelStageStarted)
            return;

        part3Installed = false;

        if (part3Ghost != null)
            ShowGhost(part3Ghost);

        CheckProgress();
    }


    // -------------------------
    // Power source
    // -------------------------

    public void PowerSourceInserted()
    {
        powerSourceInstalled = true;

        // NEW
        powerSourceDiscovered = true;

        if (powerSourceGhost != null)
            StartCoroutine(FadeOutGhost(powerSourceGhost));

        CheckProgress();
    }


    public void PowerSourceRemoved()
    {
        powerSourceInstalled = false;

        if (powerSourceGhost != null)
            ShowGhost(powerSourceGhost);

        CheckProgress();
    }


    // -------------------------
    // Resonant metal
    // -------------------------

    public void ResonantMetalInserted()
    {
        resonantMetalInstalled = true;

        // NEW
        resonantMetalDiscovered = true;

        if (resonantMetalGhost != null)
            StartCoroutine(FadeOutGhost(resonantMetalGhost));

        CheckProgress();
    }


    public void ResonantMetalRemoved()
    {
        resonantMetalInstalled = false;

        if (resonantMetalGhost != null)
            ShowGhost(resonantMetalGhost);

        CheckProgress();
    }


    // -------------------------
    // NEW: Puzzle clues
    // -------------------------

    public void FindPuzzle1Clue()
    {
        if (puzzle1FoundClues < puzzle1TotalClues)
            puzzle1FoundClues++;

        UpdatePuzzleScoreboard();
    }


    public void FindPuzzle2Clue()
    {
        if (puzzle2FoundClues < puzzle2TotalClues)
            puzzle2FoundClues++;

        UpdatePuzzleScoreboard();
    }


    // -------------------------
    // Progress
    // -------------------------

    private void CheckProgress()
    {
        if (AllPartsInstalled() && !fuelStageStarted)
        {
            fuelStageStarted = true;

            // LockMachineParts();

            powerSourceSocket.SetActive(true);
            resonantMetalSocket.SetActive(true);
        }

        UpdateProgressText();

        // NEW
        UpdateProgressScoreboard();
    }


    private bool AllPartsInstalled()
    {
        return part1Installed &&
               part2Installed &&
               part3Installed;
    }


    private bool AllFuelInstalled()
    {
        return powerSourceInstalled &&
               resonantMetalInstalled;
    }


    public bool MachineReady()
    {
        return AllPartsInstalled() &&
               AllFuelInstalled();
    }


    // -------------------------
    // Lock Stage 1 parts
    // -------------------------

    private void LockMachineParts()
    {
        LockPart(part1Object);
        LockPart(part2Object);
        LockPart(part3Object);
    }


    private void LockPart(GameObject partObject)
    {
        if (partObject == null)
            return;

        Rigidbody rb = partObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.useGravity = false;
            rb.isKinematic = true;
        }

        XRGrabInteractable grab =
            partObject.GetComponent<XRGrabInteractable>();

        if (grab != null)
            grab.enabled = false;
    }


    // -------------------------
    // Ghost fade
    // -------------------------

    private IEnumerator FadeOutGhost(GameObject ghost)
    {
        if (ghost == null)
            yield break;

        Renderer[] renderers =
            ghost.GetComponentsInChildren<Renderer>();

        float time = 0f;

        while (time < ghostFadeTime)
        {
            time += Time.deltaTime;

            float t = time / ghostFadeTime;

            // Smooth ease in and out
            t = t * t * (3f - 2f * t);

            foreach (Renderer rend in renderers)
            {
                foreach (Material mat in rend.materials)
                {
                    Color color = mat.color;
                    color.a =
                        Mathf.Lerp(ghostAlpha, 0f, t);

                    mat.color = color;
                }
            }

            yield return null;
        }

        ghost.SetActive(false);
    }


    private void ShowGhost(GameObject ghost)
    {
        if (ghost == null)
            return;

        ghost.SetActive(true);

        Renderer[] renderers =
            ghost.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                Color color = mat.color;
                color.a = ghostAlpha;
                mat.color = color;
            }
        }
    }


    // -------------------------
    // NEW: Progress Scoreboard
    // -------------------------

    private void UpdateProgressScoreboard()
    {
        if (progressScoreboardText == null)
            return;


        string keys =
            GetCircle(part1Discovered) + " " +
            GetCircle(part2Discovered) + " " +
            GetCircle(part3Discovered) + " " +
            GetCircle(powerSourceDiscovered) + " " +
            GetCircle(resonantMetalDiscovered);


        string locks =
            GetCircle(part1Installed) + " " +
            GetCircle(part2Installed) + " " +
            GetCircle(part3Installed) + " " +
            GetCircle(powerSourceInstalled) + " " +
            GetCircle(resonantMetalInstalled);


        progressScoreboardText.text =
            "AION // PROGRESS\n" +
            "KEYS\n" +
            keys + "\n" +
            "LOCKS\n" +
            locks;
    }


    // -------------------------
    // NEW: Puzzle Scoreboard
    // -------------------------

    private void UpdatePuzzleScoreboard()
    {
        if (puzzleScoreboardText == null)
            return;


        string puzzle1 =
            GetPuzzleCircles(
                puzzle1FoundClues,
                puzzle1TotalClues
            );


        string puzzle2 =
            GetPuzzleCircles(
                puzzle2FoundClues,
                puzzle2TotalClues
            );


        puzzleScoreboardText.text =
            "ARCHIVE // TRACE\n" +
            "α    " + puzzle1 + "\n" +
            "β    " + puzzle2;
    }


    // -------------------------
    // NEW: Visual helpers
    // -------------------------

    private string GetCircle(bool complete)
    {
        return complete ? "●" : "○";
    }


    private string GetPuzzleCircles(
        int found,
        int total
    )
    {
        string result = "";

        for (int i = 0; i < total; i++)
        {
            if (i < found)
                result += "●";
            else
                result += "○";

            if (i < total - 1)
                result += " ";
        }

        return result;
    }


    // -------------------------
    // Update machine screen
    // -------------------------

    private void UpdateProgressText()
    {
        if (!AllPartsInstalled())
        {
            string part1 =
                part1Installed ? "●" : "○";

            string part2 =
                part2Installed ? "●" : "○";

            string part3 =
                part3Installed ? "●" : "○";


            statusText.text =
                "REPAIR THE TIME MACHINE\n" +
                "Find and restore 3 missing parts.\n" +
                part1 + "  " +
                part2 + "  " +
                part3;
        }
        else if (!AllFuelInstalled())
        {
            string powerText =
                powerSourceInstalled ?
                "● READY" :
                "○ MISSING";

            string metalText =
                resonantMetalInstalled ?
                "● READY" :
                "○ MISSING";


            statusText.text =
                "START-UP MATERIALS\n" +
                "POWER: " + powerText + "\n" +
                "METAL: " + metalText;
        }
        else
        {
            statusText.text =
                "TIME MACHINE READY\n" +
                "Enter the chamber.";
        }
    }
}