using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TimeMachineProgress : MonoBehaviour
{
    // UI
    public TMP_Text statusText;


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


    // Stage 2 sockets
    public GameObject powerSourceSocket;
    public GameObject resonantMetalSocket;


    // Progress
    public bool part1Installed = false;
    public bool part2Installed = false;
    public bool part3Installed = false;

    public bool powerSourceInstalled = false;
    public bool resonantMetalInstalled = false;


    // Becomes true after all three machine parts are installed
    private bool fuelStageStarted = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Stage 2 should not be available at the beginning
        powerSourceSocket.SetActive(false);
        resonantMetalSocket.SetActive(false);

        UpdateProgressText();
    }


    // Update is called once per frame
    void Update()
    {

    }


    // -------------------------
    // Part 1
    // -------------------------

    public void Part1Inserted()
    {
        part1Installed = true;

        if (part1Ghost != null)
            part1Ghost.SetActive(false);

        CheckProgress();
    }


    public void Part1Removed()
    {
        // Stage 1 cannot go backwards after Stage 2 starts
        if (fuelStageStarted)
            return;

        part1Installed = false;

        if (part1Ghost != null)
            part1Ghost.SetActive(true);

        CheckProgress();
    }


    // -------------------------
    // Part 2
    // -------------------------

    public void Part2Inserted()
    {
        part2Installed = true;

        if (part2Ghost != null)
            part2Ghost.SetActive(false);

        CheckProgress();
    }


    public void Part2Removed()
    {
        if (fuelStageStarted)
            return;

        part2Installed = false;

        if (part2Ghost != null)
            part2Ghost.SetActive(true);

        CheckProgress();
    }


    // -------------------------
    // Part 3
    // -------------------------

    public void Part3Inserted()
    {
        part3Installed = true;

        if (part3Ghost != null)
            part3Ghost.SetActive(false);

        CheckProgress();
    }


    public void Part3Removed()
    {
        if (fuelStageStarted)
            return;

        part3Installed = false;

        if (part3Ghost != null)
            part3Ghost.SetActive(true);

        CheckProgress();
    }


    // -------------------------
    // Power source
    // -------------------------

    public void PowerSourceInserted()
    {
        powerSourceInstalled = true;

        if (powerSourceGhost != null)
            powerSourceGhost.SetActive(false);

        CheckProgress();
    }


    public void PowerSourceRemoved()
    {
        powerSourceInstalled = false;

        if (powerSourceGhost != null)
            powerSourceGhost.SetActive(true);

        CheckProgress();
    }


    // -------------------------
    // Resonant metal
    // -------------------------

    public void ResonantMetalInserted()
    {
        resonantMetalInstalled = true;

        if (resonantMetalGhost != null)
            resonantMetalGhost.SetActive(false);

        CheckProgress();
    }


    public void ResonantMetalRemoved()
    {
        resonantMetalInstalled = false;

        if (resonantMetalGhost != null)
            resonantMetalGhost.SetActive(true);

        CheckProgress();
    }


    // -------------------------
    // Progress
    // -------------------------

    private void CheckProgress()
    {
        if (AllPartsInstalled() && !fuelStageStarted)
        {
            fuelStageStarted = true;

            LockMachineParts();

            powerSourceSocket.SetActive(true);
            resonantMetalSocket.SetActive(true);
        }

        UpdateProgressText();
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
    // Update machine screen
    // -------------------------

    private void UpdateProgressText()
    {
        if (!AllPartsInstalled())
        {
            string part1 = part1Installed ? "●" : "○";
            string part2 = part2Installed ? "●" : "○";
            string part3 = part3Installed ? "●" : "○";

            statusText.text =
                "REPAIR THE TIME MACHINE\n" +
                "Find and restore 3 missing parts.\n" +
                part1 + "  " + part2 + "  " + part3;
        }
        else if (!AllFuelInstalled())
        {
            string powerText =
                powerSourceInstalled ? "READY" : "MISSING";

            string metalText =
                resonantMetalInstalled ? "READY" : "MISSING";

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