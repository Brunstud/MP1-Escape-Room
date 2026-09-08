
# MP1a — Shicheng's Room

**Team:** 17
**Developer:** Shicheng Hu
**Development Branch:** `mp1a-shicheng`

## Scene Concept

My MP1a room will serve as an early prototype of the **100-years-ago Principal's Office** for our later Escape Room project.

The room will contain an experimental **AION Temporal Core**, historical office decorations, VR interaction, and several visual/audio feedback systems.

---

# Current Progress

**Current Score: 0 / 42**

> Update this number whenever a rubric feature is completed and tested.

Legend:

- ✅ Completed and tested
- 🚧 In progress
- ⬜ Not started

---

# Core Requirements — 21 pts

## User Feedback

### ⬜ Particle Bursts — 2 pts

**Plan:**
When the player spawns a Chronal Orb with the VR controller, a particle burst will appear at the orb's spawn position.

**Implementation idea:**

- Particle System prefab
- Spawn at controller/orb position
- Trigger together with object spawning

---

### ⬜ Spatial Sound — 2 pts

**Plan:**
Spawning a Chronal Orb will also play a spatialized sound effect at the spawn location.

**Implementation idea:**

- AudioSource with Spatial Blend = 1
- Spawn/play sound at the same position as the orb

---

# View

### ⬜ Object Space — 1 pt

**Plan:**
Create an antique orrery / planet-and-moon system.

The Moon will be a child of the Planet so that rotating the Planet causes the Moon to move with it.

---

### ⬜ World Space — 1 pt

**Plan:**Add a World Space Canvas inside the Principal's Office displaying:

- VR controls
- AION machine status
- possibly historical knowledge score

The UI must stay in the room rather than following the player's camera.

---

### ⬜ Materials — 1 pt

**Plan:**Use multiple custom materials throughout the Principal's Office, including:

- wood
- brass
- paper
- carpet
- wall
- glass
- metal

---

### ⬜ Highlight Outline — 2 pts

**Plan:**
Apply an outline shader/material to an important interactable object, such as the **Temporal Anchor**.

This will visually indicate that the object can be collected or interacted with.

---

### ⬜ XR Tracked Camera — 2 pts

**Plan:**
Use OpenXR + XR Interaction Toolkit with an XR Origin.

The headset position and rotation should control the player's camera.

The final demonstration video will be recorded from the headset POV.

---

# World

### ⬜ Euler Steady — 2 pts

**Plan:**
Continuously rotate the antique orrery / planet system using:

`Time.deltaTime`

This avoids frame-rate-dependent motion.

---

### ⬜ Kinematic Double Integrators — 2 pts

**Plan:**The Chronal Orb will maintain:

- acceleration
- velocity
- position

Each frame:

`velocity += acceleration * deltaTime`

`position += velocity * deltaTime`

This will simulate gravitational movement around the AION Temporal Core.

---

# Execution

### ⬜ XR Controller Inputs — 1 pt

**Plan:**
Map VR controller buttons to several scripts.

Possible controls:

- Trigger → Spawn Chronal Orb
- A → Change lighting
- B → Teleport viewpoint
- Menu → Quit

---

### ⬜ Quit Key — 1 pt

**Plan:**
A VR controller button will quit the standalone application.

---

### ⬜ Object Spawning — 1 pt

**Plan:**
Pressing a controller button will instantiate a **Chronal Orb prefab** in front of the controller.

---

### ⬜ Camera Teleport — 3 pts

**Plan:**A controller button will switch the player between:

1. the Principal's Office
2. an external observation point

The external viewpoint will also make the skybox clearly visible.

---

# Side Quests — 8 pts

### ⬜ Object Shooter — 2 pts

**Plan:**
The spawned Chronal Orb will receive an initial velocity based on the direction the VR controller is pointing.

---

### ⬜ Arbitrary Orbiter — 2 pts

**Plan:**
The Chronal Orb will experience acceleration toward the **AION Temporal Core**, rather than toward the world origin.

---

### ⬜ Perfect Orbits — 2 pts

**Plan:**
Adjust the spawned orb's initial velocity so that it forms a stable orbit around the Temporal Core.

The orbital velocity magnitude will use:

`sqrt(gravity / distance)`

---

### ⬜ Skybox Material — 1 pt

**Plan:**
Apply a non-default skybox material.

The skybox will be demonstrated from the external camera viewpoint.

---

### ⬜ Rainbow Lighting — 1 pt

**Plan:**
A controller button will cycle the main room light through several colors.

Example:

White → Red → Blue → Green → White

---

# Content Stories — 13 pts

### ⬜ Object Content — 3 pts

**Target:** 16+ object assets

Planned Principal's Office objects may include:

1. Desk
2. Chair
3. Bookshelf
4. Filing cabinet
5. Clock
6. Globe
7. Lamp
8. Telephone
9. Typewriter
10. Newspaper
11. Principal portrait
12. Rug
13. Vase
14. Ink bottle
15. Books
16. AION Temporal Core

Target: **20+ objects** for safety.

---

### ⬜ Material Content — 4 pts

**Target:** 13+ materials

Possible materials:

1. Dark Wood
2. Light Wood
3. Brass
4. Steel
5. Paper
6. Leather
7. Carpet
8. Wall Plaster
9. Ceiling
10. Glass
11. Curtain Fabric
12. Painted Metal
13. Temporal Core Glow
14. Portrait Canvas

Target: **14–15 materials**.

---

### ⬜ Particle Feedback Content — 3 pts

**Target:** 16+ particle feedback emitters

Plan:

Historical evidence / collectible objects will generate a small particle effect when collected.

Possible evidence:

- newspaper
- photograph
- principal letter
- investigation notice
- school charter
- hospital record
- old books
- historical documents

Target: **16 collectible/evidence particle feedback instances**.

---

### ⬜ Spatial Sound Content — 3 pts

**Target:** 16+ spatial audio feedback instances

Plan:

The same historical evidence objects will play short spatialized pickup sounds when collected.

This lets the historical collectible system contribute to both:

- Particle Feedback Content
- Spatial Sound Content

Target: **16 spatial audio feedback instances**.

---

# Score Summary

| Category          |   Completed |      Maximum |
| ----------------- | ----------: | -----------: |
| Core Requirements |           0 |           21 |
| Side Quests       |           0 |            8 |
| Content Stories   |           0 |           13 |
| **Total**   | **0** | **42** |

---

# Development Priorities

## Phase 1 — Basic VR Scene

- [ ] Principal's Office room geometry
- [ ] XR Origin
- [ ] Controller tracking
- [ ] World Space UI
- [ ] Basic lighting
- [ ] Custom materials

## Phase 2 — Core Interaction

- [ ] Controller input
- [ ] Object spawning
- [ ] Particle feedback
- [ ] Spatial sound
- [ ] Camera teleport
- [ ] Quit button

## Phase 3 — Chronal Orb Physics

- [ ] Euler Steady
- [ ] Double Integrator
- [ ] Object Shooter
- [ ] Arbitrary Orbiter
- [ ] Perfect Orbit

## Phase 4 — Content

- [ ] 16+ object assets
- [ ] 13+ materials
- [ ] 16+ particle feedback instances
- [ ] 16+ spatial sound instances

## Phase 5 — Final Testing

- [ ] Test in VR headset
- [ ] Standalone APK build
- [ ] Gameplay demonstration video
- [ ] Contribution Summary
