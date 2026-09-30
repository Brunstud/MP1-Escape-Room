
# AION — Time Travel Escape Room

**Course:** CS 498 VR
**Team:** 17
**Integration Branch:** `mp1c-integration`

## Team

- **Rebecca Samuel**
- **Adhav Saravanan**
- **Shicheng Hu**

---

## Overview

**AION** is a multi-room VR escape game centered on experimental time travel, memory reconstruction, and the hidden history of an old institution.

The player begins in the present-day **AION Laboratory**, where the lab is attempting to recover lost records concerning the school's first principal. After calibrating the AION time-travel system, the player is sent approximately 100 years into the past and arrives in the **Principal's Office**.

There, the player discovers that the institution was once connected to **St. Dymphna Asylum** and secret experiments involving subjective time, memory regression, and temporal reconstruction.

The player must solve environmental puzzles, repair the historical AION Time Machine, break a repeating temporal loop, and return to the present.

---

## Current Game Flow

```text
00_AIONLab
│
├── Start Room
│   └── Begin Experiment
│
├── AION Lab Room
│   ├── Navigation Calibration
│   ├── Power Calibration
│   └── Core Calibration
│
└── Initiate Time Travel
        ↓

[01_PhysicsRoom]
Currently skipped in the integration build
        ↓

02_PrincipalOffice
│
├── Investigate the Principal's Office
├── Discover the Secret Room
├── Survive / break the temporal loop
├── Recover evidence
├── Repair the AION Time Machine
└── Return to the present
        ↓

00_AIONLab
└── Final Mission Results / Win Screen
```
