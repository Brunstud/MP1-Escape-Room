# AION — Time Travel Escape Room

**Course:** CS 498 VR
**Team:** 17
**Integration Branch:** `mp1c-integration`
**Itch.io Build Link:** [https://brunstud.itch.io/mp1-escaped-room](https://brunstud.itch.io/mp1-escaped-room)

## Team

- **Rebecca Samuel**
- **Adhav Saravanan**
- **Shicheng Hu**

## Overview

**AION** is a multi-room VR escape game centered on experimental time travel, hidden institutional history, and subjective temporal regression.

The player begins in the present-day **AION Laboratory**, where an experimental time-travel system is being calibrated to investigate missing records connected to the institution's past.

After completing the laboratory calibration sequence, the player travels into the past and arrives in the **Physics / Observatory Room**. There, the player must solve a set of environmental puzzles and recover three colored capsule keys. Completing all three lock mechanisms releases a sealed historical time machine from its glass enclosure.

The player must then manually activate the newly unlocked time machine to continue deeper into the past.

The final destination is the **Principal's Office**, where the player discovers evidence connecting the institution to **St. Dymphna Asylum** and experiments involving memory, subjective time, and temporal regression.

To escape, the player must investigate the office, uncover hidden evidence, survive a repeating temporal loop, repair the AION Time Machine, and return to the present.

---

## Current Game Flow

```text
00_AIONLab
│
├── Start Room
│   └── Begin Experiment
│
├── AION Laboratory
│   ├── Navigation Calibration
│   ├── Power Calibration
│   └── Core Calibration
│
└── Complete all three calibration locks
        ↓

01_PhysicsRoom
│
├── Explore the Physics / Observatory Room
│
├── Solve the Direction Sequence Puzzle
│   └── Obtain Red Capsule
│
├── Solve the Date Lock Puzzle
│   └── Obtain Blue Capsule
│
├── Locate the Green Capsule
│
├── Insert all three capsules into matching sockets
│   └── Unlock the sealed Time Machine
│
└── Activate the Time Machine
        ↓
   Temporal Transfer
        ↓

02_PrincipalOffice
│
├── Investigate the Principal's Office
├── Discover the Secret Room
├── Recover historical evidence
├── Experience and break the temporal regression loop
├── Repair the historical AION Time Machine
└── Initiate the return sequence
        ↓

00_AIONLab
└── Final Mission Results / Win Screen
```
